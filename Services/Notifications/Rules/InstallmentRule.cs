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
/// Overdue and soon-due installment notifications from active plans
/// (<see cref="InstallmentItem"/>: unpaid, past or near <c>DueDate</c>). One grouped
/// notification per condition for the current branch; member identity is the installment
/// id, so a newly-overdue installment re-alerts. Deep-links to the installments page.
/// </summary>
public sealed class InstallmentRule : INotificationRule
{
    public IReadOnlyCollection<string> OwnedTypes { get; } =
    [
        NotificationTypes.FinanceInstallmentOverdue,
        NotificationTypes.FinanceInstallmentDueSoon,
    ];

    public string Category => NotificationCategories.Finance;

    public async Task<IReadOnlyList<NotificationDraft>> EvaluateAsync(AribContext db, CancellationToken ct)
    {
        var branchId = AribContext.BranchIdProvider?.Invoke() ?? Guid.Empty;
        var today = DateTime.Today;
        // Due-soon window is branch-configurable (NotificationSetting); default 7.
        var dueSoonDays = NotificationSettingsSnapshot.Current.InstallmentDueSoonDays;
        var dueSoonUpper = today.AddDays(dueSoonDays + 1);

        var rows = await db.InstallmentItems
            .Where(i => i.BranchId == branchId
                        && i.Plan.Status == InstallmentPlanStatus.Active
                        && i.PaidAmount < i.Amount
                        && i.DueDate < dueSoonUpper)
            .Select(i => new Row(i.Id, i.Plan.Customer.Name, i.DueDate, i.Amount - i.PaidAmount))
            .ToListAsync(ct);

        var drafts = new List<NotificationDraft>();

        var overdue = rows.Where(r => r.DueDate.Date < today).ToList();
        if (overdue.Count > 0)
            drafts.Add(Build(overdue: true, branchId, overdue, today, dueSoonDays));

        var dueSoon = rows.Where(r => r.DueDate.Date >= today).ToList();
        if (dueSoon.Count > 0)
            drafts.Add(Build(overdue: false, branchId, dueSoon, today, dueSoonDays));

        return drafts;
    }

    private static NotificationDraft Build(bool overdue, Guid branchId, List<Row> items, DateTime today, int dueSoonDays)
    {
        var count = items.Count;
        var total = items.Sum(i => i.Remaining);
        var (type, severity, title, message) = overdue
            ? (NotificationTypes.FinanceInstallmentOverdue, NotificationSeverity.Critical,
               "أقساط متأخرة", $"{count} قسط متأخر السداد بإجمالي {total:0.##}")
            : (NotificationTypes.FinanceInstallmentDueSoon, NotificationSeverity.Warning,
               "أقساط مستحقة قريبًا", $"{count} قسط يستحق خلال {dueSoonDays} أيام");

        var metadata = JsonSerializer.Serialize(items
            .OrderBy(i => i.DueDate)
            .Take(50)
            .Select(i => new
            {
                i.CustomerName,
                DueDate = i.DueDate,
                DaysOverdue = (int)(today - i.DueDate.Date).TotalDays,
                Remaining = i.Remaining,
            }));

        return new NotificationDraft
        {
            Type = type,
            Category = NotificationCategories.Finance,
            Severity = severity,
            Title = title,
            Message = message,
            ReferenceType = "Installment",
            GroupKey = $"fin:installment:{type}:{branchId}",
            Count = count,
            Metadata = metadata,
            Members = items.Select(i => i.Id.ToString()).ToList(),
        };
    }

    private readonly record struct Row(Guid Id, string CustomerName, DateTime DueDate, decimal Remaining);
}
