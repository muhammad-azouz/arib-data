using System;

namespace AribONE.Models.Entities;

/// <summary>
/// A cost + expiry layer for a product in a warehouse. One row is created per
/// FIFO/LIFO purchase, per expiry-tracked purchase, per opening balance, and per
/// stock-increasing adjustment. Outgoing movements draw down <see cref="RemainingQty"/>
/// in FEFO order (when the product tracks expiry) or by <see cref="ReceivedDate"/>
/// (FIFO asc / LIFO desc). Invariant: for batch-tracked products,
/// SUM(RemainingQty) == WarehouseProductInventory.TotalQty (when non-negative).
/// </summary>
public class InventoryBatch
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    /// <summary>Optional supplier lot / batch number captured at purchase.</summary>
    public string? BatchNumber { get; set; }

    public DateTime? ExpiryDate { get; set; }
    public DateTime ReceivedDate { get; set; }

    public decimal InitialQty { get; set; }
    public decimal RemainingQty { get; set; }

    /// <summary>Cost per base unit for this layer.</summary>
    public decimal UnitCost { get; set; }

    /// <summary>RegNum of the bill / adjustment / opening balance that created this layer.</summary>
    public Guid SourceRegNum { get; set; }

    public DateTime CreatedAt { get; set; }
}
