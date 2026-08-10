using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AribONE.Models;
using AribONE.Models.Entities;
using AribONE.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AribONE.Services.Notifications.Rules;

/// <summary>
/// Branch-facing alert for an order that landed here and is still sitting unactioned
/// (tasks/spec-order-management.md D10) — without it, a phoned-in/console order is
/// invisible unless someone happens to be on the orders screen. One grouped notification
/// per branch: an order joining the New set is a fresh member (re-alerts, per
/// EfNotificationStore's member-diff), an order leaving New (accepted/cancelled/delivered)
/// just shrinks the group quietly — no re-alert. Bounded to a recent window, like
/// ShiftRule, so a stale abandoned order doesn't keep alerting forever.
/// </summary>
public sealed class OrderRule : INotificationRule
{
    /// <summary>Only orders created within this window are surfaced.</summary>
    private static readonly TimeSpan RecentWindow = TimeSpan.FromDays(2);

    public IReadOnlyCollection<string> OwnedTypes { get; } = [NotificationTypes.SalesNewOrder];

    public string Category => NotificationCategories.Sales;

    public async Task<IReadOnlyList<NotificationDraft>> EvaluateAsync(AribContext db, CancellationToken ct)
    {
        var branchId = AribContext.BranchIdProvider?.Invoke() ?? Guid.Empty;
        var since = DateTime.Now - RecentWindow;

        var pending = await db.Orders
            .Where(o => o.BranchId == branchId && o.Status == OrderStatus.New && o.CreatedAt >= since)
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => new Row(o.Id, o.Ref))
            .ToListAsync(ct);

        if (pending.Count == 0) return [];

        var metadata = JsonSerializer.Serialize(pending.Take(50).Select(r => new { r.Ref }));

        return
        [
            new NotificationDraft
            {
                Type = NotificationTypes.SalesNewOrder,
                Category = NotificationCategories.Sales,
                Severity = NotificationSeverity.Information,
                Title = "طلبيات جديدة",
                Message = pending.Count == 1
                    ? $"طلب جديد بانتظار المراجعة ({pending[0].Ref})"
                    : $"{pending.Count} طلبات جديدة بانتظار المراجعة",
                ReferenceType = "Order",
                GroupKey = $"sales:order:new:{branchId}",
                Count = pending.Count,
                Metadata = metadata,
                Members = pending.Select(r => r.Id.ToString()).ToList(),
            }
        ];
    }

    private readonly record struct Row(Guid Id, string Ref);
}
