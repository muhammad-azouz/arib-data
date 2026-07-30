using System;

namespace AribONE.Models.Entities;

/// <summary>
/// A resolved cost/expiry layer carried on the document (tasks/spec-warehouse-transfer.md D6),
/// invisible to the UI. Batch-tracked products get one layer per <c>InventoryBatchConsumption</c>
/// drawn at dispatch; WA-without-expiry products get exactly one synthetic layer at the source
/// WPI's UnitCost. Replayed through <c>InventoryCostingService.AddBatch</c> at receipt — this is
/// the only way the receiving branch can reconstruct what the sender consumed, since
/// InventoryBatchConsumption is branch-filtered and never crosses the boundary.
/// </summary>
public class StockTransferLayer
{
    public Guid Id { get; set; }

    public Guid StockTransferLineId { get; set; }
    public StockTransferLine StockTransferLine { get; set; } = null!;

    public decimal Qty { get; set; }
    public decimal UnitCost { get; set; }

    public DateTime? ExpiryDate { get; set; }
    public string? BatchNumber { get; set; }

    /// <summary>Traces back to the sender's <see cref="InventoryBatch"/> for audit only — no FK
    /// constraint (the layer is a copy, not a link; the source batch's lifecycle is independent).</summary>
    public Guid? SourceBatchId { get; set; }

    public Guid FromBranchId { get; set; }
    public Guid ToBranchId { get; set; }
}
