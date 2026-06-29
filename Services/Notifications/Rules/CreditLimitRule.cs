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
/// Customers whose outstanding balance has passed their credit limit. Balance is positive
/// when the customer owes the business, so "exceeded" = credit customer with a limit set
/// and <c>Balance &gt; CreditLimit</c>. One grouped notification per branch; member identity
/// is the customer id, so a newly-over-limit customer re-alerts. Deep-links to customers.
/// </summary>
public sealed class CreditLimitRule : INotificationRule
{
    public IReadOnlyCollection<string> OwnedTypes { get; } =
    [
        NotificationTypes.FinanceCreditLimit,
    ];

    public string Category => NotificationCategories.Finance;

    public async Task<IReadOnlyList<NotificationDraft>> EvaluateAsync(AribContext db, CancellationToken ct)
    {
        var branchId = AribContext.BranchIdProvider?.Invoke() ?? Guid.Empty;

        var rows = await db.Customers
            .Where(c => c.BranchId == branchId
                        && c.IsActive
                        && c.IsCredit
                        && c.Type == CustomerType.Customer
                        && c.CreditLimit > 0
                        && c.Balance > c.CreditLimit)
            .Select(c => new Row(c.Id, c.Name, c.Balance, c.CreditLimit))
            .ToListAsync(ct);

        if (rows.Count == 0)
            return [];

        var metadata = JsonSerializer.Serialize(rows
            .OrderByDescending(r => r.Balance - r.CreditLimit)
            .Take(50)
            .Select(r => new { r.CustomerName, r.Balance, r.CreditLimit, Over = r.Balance - r.CreditLimit }));

        return
        [
            new NotificationDraft
            {
                Type = NotificationTypes.FinanceCreditLimit,
                Category = NotificationCategories.Finance,
                Severity = NotificationSeverity.Warning,
                Title = "تجاوز حد الائتمان",
                Message = $"{rows.Count} عميل تجاوز حد الائتمان المسموح",
                ReferenceType = "Customer",
                GroupKey = $"fin:creditlimit:{branchId}",
                Count = rows.Count,
                Metadata = metadata,
                Members = rows.Select(r => r.CustomerId.ToString()).ToList(),
            },
        ];
    }

    private readonly record struct Row(Guid CustomerId, string CustomerName, decimal Balance, decimal CreditLimit);
}
