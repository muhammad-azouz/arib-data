using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AribONE.Models;

namespace AribONE.Models.Entities;

/// <summary>
/// A discount rule HQ authors once and every eligible bill takes automatically
/// (tasks/spec-promotions.md).
///
/// <b>Tier A</b> — replicated in full to every branch, like Products and Roles (D1). That
/// means a branch-scoped promotion physically lands at every branch; it is filtered
/// <i>at evaluation time</i> by <see cref="BranchId"/>, not at sync time, because the master
/// tier has no branch filter. Accepted deliberately: the row count is trivial, the same is
/// already true of Products/Users/Accounts, and the alternative (Tier B) would make a
/// company-wide promotion unrepresentable, since a Tier-B row must belong to exactly one branch.
///
/// Nothing here is money the bill knows about on its own — a promotion only ever <i>produces</i>
/// a discount that flows through the existing <c>InvoiceLine.Discount</c> /
/// <c>Invoice.BillDiscount</c> columns (D7). This table adds no new money concept.
/// </summary>
public class Promotion
{
    public Guid Id { get; set; }

    /// <summary>Shown on the cart line, on the receipt, and snapshotted onto every
    /// <see cref="PromotionApplication"/> so a later rename cannot rewrite history.</summary>
    [MaxLength(100)] public required string Name { get; set; }

    public PromotionLevel Level { get; set; }

    /// <summary>Meaningless at <see cref="PromotionLevel.Bill"/>; validated server-side rather
    /// than modelled as two tables, so "list every promotion" stays one query and the audit
    /// snapshot stays uniform.</summary>
    public PromotionScope Scope { get; set; }

    public DiscountType DiscountType { get; set; }

    /// <summary>A raw percentage number (e.g. 15) when <see cref="DiscountType"/> is
    /// Percentage, money when Fixed — the same convention the manual bill discount already
    /// uses (<c>NewSaleViewModel.EffectiveBillDiscount</c>).
    ///
    /// Money by the global convention, so <b>no [Precision] attribute</b>: the pre-convention
    /// decimal(18,2) in ConfigureConventions is applied with an Explicit configuration source
    /// and outranks a data annotation, which is exactly how InvoiceLine's attributes were
    /// silently ignored until v16 had to bump the schema to fix it.</summary>
    public decimal Value { get; set; }

    /// <summary>Null means company-wide: applies at every branch (D2). Non-null means only
    /// there.
    ///
    /// <b>Deliberately no navigation property and no FK</b>, matching <see cref="Account"/>,
    /// the other master-tier table carrying a BranchId. Branches are cloud-authoritative and
    /// never DMS-synced (D6 of the sync roadmap), so a master row that replicates to every
    /// branch cannot assume the branch it names exists in the local Branches cache at apply
    /// time. A correlation id sidesteps an FK-ordering wedge of exactly the kind that stopped
    /// sync dead in SyncScope v15.</summary>
    public Guid? BranchId { get; set; }

    /// <summary>Inclusive, date-only semantics, compared against the <b>branch-local</b> date
    /// (D8) — a promotion "ending 31 Aug" must last through the 31st at the till.</summary>
    public DateTime StartsOn { get; set; }

    /// <inheritdoc cref="StartsOn"/>
    public DateTime EndsOn { get; set; }

    /// <summary>A pause the owner expects to undo, and a different thing from
    /// <see cref="IsDeleted"/>: paused promotions still show in the console, deleted ones
    /// do not (D9).</summary>
    public bool IsActive { get; set; }

    /// <summary>Item level only; null means no threshold, and the comparison is inclusive
    /// (<c>qty &gt;= MinQty</c>). A quantity, so 18,3 — set fluently in OnModelCreating, not
    /// by attribute, for the reason given on <see cref="Value"/>.</summary>
    public decimal? MinQty { get; set; }

    /// <summary>Bill level only; null means no threshold, inclusive comparison. Money, so the
    /// (18,2) convention applies with no override.</summary>
    public decimal? MinBillTotal { get; set; }

    /// <summary>Tie-break input (D5): on an otherwise exact tie the newer campaign wins.</summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>Soft delete (D9). A hard delete would leave "why did this bill get a discount
    /// from a promotion that doesn't exist?" unanswerable when reconciling, and the row is
    /// tiny.</summary>
    public bool IsDeleted { get; set; }

    /// <summary>Empty for <see cref="PromotionScope.AllProducts"/> — storewide is the
    /// <b>absence</b> of targets, never a magic row (D3).</summary>
    public ICollection<PromotionTarget> Targets { get; set; } = null!;
}
