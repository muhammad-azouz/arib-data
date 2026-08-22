using System;
using Microsoft.EntityFrameworkCore;

namespace AribONE.Models.Entities;

public class ReservationFulfillment
{
    public Guid Id { get; set; }

    /// <summary>The reserved line this release/return draws down. Renamed from
    /// <c>OrderLineId</c> in tasks/spec-reservation-audit-management.md (D11b): <c>OrderLine</c>
    /// now names an unrelated entity — a طلبية line on the order-management table — so the old
    /// name pointed a reader at the wrong table.</summary>
    public Guid ReservationLineId { get; set; }
    public ReservationLine ReservationLine { get; set; } = null!;

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
