using System;
using System.Collections.Generic;

namespace AribONE.Models.Entities;

/// <summary>What the operator entered — one product/qty row on a <see cref="StockTransfer"/>.
/// <see cref="UnitCost"/> is the weighted average of this line's <see cref="Layers"/>, kept
/// for display/totals; the layers themselves (tasks/spec-warehouse-transfer.md D6) are what
/// actually replays at receipt.</summary>
public class StockTransferLine
{
    public Guid Id { get; set; }

    public Guid StockTransferId { get; set; }
    public StockTransfer StockTransfer { get; set; } = null!;

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public Guid UnitId { get; set; }
    public UnitOfMeasure Unit { get; set; } = null!;

    public decimal Qty { get; set; }
    public decimal UnitCost { get; set; }

    /// <summary>D7 headroom — receipt is exact today, so this always equals <see cref="Qty"/>
    /// once received; a future partial-receipt flow would let it differ.</summary>
    public decimal? ReceivedQty { get; set; }

    public Guid FromBranchId { get; set; }
    public Guid ToBranchId { get; set; }

    public List<StockTransferLayer> Layers { get; set; } = [];
}
