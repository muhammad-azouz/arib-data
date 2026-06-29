using System;

namespace AribONE.Models.Entities;

/// <summary>
/// A persisted, derived notification produced by an <c>INotificationRule</c> (low
/// stock, expiring batch, …). Deliberately generic so new notification types need no
/// schema change: the kind is a string <see cref="Type"/> code, not an enum, and the
/// engine never switches on it. Distinct from <c>Models/Notification.cs</c> (the
/// transient in-page toast).
///
/// Local-only: NOT in <c>SyncScope</c>. Notifications are derived from already-synced
/// source tables, so each node (branch app, future cloud/console worker) recomputes
/// its own against its own DB rather than syncing the rows.
///
/// Identity for dedup/reconciliation is <see cref="DedupKey"/> (unique): a rule re-run
/// upserts the same key instead of creating duplicates, and any previously-active row
/// of the rule's owned types no longer produced is auto-resolved.
/// </summary>
public class AppNotification
{
    public Guid Id { get; set; }

    /// <summary>Stable kind code, e.g. "Inventory.LowStock", "Expiry.Expired".</summary>
    public required string Type { get; set; }

    /// <summary>Coarse bucket for filtering/iconography: "Inventory", "Expiry", "Finance", "System"…</summary>
    public required string Category { get; set; }

    public NotificationSeverity Severity { get; set; }

    public required string Title { get; set; }
    public required string Message { get; set; }

    /// <summary>Deterministic business key; unique-indexed. Drives upsert + dedup.</summary>
    public required string DedupKey { get; set; }

    /// <summary>Deep-link target entity kind, e.g. "Warehouse", "Product". UI maps it to navigation.</summary>
    public string? ReferenceType { get; set; }
    public Guid? ReferenceId { get; set; }

    /// <summary>Set when this row summarizes many items ("23 products below minimum").</summary>
    public string? GroupKey { get; set; }
    public int Count { get; set; } = 1;

    /// <summary>Monotonic "alert generation". Bumped each time the condition re-alerts
    /// (reactivated from resolved, or a grouped notification gains a new member). Per-user
    /// read/dismiss is keyed to this value (<see cref="NotificationReadState"/>): bumping it
    /// re-surfaces the notification as unread for every user at once, with no per-user write.</summary>
    public int AlertSeq { get; set; } = 1;

    /// <summary>Optional JSON payload (e.g. the affected product/batch list for an expandable card).</summary>
    public string? Metadata { get; set; }

    /// <summary>Order-independent signature of the group's member items (see
    /// <c>NotificationDraft.MembersSignature</c>). Diffed on each scan: a new member appearing
    /// re-surfaces the notification (fresh event), a member leaving does not. Null for single items.</summary>
    public string? MembersSignature { get; set; }

    public bool IsResolved { get; set; }
    public DateTime? ResolvedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }

    public Guid BranchId { get; set; }
    public Guid? UserId { get; set; }
}

/// <summary>Notification importance. Higher values are more urgent; extendable later.</summary>
public enum NotificationSeverity
{
    Information = 0,
    Warning = 1,
    Critical = 2,
}
