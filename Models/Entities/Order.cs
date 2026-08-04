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

    /// <summary>Free text, not a <see cref="User"/> FK — couriers are frequently not system
    /// users (D15).</summary>
    [MaxLength(100)] public string? CourierName { get; set; }

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
