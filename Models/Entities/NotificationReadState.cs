using System;

namespace AribONE.Models.Entities;

/// <summary>
/// Per-user read/dismiss state for an <see cref="AppNotification"/>. The notification row
/// is branch-shared (one DB serves every POS terminal in the branch), but read/dismiss is
/// per-user: acknowledging an alert on one terminal must not clear it for a different
/// cashier on another. One row per (notification, user).
///
/// Read/dismiss are keyed to the notification's <see cref="AppNotification.AlertSeq"/> at
/// the moment the user acted: the notification is "read for this user" only while
/// <see cref="ReadSeq"/> equals the current <c>AlertSeq</c>. When the condition re-alerts
/// (AlertSeq is bumped) every prior ack goes stale at once — the notification is unread for
/// everyone again without touching a single read-state row.
///
/// Local-only: NOT in <c>SyncScope</c> — like the notifications themselves, read state is
/// recomputed per node, never synced.
/// </summary>
public class NotificationReadState
{
    public Guid Id { get; set; }

    public Guid NotificationId { get; set; }
    public AppNotification? Notification { get; set; }

    /// <summary>The user whose read/dismiss state this row records.</summary>
    public Guid UserId { get; set; }

    /// <summary>The <see cref="AppNotification.AlertSeq"/> the user last marked read. The
    /// notification is read for this user only while this equals the current AlertSeq.</summary>
    public int? ReadSeq { get; set; }

    /// <summary>The <see cref="AppNotification.AlertSeq"/> the user last dismissed at. The
    /// notification is hidden for this user only while this equals the current AlertSeq.</summary>
    public int? DismissedSeq { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public Guid BranchId { get; set; }
}
