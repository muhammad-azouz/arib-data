using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AribONE.Models;

namespace AribONE.Models.Entities;

/// <summary>A prep-counter ticket printed by an AribLink terminal (tasks/spec-ariblink-gateway.md
/// D4) — a customer's order at a section (butchery, bakery, west counter), scanned into a sale at
/// the till. Deliberately its own table, not an <see cref="Invoice"/> TPH subtype or the existing
/// <see cref="Order"/>: it posts <b>no</b> GL entry, inventory movement, treasury row or partner
/// ledger row — it becomes money only when the cashier finalises the sale it is redeemed into
/// (D12), tracked here by the scalar <see cref="SaleId"/>. Does <b>not</b> implement
/// <see cref="IShiftScoped"/> (D4) — a ticket moves no cash, same reasoning as <see cref="Order"/>'s
/// D13; a ticket created and redeemed across a shift close/open must still work (D9 in the client
/// spec, spec verify note).
///
/// Neither this table nor <see cref="TerminalOrderLine"/> joins <c>SyncScope</c> — an unredeemed
/// ticket is operational state of one branch's prep counters and never reaches central (D7).
/// <see cref="TerminalId"/> and <see cref="SectionId"/>/<see cref="SectionName"/> are therefore
/// plain opaque strings with no FK, matching §8.9 of the client contract exactly.</summary>
public class TerminalOrder
{
    public Guid Id { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    /// <summary><c>{tag}-{yyMMdd}-{NNNN}</c> (D5) — unique per branch, Code-128-safe ASCII.</summary>
    [MaxLength(24)] public required string TicketCode { get; set; }

    public TerminalOrderStatus Status { get; set; }

    /// <summary>A slug of the terminal's own requested section (D3) — no FK, no master table.</summary>
    [MaxLength(64)] public required string SectionId { get; set; }
    [MaxLength(100)] public required string SectionName { get; set; }

    /// <summary>The terminal that created this ticket — opaque, no FK (D7: the seat registry is a
    /// machine-local file, never a table).</summary>
    [MaxLength(64)] public required string TerminalId { get; set; }

    /// <summary>Idempotency key from the terminal, unique together with <see cref="TerminalId"/> —
    /// the replay log <b>is</b> the ticket itself (spec "Idempotency"), not a second store.</summary>
    [MaxLength(64)] public required string ClientRequestId { get; set; }

    [MaxLength(500)] public string? Notes { get; set; }

    public Guid CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; } = null!;

    public int ItemCount { get; set; }
    public decimal TotalAmount { get; set; }

    /// <summary>The sale this ticket was redeemed into (D12). Scalar audit pointer, deliberately no
    /// FK/navigation — the same posture <see cref="Order.SaleId"/> takes.</summary>
    public Guid? SaleId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<TerminalOrderLine> Lines { get; set; } = [];
}
