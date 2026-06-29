using System;
using System.Collections.Generic;
using System.Linq;
using AribONE.Models.Entities;

namespace AribONE.Models;

/// <summary>
/// A rule's intent for a single notification in the current scan: the desired state,
/// before the engine reconciles it against what is already stored. The engine upserts
/// drafts by <see cref="DedupKey"/> and resolves anything no longer drafted.
/// Carries no persistence/read state — those are owned by the stored
/// <see cref="AppNotification"/>.
/// </summary>
public sealed record NotificationDraft
{
    public required string Type { get; init; }
    public required string Category { get; init; }
    public NotificationSeverity Severity { get; init; }
    public required string Title { get; init; }
    public required string Message { get; init; }

    public string? ReferenceType { get; init; }
    public Guid? ReferenceId { get; init; }

    public string? GroupKey { get; init; }
    public int Count { get; init; } = 1;

    public string? Metadata { get; init; }
    public DateTime? ExpiresAt { get; init; }

    /// <summary>
    /// Stable identity of each item in a grouped notification (e.g. the affected product
    /// ids). Lets the store tell a <em>new</em> member joining the group (re-alert) from a
    /// member merely leaving it (decrement quietly). Null for single-item notifications.
    /// </summary>
    public IReadOnlyCollection<string>? Members { get; init; }

    /// <summary>Order-independent signature of <see cref="Members"/>, persisted so the next
    /// scan can diff membership. Null when the draft has no members.</summary>
    public string? MembersSignature => Members is null
        ? null
        : string.Join(",", Members.OrderBy(m => m, StringComparer.Ordinal));

    /// <summary>
    /// Deterministic identity. Defaults to <c>Type|ReferenceType|ReferenceId</c>, or the
    /// explicit <see cref="GroupKey"/> for grouped rows. Stable across scans so re-runs
    /// upsert the same stored row instead of duplicating.
    /// </summary>
    public string DedupKey => GroupKey ?? $"{Type}|{ReferenceType}|{ReferenceId}";
}
