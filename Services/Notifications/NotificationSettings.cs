using System;

namespace AribONE.Services.Notifications;

/// <summary>
/// Immutable read snapshot of the branch's <c>NotificationSetting</c> that the engine and
/// rules consume, behind a static provider (mirroring <c>AribContext.BranchIdProvider</c>):
/// the host loads the row once and points <see cref="Provider"/> at the cached value, so a
/// rule never hits the DB for config on every evaluation, and a node that never sets the
/// provider (or runs before the row exists) gets all-enabled defaults.
/// </summary>
public sealed record NotificationSettingsSnapshot
{
    public bool InventoryEnabled { get; init; } = true;
    public bool ExpiryEnabled { get; init; } = true;
    public bool FinanceEnabled { get; init; } = true;
    public bool SystemEnabled { get; init; } = true;

    public int ExpiryDaysAhead { get; init; } = 30;
    public int InstallmentDueSoonDays { get; init; } = 7;
    public int BackupStaleDays { get; init; } = 2;

    /// <summary>Whether a rule's <see cref="INotificationRule.Category"/> is enabled. Unknown
    /// categories default to enabled so a new category works before it gets a toggle.</summary>
    public bool IsCategoryEnabled(string category) => category switch
    {
        NotificationCategories.Inventory => InventoryEnabled,
        NotificationCategories.Expiry => ExpiryEnabled,
        NotificationCategories.Finance => FinanceEnabled,
        NotificationCategories.System => SystemEnabled,
        _ => true,
    };

    /// <summary>Supplies the active branch's settings. The desktop host sets this to its cached
    /// snapshot; the default returns defaults so the engine/rules are always safe to read.</summary>
    public static Func<NotificationSettingsSnapshot> Provider { get; set; } = static () => new();

    public static NotificationSettingsSnapshot Current => Provider();
}
