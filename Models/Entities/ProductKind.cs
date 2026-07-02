namespace AribONE.Models.Entities;

public enum ProductKind
{
    /// <summary>Inventory good — WPI/batches/movements + full perpetual costing
    /// (Sales / Inventory Asset / COGS accounts).</summary>
    Product = 0,

    /// <summary>A service that is only sold (revenue only, no inventory/COGS).
    /// Value 1 is retained from the former <c>Service</c> member so existing
    /// service rows auto-reclassify as sales services with no data backfill.</summary>
    SalesService = 1,

    /// <summary>A service that is only purchased (expense only, no inventory/COGS).</summary>
    PurchaseService = 2,
}
