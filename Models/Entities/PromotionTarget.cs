using System;
using AribONE.Models;

namespace AribONE.Models.Entities;

/// <summary>
/// One thing an item-level promotion applies to: a product, or a group (tasks/spec-promotions.md D3).
///
/// <b>Tier A</b>, alongside <see cref="Promotion"/> itself, and declared after it in
/// <c>SyncScope.MasterTables</c> so the FK below resolves at apply time — an upload batch is
/// applied table-by-table in that array's declared order, which is load-bearing for
/// correctness and not merely documentation (the v15 lesson).
///
/// One polymorphic table rather than two junction tables: the alternative buys nothing but a
/// second query and a second migration the next time a target kind is added.
/// </summary>
public class PromotionTarget
{
    public Guid Id { get; set; }

    public Guid PromotionId { get; set; }
    public Promotion Promotion { get; set; } = null!;

    public PromotionTargetKind Kind { get; set; }

    /// <summary>A ProductId or a GroupId, per <see cref="Kind"/>.
    ///
    /// <b>No FK by construction</b> — it points at two different tables. Same
    /// "correlation id, not an enforced FK" posture <c>Invoice.OriginalInvoiceId</c> and
    /// <c>RegNum</c> already take, and the reason that posture exists: a self-describing
    /// reference costs nothing at apply time.
    ///
    /// A <see cref="PromotionTargetKind.Group"/> row cascades to every <b>descendant</b> group
    /// (D3). The cascade is resolved at billing time, expanded once when the sale screen opens
    /// — which is what makes a group added under a targeted parent tomorrow automatically
    /// covered, with no edit to the promotion.</summary>
    public Guid RefId { get; set; }
}
