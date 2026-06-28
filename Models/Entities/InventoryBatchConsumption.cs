using System;

namespace AribONE.Models.Entities;

/// <summary>
/// Links an outgoing movement (sale, return to supplier, stock-decreasing
/// adjustment) to the <see cref="InventoryBatch"/> slices it drew from, so the
/// movement can be reversed exactly (restoring each batch's RemainingQty) and so
/// per-batch COGS is auditable. Shortfall quantities sold beyond available batches
/// are NOT recorded here (there is no batch to restore); WarehouseProductInventory
/// remains the quantity source of truth.
/// </summary>
public class InventoryBatchConsumption
{
    public Guid Id { get; set; }

    public Guid BatchId { get; set; }
    public InventoryBatch Batch { get; set; } = null!;

    /// <summary>RegNum of the outgoing bill / adjustment that consumed the batch.</summary>
    public Guid RegNum { get; set; }

    public Guid ProductId { get; set; }
    public Guid WarehouseId { get; set; }
    public Guid BranchId { get; set; }

    public decimal Qty { get; set; }

    /// <summary>The batch's cost per base unit at the time of consumption.</summary>
    public decimal UnitCost { get; set; }

    public DateTime CreatedAt { get; set; }
}
