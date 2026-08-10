using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AribONE.Models;
using AribONE.Models.Entities;

namespace AribONE.Services.Notifications;

/// <summary>
/// Persistence + reconciliation for notifications. The single write path the engine
/// uses; the UI reads/mutates the <c>AppNotifications</c> set directly (like other
/// view models). Kept narrow so the same engine can run against any node's DB.
/// </summary>
public interface INotificationStore
{
    /// <summary>
    /// Reconciles a rule's drafts with what is stored for its <paramref name="ownedTypes"/>:
    /// upsert each draft by <c>DedupKey</c> (insert new, or update an existing row in place
    /// and reactivate it if it had been resolved), then resolve every still-active owned
    /// row the drafts no longer contain.
    /// </summary>
    Task<ReconcileResult> ReconcileAsync(
        IReadOnlyCollection<string> ownedTypes,
        IReadOnlyList<NotificationDraft> drafts,
        CancellationToken ct = default);
}

/// <summary>Outcome of one reconcile: total rows changed (so the host can skip raising a
/// change event when nothing moved) plus the rows that just became newly active — a brand
/// new row, a resolved one reactivating, or a grouped row gaining a member. Recovery
/// (a member merely leaving, or an unchanged group) is deliberately excluded so a caller
/// that surfaces these as toasts doesn't re-alert on every scan.</summary>
public readonly record struct ReconcileResult(int Changed, IReadOnlyList<AppNotification> FreshlyAlerted);
