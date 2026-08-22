using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

/// <summary>A delivery courier — «مندوب» (tasks/spec-delivery-couriers.md).
///
/// Deliberately its own small table, neither a <see cref="User"/> nor a <see cref="Partner"/>
/// (D1): couriers do not log into the POS, so modelling them as users would mean a fake
/// credential, a role and a seed row per hire — and they would then pollute every user picker,
/// shift list and audit dropdown; modelling them as partners would put motorbike drivers in the
/// customer list.
///
/// Does <b>not</b> implement <see cref="IShiftScoped"/> — a courier moves no cash in v1, the
/// same reasoning as <see cref="Order"/>'s D13. Courier cash settlement is phase 2 and gets its
/// own spec (D11); until then the money still lands in the treasury at تم التسليم.
///
/// Tier B, own <c>BranchId</c> filter (SyncScope v15).</summary>
public class Courier
{
    public Guid Id { get; set; }

    [MaxLength(100)] public required string Name { get; set; }

    [MaxLength(12)] public string? Phone { get; set; }

    /// <summary>The employing branch — the sync filter column. One branch per courier in v1
    /// (spec OQ1: a courier pool shared across a chain would need the two-sided treatment
    /// <c>StockTransfers</c> uses).</summary>
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    /// <summary>Deactivate, never delete (D12): a false value hides the courier from every
    /// picker and from the live board's idle list while keeping every historical order
    /// readable. Deactivation is refused while the courier still holds an
    /// <c>OutForDelivery</c> order.</summary>
    public bool IsActive { get; set; } = true;

    /// <summary>Referenced by <b>no code</b> in v1 — the named seam (D11) a future courier
    /// login / courier app plugs into without a schema change, i.e. without a fleet flag day.</summary>
    public Guid? UserId { get; set; }
    public User? User { get; set; }

    [MaxLength(200)] public string? Note { get; set; }

    public DateTime CreatedAt { get; set; }
}
