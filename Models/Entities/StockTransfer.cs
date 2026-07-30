using System;
using System.Collections.Generic;

namespace AribONE.Models.Entities;

/// <summary>
/// A stock transfer document — moves goods from one warehouse to another.
/// Same-branch transfers are born <see cref="StockTransferStatus.Received"/> in one step;
/// cross-branch transfers go <c>Dispatched</c> → <c>Received</c>, one leg posted by each
/// branch under its own <see cref="DispatchRegNum"/>/<see cref="ReceiptRegNum"/> so each
/// side's rows stay independently reversible (tasks/spec-warehouse-transfer.md D3/D9).
///
/// The dispatcher names a destination <see cref="ToBranchId"/>; the receiver picks the
/// destination <see cref="ToWarehouseId"/> at receipt time (D2) — a branch never sees another
/// branch's warehouses under branch-filtered sync.
/// </summary>
public class StockTransfer
{
    public Guid Id { get; set; }

    /// <summary>Per-branch sequence, assigned at dispatch.</summary>
    public int Num { get; set; }

    public StockTransferStatus Status { get; set; }

    public Guid FromBranchId { get; set; }
    public Branch FromBranch { get; set; } = null!;

    /// <summary>No FK to <see cref="Warehouse"/> (matches <c>InventoryAdjustment.WarehouseId</c>'s
    /// existing loose-column precedent) — Warehouses is single-branch-filtered, so once this
    /// document crosses to the other branch under D4, that branch has no local row for the
    /// sender's warehouse and a real constraint would fail the sync merge.</summary>
    public Guid FromWarehouseId { get; set; }

    public Guid ToBranchId { get; set; }
    public Branch ToBranch { get; set; } = null!;

    /// <summary>Null until receipt (D2) — chosen by the receiving branch, not the dispatcher.
    /// No FK to <see cref="Warehouse"/>, same reason as <see cref="FromWarehouseId"/> but
    /// mirrored: the sender has no local row for the receiver's warehouse.</summary>
    public Guid? ToWarehouseId { get; set; }

    public DateTime DispatchedAt { get; set; }
    public Guid DispatchedByUserId { get; set; }
    public User DispatchedByUser { get; set; } = null!;

    public DateTime? ReceivedAt { get; set; }
    public Guid? ReceivedByUserId { get; set; }
    public User? ReceivedByUser { get; set; }

    /// <summary>Set only when <see cref="Status"/> is <see cref="StockTransferStatus.Cancelled"/>
    /// (D9 — cancel is only reachable while still <c>Dispatched</c>).</summary>
    public DateTime? CancelledAt { get; set; }
    public Guid? CancelledByUserId { get; set; }
    public User? CancelledByUser { get; set; }

    public string? Note { get; set; }

    /// <summary>RegNum of the sender's leg (batch consumption + InventoryMovement + GL).</summary>
    public Guid DispatchRegNum { get; set; }

    /// <summary>RegNum of the receiver's leg. Null until receipt.</summary>
    public Guid? ReceiptRegNum { get; set; }

    public decimal TotalCost { get; set; }
    public int ItemCount { get; set; }

    public List<StockTransferLine> Lines { get; set; } = [];
}
