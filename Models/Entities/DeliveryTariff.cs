using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

/// <summary>The delivery fee this branch charges for one zone — the middle layer of D4's
/// three-layer resolution (tasks/spec-delivery-couriers.md).
///
/// Keyed <c>(BranchId, AreaId)</c> rather than a single number on <see cref="Area"/> (D5):
/// <see cref="Area"/> is a Tier-A master replicated to every branch, so a fee living there
/// would force ten branches of a chain to charge one price for a neighbourhood that is next
/// door to one of them and across town from another. This is the smallest table that expresses
/// the real thing without making <see cref="Area"/> branch-aware.
///
/// <b>A missing row means "no tariff", never a zero row.</b> The resolver takes the first
/// <i>non-null</i> layer, so a stored <c>0</c> would read as "free delivery for this zone" and
/// suppress the branch default — <c>DeliveryTariffService.SetAsync(null)</c> deletes the row.
///
/// Tier B, own <c>BranchId</c> filter (SyncScope v15).</summary>
public class DeliveryTariff
{
    public Guid Id { get; set; }

    /// <summary>The pricing branch — the sync filter column.</summary>
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    public Guid AreaId { get; set; }
    public Area Area { get; set; } = null!;

    /// <summary>Money — left to the global decimal(18,2) convention, no precision override.</summary>
    public decimal Fee { get; set; }
}
