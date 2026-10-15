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

    /// <summary>The purchase line that received this reservation's quantity — set only for a
    /// <see cref="FulfillmentType.Release"/> against a <see cref="PurchaseReservation"/>
    /// (tasks/spec-purchase-reservation.md). Exactly one of the three line FKs is non-null.</summary>
    public Guid? PurchaseLineId { get; set; }
    public PurchaseLine? PurchaseLine { get; set; }

    [Precision(18, 3)] public decimal Qty { get; set; }

    public FulfillmentType Type { get; set; }

    public DateTime FulfilledAt { get; set; }

    public Guid FulfilledByUserId { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
}
