using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace AribONE.Services;

/// <summary>PIN hashing and brute-force protection for AribLink logins
/// (tasks/spec-ariblink-gateway.md D10, T6). Lives in <c>Services/</c>, not <c>Gateway/</c>, because
/// <see cref="Hash"/>/<see cref="Verify"/> is also the primitive the users screen's "set a PIN"
/// field calls (T7) — there is exactly one implementation, and a ViewModel has no business
/// referencing the Gateway layer to reach it.</summary>
public static class PinAuth
{
    private const int Iterations = 100_000;
    private const int SaltBytes = 16;
    private const int HashBytes = 32;

    /// <summary>5 consecutive failures ⇒ locked for 15 minutes (D10/§9.1), applied independently
    /// on two axes: the username (persisted on <c>User</c>, T6) and the fingerprint (in-memory
    /// here) — so an attacker cannot spread guesses across accounts from one machine.</summary>
    private const int MaxFailures = 5;
    private static readonly TimeSpan LockDuration = TimeSpan.FromMinutes(15);

    // A fixed dummy salt/hash computed once at startup so the "no such user" / "user has no PIN"
    // path spends the same PBKDF2 cost as a real verify — Derive() never short-circuits, so the
    // failure path is indistinguishable by timing from a wrong PIN on a real account (D10/§9.1).
    private static readonly byte[] DummySalt = RandomNumberGenerator.GetBytes(SaltBytes);
    private static readonly byte[] DummyHash = Derive("0000", DummySalt);

    /// <summary>Produces the <c>{salt}.{hash}</c> string stored in <c>User.PinHash</c>.</summary>
    public static string Hash(string pin)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltBytes);
        var hash = Derive(pin, salt);
        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    /// <summary>Constant-time verify. <paramref name="storedHash"/> null (no PIN set) or
    /// unparsable both refuse — after spending the same PBKDF2 cost as a match would have.</summary>
    public static bool Verify(string? storedHash, string pin)
    {
        if (storedHash is not null && TrySplit(storedHash, out var salt, out var expected))
            return CryptographicOperations.FixedTimeEquals(Derive(pin, salt), expected);

        Derive(pin, DummySalt); // spend the cost; the comparison below always fails
        return false;
    }

    private static byte[] Derive(string pin, byte[] salt) =>
        Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(pin), salt, Iterations, HashAlgorithmName.SHA256, HashBytes);

    private static bool TrySplit(string stored, out byte[] salt, out byte[] hash)
    {
        var parts = stored.Split('.', 2);
        if (parts.Length == 2)
        {
            try
            {
                salt = Convert.FromBase64String(parts[0]);
                hash = Convert.FromBase64String(parts[1]);
                return true;
            }
            catch (FormatException)
            {
                // fall through to the failure return below
            }
        }

        salt = [];
        hash = [];
        return false;
    }

    /// <summary>The in-memory per-fingerprint half of D10's lockout — independent of the
    /// DB-persisted per-username lock on <c>User.PinFailedCount</c>/<c>PinLockedUntil</c>, and
    /// reset only by a restart (it needs no persistence: a restarted gateway is not a faster way
    /// to brute-force a PIN, since the per-username lock still stands).</summary>
    public static class FingerprintThrottle
    {
        private static readonly Dictionary<string, (int Fails, DateTime? LockedUntil)> Attempts = new();
        private static readonly Lock Gate = new();

        public static bool IsLocked(string fingerprint)
        {
            lock (Gate)
                return Attempts.TryGetValue(fingerprint, out var s) && s.LockedUntil is { } until && until > DateTime.UtcNow;
        }

        public static void RegisterFailure(string fingerprint)
        {
            lock (Gate)
            {
                var fails = (Attempts.TryGetValue(fingerprint, out var s) ? s.Fails : 0) + 1;
                DateTime? lockedUntil = fails >= MaxFailures ? DateTime.UtcNow.Add(LockDuration) : null;
                Attempts[fingerprint] = (fails, lockedUntil);
            }
        }

        public static void ClearFailures(string fingerprint)
        {
            lock (Gate)
                Attempts.Remove(fingerprint);
        }
    }

    /// <summary>The DB-persisted per-username half — a pure function over the counters so the
    /// caller owns the <c>AribContext</c>/<c>SaveChangesAsync</c> lifecycle (CLAUDE.md: a fresh
    /// context per operation).</summary>
    public static class UserLock
    {
        public static bool IsLocked(DateTime? pinLockedUntil) => pinLockedUntil is { } until && until > DateTime.UtcNow;

        /// <summary>Returns the two counters' next values for the caller to assign onto the
        /// <c>User</c> entity and save (a fresh <c>AribContext</c> per operation, CLAUDE.md).</summary>
        public static (int PinFailedCount, DateTime? PinLockedUntil) RegisterFailure(int pinFailedCount, DateTime? pinLockedUntil)
        {
            var fails = pinFailedCount + 1;
            return fails < MaxFailures ? (fails, pinLockedUntil) : (fails, DateTime.UtcNow.Add(LockDuration));
        }

        public static (int PinFailedCount, DateTime? PinLockedUntil) ClearFailures() => (0, null);
    }
}
