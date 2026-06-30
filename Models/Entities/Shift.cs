using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

/// <summary>
/// A cashier drawer session (roadmap: Shift Management). A shift is a time-bounded
/// session that TAGS existing financial rows (<see cref="IShiftScoped"/>) on the
/// branch's shared treasury — it is not a separate drawer or a parallel ledger.
///
/// Scope: one open shift per workstation (<see cref="WorkstationId"/>); several
/// workstations in a branch can be open at once. Reconciliation is computed from
/// the shift's own tagged rows + <see cref="OpeningCash"/>, never from
/// <c>Treasury.Balance</c>.
///
/// <see cref="TreasuryId"/> defaults to the branch's default treasury today; it is
/// the seam for a future per-drawer GL (the deferred "multiple drawers" feature)
/// with no schema churn. Shifts are never deleted.
/// </summary>
public class Shift
{
    public Guid Id { get; set; }

    /// <summary>Per-branch sequential shift number (human-facing).</summary>
    public int Num { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    /// <summary>The treasury this shift draws on. Defaults to the branch default
    /// treasury; the per-drawer seam for the future multiple-drawers feature.</summary>
    public Guid TreasuryId { get; set; }

    /// <summary>Machine id of the workstation that opened the shift
    /// (<c>MachineIdGenerator</c>). The ambient open-shift lookup keys on this.</summary>
    [MaxLength(100)] public required string WorkstationId { get; set; }

    public ShiftStatus Status { get; set; }

    // --- Open ---
    public Guid OpenedByUserId { get; set; }
    public User OpenedByUser { get; set; } = null!;
    public DateTime OpenedAt { get; set; }
    public decimal OpeningCash { get; set; }
    [MaxLength(500)] public string? OpenNote { get; set; }

    // --- Close (null until closed) ---
    public Guid? ClosedByUserId { get; set; }
    public DateTime? ClosedAt { get; set; }

    /// <summary>System-computed expected drawer cash at close
    /// (= <see cref="OpeningCash"/> + Σ shift-tagged cash movements).</summary>
    public decimal ExpectedCash { get; set; }

    /// <summary>Cashier-counted physical cash at close.</summary>
    public decimal ActualCash { get; set; }

    /// <summary>ActualCash − ExpectedCash. Positive = over, negative = short.</summary>
    public decimal Difference { get; set; }

    [MaxLength(500)] public string? CloseNote { get; set; }

    /// <summary>True when closed by a supervisor (ForceCloseShift) rather than the
    /// opening cashier; <see cref="ClosedByUserId"/> then differs from the opener.</summary>
    public bool IsForceClosed { get; set; }

    // --- Per-tender expected snapshots, filled at close (cash-only is reconciled;
    // these back the Z report's tender breakdown and a future blind/multi-tender count). ---
    public decimal ExpectedBank { get; set; }
    public decimal ExpectedWallet { get; set; }
    public decimal ExpectedCredit { get; set; }

    // --- Denormalised activity totals, filled at close for fast Z reprint
    // (reports remain recomputable from the ShiftId tags). ---
    public decimal SalesTotal { get; set; }
    public decimal RefundsTotal { get; set; }
}
