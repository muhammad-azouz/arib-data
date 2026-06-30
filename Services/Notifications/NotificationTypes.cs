namespace AribONE.Services.Notifications;

/// <summary>Stable notification type/category codes shared by rules and UI. Codes are
/// strings (not enums) so new types add a constant, never a schema change.</summary>
public static class NotificationCategories
{
    public const string Inventory = "Inventory";
    public const string Expiry = "Expiry";
    public const string Finance = "Finance";
    public const string System = "System";
}

public static class NotificationTypes
{
    public const string InventoryLowStock = "Inventory.LowStock";
    public const string InventoryOutOfStock = "Inventory.OutOfStock";
    public const string InventoryNegative = "Inventory.Negative";
    public const string InventoryDuplicateCode = "Inventory.DuplicateCode";

    public const string ExpiryExpiring = "Expiry.Expiring";
    public const string ExpiryExpired = "Expiry.Expired";

    public const string FinanceInstallmentOverdue = "Finance.InstallmentOverdue";
    public const string FinanceInstallmentDueSoon = "Finance.InstallmentDueSoon";
    public const string FinanceCreditLimit = "Finance.CreditLimitExceeded";
    public const string FinanceShiftOverShort = "Finance.ShiftOverShort";
    public const string FinanceShiftForceClosed = "Finance.ShiftForceClosed";

    public const string SystemSyncFailed = "System.SyncFailed";
    public const string SystemUpdateRequired = "System.UpdateRequired";
    public const string SystemBackupMissing = "System.BackupMissing";
}
