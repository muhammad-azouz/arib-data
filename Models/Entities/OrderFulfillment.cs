using System;
using Microsoft.EntityFrameworkCore;

namespace AribONE.Models.Entities;

public class OrderFulfillment
{
    public Guid Id { get; set; }

    public Guid OrderEntryId { get; set; }
    public OrderEntry OrderEntry { get; set; }

    public Guid? SaleEntryId { get; set; }
    public SaleEntry? SaleEntry { get; set; }

    public Guid? ReSaleEntryId { get; set; }
    public ReSaleEntry? ReSaleEntry { get; set; }

    [Precision(18, 3)] public decimal Qty { get; set; }

    public FulfillmentType Type { get; set; }

    public DateTime FulfilledAt { get; set; }

    public Guid FulfilledByUserId { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; }
}
