using System;
using Microsoft.EntityFrameworkCore;

namespace AribONE.Models.Entities;

/// <summary>Ledger of how much of a purchase line has been returned so far, the
/// structural twin of SaleLineReturn for the Purchase→PurchaseReturn case. One row
/// per return line traced back to the purchase line it credits; remaining
/// returnable qty is TotalQty minus the sum of these rows, never a cached column.</summary>
public class PurchaseLineReturn
{
    public Guid Id { get; set; }

    public Guid PurchaseLineId { get; set; }
    public PurchaseLine PurchaseLine { get; set; } = null!;

    public Guid PurchaseReturnLineId { get; set; }
    public PurchaseReturnLine PurchaseReturnLine { get; set; } = null!;

    [Precision(18, 3)] public decimal Qty { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
}
