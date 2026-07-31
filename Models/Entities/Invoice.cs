using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public abstract class Invoice : IShiftScoped
{
    public Guid Id { get; set; }
    [StringLength(16)] public required string Num { get; set; }

    /// <summary>The shift that owns this invoice when Shift Mode is on; null in Open
    /// Safe mode (and for all pre-shift history). Stamped by ShiftIdInterceptor.
    /// (Repurposed from a dead pre-GUID int column that was always 0.)</summary>
    public Guid? ShiftId { get; set; }

    public InvoiceType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime IssuedAt { get; set; }
    public Guid? PartnerId { get; set; }
    public Partner? Partner { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
    public ICollection<InvoiceLine> InvoiceLines { get; set; } = null!;

    [MaxLength(50)] public required string warehouse { get; set; }
    [MaxLength(50)] public string? Ship { get; set; }
    [MaxLength(50)] public string? ShipAddress { get; set; }
    [MaxLength(12)] public string? ShipPhone1 { get; set; }
    [MaxLength(12)] public string? ShipPhone2 { get; set; }
    public decimal ItemTotal { get; set; }
    public decimal BillDiscount { get; set; }
    public Guid BillDiscountId { get; set; }
    public decimal ItemDiscount { get; set; }
    public Guid ItemDiscountId { get; set; }
    public decimal BillTax { get; set; }
    public decimal BillTaxPercentage { get; set; }
    public Guid BillTaxId { get; set; }
    public decimal Money { get; set; }
    public Guid MoneyId { get; set; }
    public decimal WalletMoney { get; set; }
    public Guid? WalletId { get; set; }
    public decimal BankMoney { get; set; }
    public Guid? BankId { get; set; }
    public decimal Total { get; set; }
    public decimal Remain { get; set; }
    public decimal TotalMoney { get; set; }
    public Guid BillExtraId { get; set; }
    public decimal TotalExtra { get; set; }
    public decimal TotalDiscount { get; set; }

    /// <summary>Ledger-based snapshot of the customer's/supplier's account
    /// balance strictly before this invoice's own PartnerLedgerEntry row,
    /// computed once at finalize time (SUM(Debit-Credit) over all earlier
    /// ledger rows). Null when never computed (no Partner, an Order invoice, or
    /// an invoice predating this feature) — PrintingService falls back to a live
    /// recompute in that case. Frozen forever after finalize: never touched
    /// by later payments, reversals, or deletions of earlier invoices.</summary>
    public decimal? PreviousBalance { get; set; }

    /// <summary>PreviousBalance + this invoice's own ledger contribution
    /// (Debit - Credit) at finalize time. Same snapshot/freeze semantics as
    /// PreviousBalance.</summary>
    public decimal? EndingBalance { get; set; }

    public Guid RegNum { get; set; }

    /// <summary>For a SalesReturn or PurchaseReturn created via the one-click return flow,
    /// the Sale/Purchase it returns. A correlation id, not an enforced FK — same posture as
    /// RegNum, which avoids a self-referential FK on this TPH table. Null for
    /// manually-entered returns and every other invoice type.</summary>
    public Guid? OriginalInvoiceId { get; set; }

    public int ItemCount { get; set; }
    public bool IsCash { get; set; }
    public bool IsPaid { get; set; }
    public DateTime? PaidDate { get; set; }
    public decimal? PaidValue { get; set; }
    public decimal? PaidDiscount { get; set; }
    public decimal? MoneyTotalPaid { get; set; }
    public Guid? PaidRegNum { get; set; }
    [MaxLength(500)] public string? InternalNote { get; set; }
    [MaxLength(500)] public string? Note { get; set; }
    public bool IsDeleted { get; set; }
}
