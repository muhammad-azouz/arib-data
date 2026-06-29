using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AribONE.Models;
using AribONE.Models.Entities;
using AribONE.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AribONE.Services.Notifications;

/// <summary>
/// EF Core <see cref="INotificationStore"/>. The unique index on <c>DedupKey</c> is why
/// this always upserts and never blind-inserts a duplicate.
///
/// One branch DB is shared by every POS terminal, so several terminals can reconcile the
/// same rule at the same instant. Each reconcile runs under a branch+rule-scoped SQL Server
/// application lock (<c>sp_getapplock</c>, 0 timeout): the first terminal in wins and the
/// others skip this pass — the winner's result reaches them through the change-poll. This
/// removes both the unique-key race and the redundant N× work without leader election.
/// </summary>
public sealed class EfNotificationStore : INotificationStore
{
    public async Task<int> ReconcileAsync(
        IReadOnlyCollection<string> ownedTypes,
        IReadOnlyList<NotificationDraft> drafts,
        CancellationToken ct = default)
    {
        // Dedup the rule's own output first (last write wins) so one scan never carries
        // two drafts with the same key into the unique-indexed column.
        var draftByKey = new Dictionary<string, NotificationDraft>();
        foreach (var draft in drafts)
            draftByKey[draft.DedupKey] = draft;
        var draftKeys = draftByKey.Keys.ToList();

        var ownedSet = ownedTypes as IReadOnlyCollection<string> ?? ownedTypes.ToList();
        var branchId = AribContext.BranchIdProvider?.Invoke() ?? Guid.Empty;

        // Stable per-branch, per-rule lock name (the owned-type set identifies the rule).
        var lockResource = $"arib-notif:{branchId}:" +
                           string.Join(",", ownedSet.OrderBy(t => t, StringComparer.Ordinal));

        await using var db = new AribContext();
        // Keep one open connection for the whole read-modify-write so the session-scoped
        // app lock is held across the query and the save, then released in finally.
        await db.Database.OpenConnectionAsync(ct);
        try
        {
            if (!await TryAcquireLockAsync(db, lockResource, ct))
                return 0; // another terminal is reconciling this rule right now — skip.

            // Up to two attempts: if a writer somehow slips past the lock (e.g. the proc is
            // unavailable) and collides on the unique DedupKey, reset and retry once.
            for (var attempt = 1; ; attempt++)
            {
                try
                {
                    return await ApplyAsync(db, draftByKey, draftKeys, ownedSet, branchId, ct);
                }
                catch (DbUpdateException) when (attempt == 1)
                {
                    db.ChangeTracker.Clear();
                }
            }
        }
        finally
        {
            try { await ReleaseLockAsync(db, lockResource); } catch { /* best effort */ }
            await db.Database.CloseConnectionAsync();
        }
    }

    /// <summary>The reconcile read-modify-write itself: upsert each draft by DedupKey,
    /// re-alert reactivated/grown groups, resolve the obsolete ones. Re-queryable so the
    /// caller can retry it after clearing the change tracker.</summary>
    private static async Task<int> ApplyAsync(
        AribContext db,
        Dictionary<string, NotificationDraft> draftByKey,
        List<string> draftKeys,
        IReadOnlyCollection<string> ownedSet,
        Guid branchId,
        CancellationToken ct)
    {
        // Load every still-active owned row (to resolve the obsolete ones) plus any
        // resolved row whose key is being re-drafted (to reactivate rather than collide
        // on the unique DedupKey). Bounded by the active set + the drafted keys.
        var existing = await db.AppNotifications
            .Where(n => n.BranchId == branchId
                        && ownedSet.Contains(n.Type)
                        && (!n.IsResolved || draftKeys.Contains(n.DedupKey)))
            .ToListAsync(ct);
        var byKey = existing.ToDictionary(n => n.DedupKey);

        var now = DateTime.Now;

        foreach (var (key, draft) in draftByKey)
        {
            if (byKey.TryGetValue(key, out var row))
            {
                var wasResolved = row.IsResolved;
                // A new item joining a grouped notification (e.g. another product going
                // negative) is a fresh event — detect it before overwriting the signature.
                var hasNewMembers = HasNewMembers(row.MembersSignature, draft.MembersSignature);

                row.Type = draft.Type;
                row.Category = draft.Category;
                row.Severity = draft.Severity;
                row.Title = draft.Title;
                row.Message = draft.Message;
                row.ReferenceType = draft.ReferenceType;
                row.ReferenceId = draft.ReferenceId;
                row.GroupKey = draft.GroupKey;
                row.Count = draft.Count;
                row.Metadata = draft.Metadata;
                row.MembersSignature = draft.MembersSignature;
                row.ExpiresAt = draft.ExpiresAt;
                row.UpdatedAt = now;

                // A resolved condition that re-appears is a fresh occurrence: reactivate it.
                // An ongoing active group that gains a NEW member is likewise a fresh event.
                // Either way bump AlertSeq: per-user read/dismiss is keyed to it, so the
                // notification goes unread + undismissed for every user at once with no
                // per-user write. An unchanged or shrinking group keeps each user's state so
                // we never re-alert on every scan.
                if (wasResolved || hasNewMembers)
                {
                    row.IsResolved = false;
                    row.ResolvedAt = null;
                    row.AlertSeq += 1;
                    row.CreatedAt = now;
                }
            }
            else
            {
                db.AppNotifications.Add(new AppNotification
                {
                    Type = draft.Type,
                    Category = draft.Category,
                    Severity = draft.Severity,
                    Title = draft.Title,
                    Message = draft.Message,
                    DedupKey = key,
                    ReferenceType = draft.ReferenceType,
                    ReferenceId = draft.ReferenceId,
                    GroupKey = draft.GroupKey,
                    Count = draft.Count,
                    Metadata = draft.Metadata,
                    MembersSignature = draft.MembersSignature,
                    ExpiresAt = draft.ExpiresAt,
                    CreatedAt = now,
                    BranchId = branchId,
                });
            }
        }

        // Auto-resolution: any owned row still active but no longer drafted is obsolete.
        foreach (var row in existing)
        {
            if (row.IsResolved || draftByKey.ContainsKey(row.DedupKey))
                continue;
            row.IsResolved = true;
            row.ResolvedAt = now;
            row.UpdatedAt = now;
        }

        return await db.SaveChangesAsync(ct);
    }

    /// <summary>True when <paramref name="newSig"/> contains at least one member not present in
    /// <paramref name="oldSig"/> — i.e. the group gained a member since the last scan. Members
    /// merely leaving (a shrinking group) returns false, so recovery never re-alerts.</summary>
    private static bool HasNewMembers(string? oldSig, string? newSig)
    {
        if (string.IsNullOrEmpty(newSig)) return false;
        if (string.IsNullOrEmpty(oldSig)) return true;
        var previous = oldSig.Split(',');
        var current = newSig.Split(',');
        return current.Except(previous, StringComparer.Ordinal).Any();
    }

    /// <summary>Tries a non-blocking, session-scoped <c>sp_getapplock</c>. Return codes 0/1
    /// mean granted; negatives mean another session holds it (or the proc errored), in which
    /// case the caller skips this reconcile.</summary>
    private static async Task<bool> TryAcquireLockAsync(AribContext db, string resource, CancellationToken ct)
    {
        try
        {
            var conn = db.Database.GetDbConnection();
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "sp_getapplock";
            cmd.CommandType = CommandType.StoredProcedure;
            AddParam(cmd, "@Resource", resource);
            AddParam(cmd, "@LockMode", "Exclusive");
            AddParam(cmd, "@LockOwner", "Session");
            AddParam(cmd, "@LockTimeout", 0);
            var ret = cmd.CreateParameter();
            ret.ParameterName = "@Result";
            ret.Direction = ParameterDirection.ReturnValue;
            ret.DbType = DbType.Int32;
            cmd.Parameters.Add(ret);
            await cmd.ExecuteNonQueryAsync(ct);
            var code = ret.Value is int i ? i : Convert.ToInt32(ret.Value);
            return code >= 0;
        }
        catch (DbException)
        {
            // If app locks aren't available, fall through and reconcile anyway — the unique
            // index plus the one-shot retry still keep the table consistent.
            return true;
        }
    }

    private static async Task ReleaseLockAsync(AribContext db, string resource)
    {
        var conn = db.Database.GetDbConnection();
        if (conn.State != ConnectionState.Open) return;
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = "sp_releaseapplock";
        cmd.CommandType = CommandType.StoredProcedure;
        AddParam(cmd, "@Resource", resource);
        AddParam(cmd, "@LockOwner", "Session");
        await cmd.ExecuteNonQueryAsync();
    }

    private static void AddParam(DbCommand cmd, string name, object value)
    {
        var p = cmd.CreateParameter();
        p.ParameterName = name;
        p.Value = value;
        cmd.Parameters.Add(p);
    }
}
