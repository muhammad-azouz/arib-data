using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AribONE.Models;

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
    /// row the drafts no longer contain. Returns the number of rows changed so the host can
    /// skip raising a change event when nothing moved.
    /// </summary>
    Task<int> ReconcileAsync(
        IReadOnlyCollection<string> ownedTypes,
        IReadOnlyList<NotificationDraft> drafts,
        CancellationToken ct = default);
}
