using System;

namespace AribONE.Models.Entities;

/// <summary>
/// One link in the company-wide fiscal calendar chain (roadmap: Fiscal Year).
/// Years are always strict 12-month spans with no gaps or overlaps; the chain
/// itself is the configuration — there is no separate "start month" setting.
///
/// Company-wide (no <c>BranchId</c>): closing batches are posted per-branch,
/// but the calendar is one chain per tenant. No <c>Name</c> column — the
/// display label is computed from <see cref="StartDate"/>/<see cref="EndDate"/>.
/// </summary>
public class FiscalYear
{
    public Guid Id { get; set; }

    /// <summary>Inclusive; date-only semantics.</summary>
    public DateTime StartDate { get; set; }

    /// <summary>Inclusive; = StartDate.AddYears(1).AddDays(-1).</summary>
    public DateTime EndDate { get; set; }

    public FiscalYearStatus Status { get; set; }

    // --- Close (null until closed) ---
    public DateTime? ClosedAt { get; set; }
    public Guid? ClosedByUserId { get; set; }

    /// <summary>RegNum of the closing GeneralLedgerEntry batch; the reversal handle
    /// for <c>FiscalYearService.ReopenYearAsync</c>.</summary>
    public Guid? ClosingRegNum { get; set; }

    /// <summary>Snapshotted at close: Σ(revenue) − Σ(expense) over the year.</summary>
    public decimal NetProfit { get; set; }

    public DateTime CreatedAt { get; set; }
}
