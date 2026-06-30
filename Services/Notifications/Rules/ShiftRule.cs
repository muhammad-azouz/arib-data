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
/// Manager-facing shift alerts: recently-closed shifts that came up over/short beyond
/// a threshold, and shifts closed by a supervisor (force close). Bounded to a recent
/// window so old discrepancies auto-resolve out of the drawer once they age past it.
/// Lives in AribONE.Data so the console can reuse the same engine.
/// </summary>
public sealed class ShiftRule : INotificationRule
{
    /// <summary>Over/short magnitude (absolute) at or above which a shift alerts.
    /// Kept here as a constant for now; can move to NotificationSettings later.</summary>
    private const decimal OverShortThreshold = 1m;

    /// <summary>Only shifts closed within this window are surfaced.</summary>
    private static readonly TimeSpan RecentWindow = TimeSpan.FromDays(2);

    public IReadOnlyCollection<string> OwnedTypes { get; } =
    [
        NotificationTypes.FinanceShiftOverShort,
        NotificationTypes.FinanceShiftForceClosed,
    ];

    public string Category => NotificationCategories.Finance;

    public async Task<IReadOnlyList<NotificationDraft>> EvaluateAsync(AribContext db, CancellationToken ct)
    {
        var branchId = AribContext.BranchIdProvider?.Invoke() ?? Guid.Empty;
        var since = DateTime.Now - RecentWindow;

        var recent = await db.Shifts
            .Where(s => s.BranchId == branchId
                        && s.Status == ShiftStatus.Closed
                        && s.ClosedAt != null && s.ClosedAt >= since)
            .Select(s => new Row(s.Id, s.Num, s.Difference, s.IsForceClosed))
            .ToListAsync(ct);

        var drafts = new List<NotificationDraft>();

        var overShort = recent.Where(r => Math.Abs(r.Difference) >= OverShortThreshold).ToList();
        if (overShort.Count > 0)
        {
            var metadata = JsonSerializer.Serialize(overShort
                .OrderByDescending(r => Math.Abs(r.Difference))
                .Take(50)
                .Select(r => new { r.Num, r.Difference }));
            drafts.Add(new NotificationDraft
            {
                Type = NotificationTypes.FinanceShiftOverShort,
                Category = NotificationCategories.Finance,
                Severity = NotificationSeverity.Warning,
                Title = "فروقات في الورديات",
                Message = $"{overShort.Count} وردية بها زيادة أو عجز نقدي",
                ReferenceType = "Shift",
                GroupKey = $"fin:shift:overshort:{branchId}",
                Count = overShort.Count,
                Metadata = metadata,
                Members = overShort.Select(r => r.Id.ToString()).ToList(),
            });
        }

        var forced = recent.Where(r => r.IsForceClosed).ToList();
        if (forced.Count > 0)
        {
            var metadata = JsonSerializer.Serialize(forced
                .OrderByDescending(r => r.Num).Take(50).Select(r => new { r.Num, r.Difference }));
            drafts.Add(new NotificationDraft
            {
                Type = NotificationTypes.FinanceShiftForceClosed,
                Category = NotificationCategories.Finance,
                Severity = NotificationSeverity.Warning,
                Title = "إغلاق إجباري للورديات",
                Message = $"{forced.Count} وردية تم إغلاقها إجبارياً",
                ReferenceType = "Shift",
                GroupKey = $"fin:shift:forced:{branchId}",
                Count = forced.Count,
                Metadata = metadata,
                Members = forced.Select(r => r.Id.ToString()).ToList(),
            });
        }

        return drafts;
    }

    private readonly record struct Row(Guid Id, int Num, decimal Difference, bool IsForceClosed);
}
