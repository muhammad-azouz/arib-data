using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AribONE.Models;
using AribONE.Repositories;

namespace AribONE.Services.Notifications;

/// <summary>
/// A self-contained notification source for one domain (inventory, expiry, …). Each
/// rule scans its own data and returns the <em>current</em> set of notifications that
/// should exist; the engine reconciles that against the store (upsert + auto-resolve).
/// Adding a notification type is just a new rule class registered once — the engine,
/// store and entity never change, and the engine never switches on notification type.
/// </summary>
public interface INotificationRule
{
    /// <summary>
    /// The <see cref="Entities.AppNotification.Type"/> codes this rule fully owns. The
    /// engine auto-resolves any active stored notification of these types that the
    /// latest <see cref="EvaluateAsync"/> no longer produced — so a rule must list every
    /// type it can emit, and two rules must not share a type.
    /// </summary>
    IReadOnlyCollection<string> OwnedTypes { get; }

    /// <summary>
    /// The single <see cref="NotificationCategories"/> bucket this rule belongs to. Lets an
    /// event-driven nudge re-run just the rules for an affected domain (e.g. a sale nudges
    /// "Inventory" + "Finance") instead of the whole registry. Each rule is single-category.
    /// </summary>
    string Category { get; }

    /// <summary>
    /// Scans the domain and returns the notifications that should currently be active.
    /// Read-only: never persists. SQL-side filtering only — never load whole tables.
    /// </summary>
    Task<IReadOnlyList<NotificationDraft>> EvaluateAsync(AribContext db, CancellationToken ct);
}
