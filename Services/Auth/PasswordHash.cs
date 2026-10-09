using System;
using System.Security.Cryptography;
using System.Text;

namespace AribONE.Services;

/// <summary>
/// The branch app's POS password hash: unsalted SHA-256, Base64. This is the format
/// <c>AuthService.AuthenticateAsync</c> compares against <c>User.PasswordHash</c>, and the
/// <c>Users</c> table is Tier-A synced, so a password written centrally (the console's branch
/// staff screen, via the sync gateway) must use exactly this scheme or the user cannot sign in
/// at a branch. It is deliberately the one copy: the desktop's <c>UserService</c>/<c>AuthService</c>
/// and the gateway all call it. Moving to a salted scheme is a separate, fleet-wide change.
/// </summary>
public static class PasswordHash
{
    public static string Legacy(string password) =>
        Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
}
