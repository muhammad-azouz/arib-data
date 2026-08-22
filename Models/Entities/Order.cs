using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AribONE.Models;

namespace AribONE.Models.Entities;

/// <summary>A call-centre/branch order (tasks/spec-order-management.md). Deliberately a
/// dedicated table, not an <see cref="Invoice"/> TPH subtype (D2) — an order posts no GL,
/// inventory, treasury or partner-ledger row; it becomes real money only when تسليم creates
/// a <see cref="Sale"/> (D6), tracked here by the scalar <see cref="SaleId"/>. Does
/// <b>not</b> implement <see cref="IShiftScoped"/> (D13): an order moves no cash.</summary>
public class Order
{
    public Guid Id { get; set; }

    /// <summary>The only number an order has (D5) — <c>{origin}-{yy}-{counter:D5}</c>,
    /// allocated once at the origin branch/HQ and carried unchanged through every transfer
    /// (D7). Indexed, not unique: a transfer chain shares one <see cref="Ref"/> across
    /// several rows.</summary>
    [MaxLength(16)] public required string Ref { get; set; }

    public OrderStatus Status { get; set; }
    public OrderChannel Channel { get; set; }

    /// <summary>The fulfilling branch — the sync filter column (T3).</summary>
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    public Guid PartnerId { get; set; }
    public Partner Partner { get; set; } = null!;

    /// <summary>Who created it: the real tenant user for a branch order, the HQ anchor row
    /// for a console order (D11).</summary>
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    /// <summary>The console operator's own name, carried as a string — no row, no FK (D11).</summary>
    [MaxLength(100)] public string? CreatedByName { get; set; }

    /// <summary>Who at the branch accepted it — «استلم الطلب», stamped on <c>New → Preparing</c>.</summary>
    public Guid? AcceptedByUserId { get; set; }
    public User? AcceptedByUser { get; set; }

    /// <summary>Denormalized from the partner so the branch can call without a lookup, and
    /// so a transfer (D7/D8) can match the destination partner on phone.</summary>
    [MaxLength(100)] public string? ContactName { get; set; }
    [MaxLength(12)] public string? ContactPhone { get; set; }

    /// <summary>Required when <see cref="Mode"/> is <see cref="FulfillmentMode.Delivery"/> (D15).</summary>
    [MaxLength(200)] public string? ContactAddress { get; set; }

    public FulfillmentMode Mode { get; set; }

    /// <summary>The registered courier carrying this order (tasks/spec-delivery-couriers.md D1).
    /// <b>Sticky</b>: رجوع للفرع deliberately leaves it set (D2/D6) — the failure is attributed via
    /// <see cref="FailedByCourierId"/>, and the last carrier stays visible on the order. Null on
    /// every order placed before the couriers feature, and on pickup orders.</summary>
    public Guid? CourierId { get; set; }
    public Courier? Courier { get; set; }

    /// <summary>The courier's name as it read <i>at dispatch</i> — kept alongside
    /// <see cref="CourierId"/>, not replaced by it (D1). It keeps every pre-feature order readable
    /// with no backfill (those rows carry a name and a null <see cref="CourierId"/>; no migration
    /// invents <see cref="Courier"/> rows from historical free text), and it survives a courier
    /// being renamed. Was free text typed at every dispatch until the couriers feature.</summary>
    [MaxLength(100)] public string? CourierName { get; set; }

    /// <summary>Who was carrying the order when it came back to the branch — captured before any
    /// redispatch overwrites <see cref="CourierId"/>, which is what lets the period report
    /// attribute a failure to courier A after courier B has delivered it (D6).</summary>
    public Guid? FailedByCourierId { get; set; }
    public Courier? FailedByCourier { get; set; }

    /// <summary>When the last failed attempt came back — the period report date-filters on this.</summary>
    public DateTime? FailedAt { get; set; }

    /// <summary>From a fixed list (لم يرد / عنوان خطأ / العميل رفض الاستلام / أخرى) plus free text.</summary>
    [MaxLength(200)] public string? FailedReason { get; set; }

    /// <summary>Incremented on every رجوع للفرع, so a repeatedly-failing order is visible.
    /// The three <c>Failed*</c> fields above are <b>last-attempt</b> values, not a log (D6): if two
    /// different couriers fail the same order only the second is counted. The full narrative stays
    /// in <see cref="DocumentAuditEntry"/> (D7), and reading it into the report later needs no
    /// schema change — which is why a DeliveryAttempt table is not worth a fleet flag day.</summary>
    public int FailedCount { get; set; }

    /// <summary>Prefills the Sale's existing bill extra at delivery (D6/D15) — no new
    /// accounting.</summary>
    public decimal? DeliveryFee { get; set; }

    public DateTime? DispatchedAt { get; set; }

    public int ItemCount { get; set; }

    /// <summary>Indicative quote, never posted (D2); excludes <see cref="DeliveryFee"/>.</summary>
    public decimal Total { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? DueAt { get; set; }

    /// <summary>Drives the "منذ" column on the board.</summary>
    public DateTime? StatusChangedAt { get; set; }

    public DateTime? DeliveredAt { get; set; }

    /// <summary>The Sale produced at delivery (D6). Scalar audit pointer, deliberately no
    /// FK/navigation — should not tie an order's lifecycle to an invoice's.</summary>
    public Guid? SaleId { get; set; }

    /// <summary>Transfer chain (D7). Scalar, deliberately no FK/navigation — points across a
    /// sync boundary at a row the destination branch will never hold.</summary>
    public Guid? PreviousOrderId { get; set; }

    [MaxLength(500)] public string? Note { get; set; }
    [MaxLength(200)] public string? CancelReason { get; set; }

    public List<OrderLine> Lines { get; set; } = [];
}
