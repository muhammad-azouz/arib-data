using System;
using System.ComponentModel.DataAnnotations;
using AribONE.Models;

namespace AribONE.Models.Entities;

/// <summary>
/// One audited field change on any document (tasks/spec-reservation-audit-management.md).
///
/// Deliberately <b>generic</b> — keyed by (<see cref="EntityType"/>, <see cref="EntityId"/>)
/// rather than by a reservation FK (D1). Reservations are the first consumer; invoices, orders,
/// products and price changes can write here later with no further migration, which matters
/// because every migration that touches a synced table is a fleet flag day (D11 of
/// tasks/spec-order-management.md).
///
/// <b>Append-only.</b> Nothing in the app may update or delete a row here.
///
/// <b>One row per changed field</b> (D6), not a JSON diff: the history grid renders straight
/// from these columns with no parser, and every old/new value stays queryable in SQL. Rows
/// written by a single user action share a <see cref="BatchId"/>.
///
/// <b>Deliberately does NOT implement <see cref="IShiftScoped"/></b> (D7), even though it
/// carries <see cref="ShiftId"/>. <c>ShiftIdInterceptor</c> keys on that interface, and when it
/// tags a row it re-validates the shift at commit time and <i>throws, rolling back the entire
/// SaveChanges</i>, if the shift closed underneath. An audit row must never be the thing that
/// fails someone else's write, so the id is stamped explicitly by <c>DocumentAuditService</c>
/// from <c>ShiftContext.Current.CurrentShiftId</c> instead.
/// </summary>
public class DocumentAuditEntry
{
    public Guid Id { get; set; }

    /// <summary>Groups the rows produced by one user action — e.g. changing a line's quantity
    /// and price in a single save writes two rows sharing this id.</summary>
    public Guid BatchId { get; set; }

    /// <summary>The CLR entity name of the row that changed: "Reservation" or
    /// "ReservationLine" today. Not an enum — a new consumer should cost no migration, which
    /// is the whole point of this table.</summary>
    [MaxLength(40)] public required string EntityType { get; set; }

    /// <summary>Id of the row that changed (the line, for a line-level change).</summary>
    public Guid EntityId { get; set; }

    /// <summary>Id of the owning document — the Reservation's id for both header and line
    /// changes. This is the column the history screen queries on, so a document's whole trail
    /// is one indexed seek rather than a union over line ids.</summary>
    public Guid DocumentId { get; set; }

    public AuditAction Action { get; set; }

    /// <summary>Property name for <see cref="AuditAction.Updated"/> ("Qty", "Price", "Note"…);
    /// null for the action-only kinds.</summary>
    [MaxLength(40)] public string? Field { get; set; }

    /// <summary>Values are formatted with <c>CultureInfo.InvariantCulture</c>, never the UI
    /// locale — a row written on an Arabic workstation must read identically on an English one
    /// and must round-trip back to the same decimal.</summary>
    [MaxLength(200)] public string? OldValue { get; set; }

    /// <inheritdoc cref="OldValue"/>
    [MaxLength(200)] public string? NewValue { get; set; }

    /// <summary>Free text the user typed in the confirm popup. Optional.</summary>
    [MaxLength(200)] public string? Reason { get; set; }

    /// <summary>Human label for the affected thing (the product name, for a reservation line),
    /// captured at write time so the trail stays readable after a product is renamed — the
    /// point of an audit row is what the user saw <i>then</i>, not what the FK resolves to now.</summary>
    [MaxLength(100)] public string? Subject { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    /// <summary>The sync filter column (D2/D8).</summary>
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    /// <summary>Plain nullable column, not an <see cref="IShiftScoped"/> tag — see the type
    /// remarks. Null in Open Safe mode and whenever no shift is open.</summary>
    public Guid? ShiftId { get; set; }

    public DateTime CreatedAt { get; set; }
}
