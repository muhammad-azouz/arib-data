using System;
using System.Threading;
using System.Threading.Tasks;
using AribONE.Models.Entities;
using AribONE.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AribONE.Services.Notifications;

/// <summary>
/// Loads/persists the branch's <see cref="NotificationSetting"/> row as a
/// <see cref="NotificationSettingsSnapshot"/>. Shared so the desktop settings UI and a future
/// console worker read/write config through the same path. One fresh <see cref="AribContext"/>
/// per call (repo convention); the row is local-only, not synced.
/// </summary>
public static class NotificationSettingsStore
{
    /// <summary>The branch's settings, or defaults when no row exists yet.</summary>
    public static async Task<NotificationSettingsSnapshot> LoadAsync(Guid branchId, CancellationToken ct = default)
    {
        await using var db = new AribContext();
        var row = await db.NotificationSettings.AsNoTracking()
            .FirstOrDefaultAsync(s => s.BranchId == branchId, ct);
        return row is null ? new NotificationSettingsSnapshot() : ToSnapshot(row);
    }

    /// <summary>Upserts the branch's single settings row from the snapshot.</summary>
    public static async Task SaveAsync(NotificationSettingsSnapshot snapshot, Guid branchId, CancellationToken ct = default)
    {
        await using var db = new AribContext();
        var row = await db.NotificationSettings.FirstOrDefaultAsync(s => s.BranchId == branchId, ct);
        if (row is null)
        {
            row = new NotificationSetting { BranchId = branchId };
            db.NotificationSettings.Add(row);
        }

        row.InventoryEnabled = snapshot.InventoryEnabled;
        row.ExpiryEnabled = snapshot.ExpiryEnabled;
        row.FinanceEnabled = snapshot.FinanceEnabled;
        row.SystemEnabled = snapshot.SystemEnabled;
        row.ExpiryDaysAhead = snapshot.ExpiryDaysAhead;
        row.InstallmentDueSoonDays = snapshot.InstallmentDueSoonDays;
        row.BackupStaleDays = snapshot.BackupStaleDays;
        row.UpdatedAt = DateTime.Now;

        await db.SaveChangesAsync(ct);
    }

    private static NotificationSettingsSnapshot ToSnapshot(NotificationSetting row) => new()
    {
        InventoryEnabled = row.InventoryEnabled,
        ExpiryEnabled = row.ExpiryEnabled,
        FinanceEnabled = row.FinanceEnabled,
        SystemEnabled = row.SystemEnabled,
        ExpiryDaysAhead = row.ExpiryDaysAhead,
        InstallmentDueSoonDays = row.InstallmentDueSoonDays,
        BackupStaleDays = row.BackupStaleDays,
    };
}
