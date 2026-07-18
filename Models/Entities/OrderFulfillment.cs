using System;
using Microsoft.EntityFrameworkCore;

namespace AribONE.Models.Entities;

public class OrderFulfillment
{
    public Guid Id { get; set; }

    public Guid OrderLineId { get; set; }
    public OrderLine OrderLine { get; set; } = null!;

    public Guid? SaleLineId { get; set; }
    public SaleLine? SaleLine { get; set; }

    public Guid? SalesReturnLineId { get; set; }
    public SalesReturnLine? SalesReturnLine { get; set; }

    [Precision(18, 3)] public decimal Qty { get; set; }

    public FulfillmentType Type { get; set; }

    public DateTime FulfilledAt { get; set; }

    public Guid FulfilledByUserId { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
}
