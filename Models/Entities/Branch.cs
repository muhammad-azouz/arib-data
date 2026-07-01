using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public sealed class Branch
{
    public Branch()
    {
        Warehouses = new HashSet<Warehouse>();
    }

    public Guid Id { get; set; }
    [MaxLength(100)] public required string Name { get; set; }
    [MaxLength(50)] public required string Phone1 { get; set; }
    [MaxLength(50)] public string? Phone2 { get; set; }
    [MaxLength(50)] public string? Phone3 { get; set; }
    [MaxLength(150)] public required string Address { get; set; }
    public bool IsActive { get; set; }

    /// <summary>When true this branch runs in Shift Mode (cashiers must open a shift
    /// before any money transaction, per-shift X/Z reconciliation). Default false =
    /// Open Safe mode = unchanged legacy behaviour. Per-branch because the branch is
    /// the sync node; stored in the DB (not preference.json).</summary>
    public bool ShiftModeEnabled { get; set; }

    /// <summary>When true, receipts recompute PreviousBalance/EndingBalance live
    /// from the customer ledger at print/reprint time instead of using the
    /// snapshot frozen on the Bill at finalize time. Default false = snapshot
    /// mode = frozen receipts. Per-branch, DB-resident like ShiftModeEnabled —
    /// not preference.json.</summary>
    public bool DynamicBalanceModeEnabled { get; set; }

    /// <summary>When true, receipts also print a live "Current Balance" field
    /// (Customer.Balance as of print time) under the customer-info section,
    /// independent of PreviousBalance/EndingBalance. Default false. Per-branch,
    /// DB-resident like ShiftModeEnabled.</summary>
    public bool ShowCurrentBalanceOnReceipt { get; set; }

    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public ICollection<Warehouse>? Warehouses { get; set; }
    public ICollection<Treasury>? Safes { get; set; }
}
