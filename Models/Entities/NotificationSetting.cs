using System;

namespace AribONE.Models.Entities;

/// <summary>
/// Branch-wide notification configuration: which categories are enabled and the time-window
/// thresholds for the time-based rules. One row per branch in the shared branch DB, so every
/// POS terminal — and the future console worker — reads the same config.
///
/// Inventory has no threshold here: low/out/negative is derived from each
/// <c>Product.ReOrder</c>, so only the on/off toggle applies.
///
/// Local-only: NOT in <c>SyncScope</c> — config, like the notifications themselves, is held
/// per node, never synced.
/// </summary>
public class NotificationSetting
{
    public Guid Id { get; set; }

    public Guid BranchId { get; set; }

    public bool InventoryEnabled { get; set; } = true;
    public bool ExpiryEnabled { get; set; } = true;
    public bool FinanceEnabled { get; set; } = true;
    public bool SystemEnabled { get; set; } = true;

    /// <summary>Days ahead a batch is flagged "expiring soon" (ExpiryRule).</summary>
    public int ExpiryDaysAhead { get; set; } = 30;

    /// <summary>Days ahead an unpaid installment is flagged "due soon" (InstallmentRule).</summary>
    public int InstallmentDueSoonDays { get; set; } = 7;

    /// <summary>Days without a fresh backup before the backup-missing warning fires (BackupHealthRule).</summary>
    public int BackupStaleDays { get; set; } = 2;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
