using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AribONE.Models;
using AribONE.Repositories;

namespace AribONE.Services.Notifications;

/// <summary>
/// Coordinates rule execution. It only runs each rule and hands its drafts to the
/// store to reconcile — it has no knowledge of any specific notification type, so new
/// rules never require an engine change. UI-free and node-agnostic: the branch app and
/// a future cloud/console worker share it, each against its own DB.
/// </summary>
public sealed class NotificationEngine
{
    private readonly INotificationStore _store;

    public NotificationEngine(INotificationStore store) => _store = store;

    /// <summary>Runs one rule and reconciles it. Returns rows changed. Never throws for a
    /// rule fault — a broken rule must not take down the others; the fault is returned.</summary>
    public async Task<RuleRunResult> RunRuleAsync(INotificationRule rule, CancellationToken ct = default)
    {
        try
        {
            // A disabled category reconciles with no drafts: the store auto-resolves any live
            // notifications of this rule's types (badge drops) and the evaluation query is
            // skipped entirely. Re-enabling re-creates them on the next pass.
            IReadOnlyList<NotificationDraft> drafts;
            if (NotificationSettingsSnapshot.Current.IsCategoryEnabled(rule.Category))
            {
                await using var db = new AribContext();
                drafts = await rule.EvaluateAsync(db, ct);
            }
            else
            {
                drafts = [];
            }

            var changed = await _store.ReconcileAsync(rule.OwnedTypes, drafts, ct);
            return new RuleRunResult(rule, changed, null);
        }
        catch (Exception ex)
        {
            return new RuleRunResult(rule, 0, ex);
        }
    }

    /// <summary>Runs all rules sequentially. Returns the total number of rows changed
    /// across rules (so the host can decide whether to notify the UI).</summary>
    public async Task<int> RunAsync(IEnumerable<INotificationRule> rules, CancellationToken ct = default)
    {
        var totalChanged = 0;
        foreach (var rule in rules)
        {
            ct.ThrowIfCancellationRequested();
            var result = await RunRuleAsync(rule, ct);
            totalChanged += result.Changed;
        }
        return totalChanged;
    }
}

/// <summary>Outcome of a single rule run: rows changed and the fault, if any.</summary>
public sealed record RuleRunResult(INotificationRule Rule, int Changed, Exception? Error);
