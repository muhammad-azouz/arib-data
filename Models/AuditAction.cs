namespace AribONE.Models;

/// <summary>What happened to an audited row (tasks/spec-reservation-audit-management.md).
///
/// There is deliberately <b>no <c>Created</c> member</b> (D5): a document's creation is already
/// fully recorded by the document itself (<c>CreatedAt</c>/<c>UserId</c>), and synthesizing the
/// create event at read time instead of storing it is what lets the history screen work for
/// documents that pre-date this table — the alternative would show an empty timeline for every
/// row created before the feature shipped.</summary>
public enum AuditAction
{
    /// <summary>One field changed. <c>Field</c>/<c>OldValue</c>/<c>NewValue</c> are populated.</summary>
    Updated = 1,

    /// <summary>The document reached a cancelled/void terminal state.</summary>
    Cancelled = 2,

    /// <summary>A child line was appended to the document.</summary>
    LineAdded = 3,

    /// <summary>A child line was removed (soft delete, in the reservation case).</summary>
    LineDeleted = 4,

    /// <summary>An order left the branch for delivery (tasks/spec-delivery-couriers.md D7).
    /// <c>Subject</c> is the dispatching courier's name.</summary>
    Dispatched = 5,

    /// <summary>An out-for-delivery order's courier was swapped without a failed attempt (D2/D7).
    /// <c>Subject</c> is the newly-assigned courier's name.</summary>
    Reassigned = 6,

    /// <summary>A failed delivery attempt came back to the branch (D6/D7). <c>Subject</c> is the
    /// courier who was carrying it; <c>Reason</c> is the failure reason.</summary>
    ReturnedToBranch = 7,
}
