using System;
using System.ComponentModel.DataAnnotations;
using AribONE.Models;

namespace AribONE.Models.Entities;

/// <summary>
/// One promotion, as actually applied to one bill (tasks/spec-promotions.md D6). The audit
/// overlay that makes promotional money separable from money a cashier typed by hand.
///
/// <b>Tier B</b>, BranchId-filtered, declared after <c>Invoices</c>/<c>InvoiceLines</c> in
/// <c>SyncScope.BranchTables</c> so <see cref="InvoiceId"/>'s FK resolves at apply time.
///
/// <b>Append-only.</b> Nothing may update or delete a row here — so ServerWins has nothing to
/// resolve, the same reasoning <see cref="DocumentAuditEntry"/> carries. Editing a saved bill
/// replaces its rows by inserting a fresh set against the new invoice, never by mutating.
///
/// <b>Why this table exists at all:</b> the invoice already splits item from bill discount
/// (<c>Invoice.ItemDiscount</c> / <c>BillDiscount</c>), but those columns cannot say <i>which</i>
/// promotion caused them, or whether a promotion caused them at all. The reconciliation
/// invariant is:
///
/// <code>
/// Σ Amount WHERE Level=Item AND InvoiceId=X  ≤  Invoice[X].ItemDiscount
/// Σ Amount WHERE Level=Bill AND InvoiceId=X  ≤  Invoice[X].BillDiscount
/// </code>
///
/// with equality when every discount on the bill was promotional — and <b>the shortfall is
/// exactly the money the cashier typed</b>. That difference is the whole point: four separable
/// numbers (promotional/manual × item/bill) out of columns that already existed.
/// </summary>
public class PromotionApplication
{
    public Guid Id { get; set; }

    public Guid InvoiceId { get; set; }

    /// <summary>Restrict, not Cascade: an audit row must not vanish because someone hard-deleted
    /// the bill. Invoices are soft-deleted (<c>Invoice.IsDeleted</c>) in normal operation, so
    /// this blocks nothing real — it just makes an accidental hard delete fail loudly instead of
    /// silently erasing the trail.</summary>
    public Invoice Invoice { get; set; } = null!;

    /// <summary>The discounted line for an item-level application; <b>null exactly when</b>
    /// <see cref="Level"/> is Bill.
    ///
    /// No FK and no navigation: the line lives in the same invoice this row already points at,
    /// so a second FK buys nothing while adding another apply-order constraint to a table that
    /// is already the last one in its tier.</summary>
    public Guid? InvoiceLineId { get; set; }

    /// <summary>Which promotion. <b>A correlation id, not an FK</b> — two reasons, both
    /// decisive: this is a cross-tier reference (a branch row pointing at a master row), which
    /// is the exact shape that wedged sync permanently in v15; and an audit row has to outlive
    /// its promotion being deleted, which is the entire reason the table is worth keeping.</summary>
    public Guid PromotionId { get; set; }

    // ── snapshot ────────────────────────────────────────────────────────────────────────
    // Frozen at apply time. Editing a promotion from 15% to 20% next week must not rewrite
    // what last week's bills say they gave away. Same freeze semantics as
    // Invoice.PreviousBalance/EndingBalance, and the same reasoning DocumentAuditEntry.Subject
    // records: the point of an audit row is what was true *then*, not what a lookup resolves
    // to now.

    /// <summary>The promotion's name as it stood when this bill was rung up.</summary>
    [MaxLength(100)] public required string PromotionName { get; set; }

    public PromotionLevel Level { get; set; }

    public DiscountType DiscountType { get; set; }

    /// <summary>The rate or flat amount as it stood at apply time — a raw percentage number
    /// when <see cref="DiscountType"/> is Percentage, money when Fixed.</summary>
    public decimal Value { get; set; }

    /// <summary>
    /// The money this promotion actually took off.
    ///
    /// <b>This is the stored discount value, not the totals-side one.</b> For a percentage
    /// promotion the two differ: the sale screen subtracts a plain-rounded figure from the
    /// totals while the value written to <c>InvoiceLine.Discount</c> — and therefore summed
    /// into <c>Invoice.ItemDiscount</c> — is separately quarter-rounded
    /// (<c>RoundToNearestQuarter</c>; see <c>NewSaleViewModel.RecalcLineForQty</c>, which spells
    /// the decoupling out). Since the invariant above reconciles against those columns, this
    /// must hold what they hold. Recording the totals-side figure instead would leave the
    /// invariant off by the rounding difference on every percentage promotion — a slow,
    /// plausible-looking drift rather than an obvious break.
    ///
    /// <c>PromotionMatch.LineDiscountValue</c> is the number to write here;
    /// <c>PromotionMatch.Amount</c> is the one to subtract from totals.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>The sync filter column (D2). Unlike <see cref="Promotion.BranchId"/> this is a
    /// real FK: a Tier-B row is filtered to its own branch, so the branch it names is always the
    /// local one.</summary>
    public Guid BranchId { get; set; }

    public Branch Branch { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
