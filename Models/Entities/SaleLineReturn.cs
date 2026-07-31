using System;
using Microsoft.EntityFrameworkCore;

namespace AribONE.Models.Entities;

/// <summary>Ledger of how much of a sale line has been returned so far, mirroring
/// OrderFulfillment's Order→Sale/SalesReturn linkage for the Sale→SalesReturn case.
/// One row per return line traced back to the sale line it pays back; remaining
/// returnable qty is TotalQty minus the sum of these rows, never a cached column.</summary>
public class SaleLineReturn
{
    public Guid Id { get; set; }

    public Guid SaleLineId { get; set; }
    public SaleLine SaleLine { get; set; } = null!;

    public Guid SalesReturnLineId { get; set; }
    public SalesReturnLine SalesReturnLine { get; set; } = null!;

    [Precision(18, 3)] public decimal Qty { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
}
