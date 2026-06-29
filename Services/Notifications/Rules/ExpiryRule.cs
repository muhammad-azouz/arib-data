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
/// Expiring-soon / expired notifications from the open <see cref="InventoryBatch"/>
/// cost-and-expiry layers (same source as the Expiry report). Emits one grouped
/// notification per (warehouse, condition) with the affected batches in <c>Metadata</c>.
/// Deep-links to the warehouse. The look-ahead window is fixed here for Phase 1; a user
/// preference is a later phase.
/// </summary>
public sealed class ExpiryRule : INotificationRule
{
    public IReadOnlyCollection<string> OwnedTypes { get; } =
    [
        NotificationTypes.ExpiryExpiring,
        NotificationTypes.ExpiryExpired,
    ];

    public string Category => NotificationCategories.Expiry;

    public async Task<IReadOnlyList<NotificationDraft>> EvaluateAsync(AribContext db, CancellationToken ct)
    {
        var today = DateTime.Today;
        // Days-ahead window is branch-configurable (NotificationSetting); default 30.
        var daysAhead = NotificationSettingsSnapshot.Current.ExpiryDaysAhead;
        // Exclusive upper bound = midnight after the last in-window day, so the whole
        // threshold day is included regardless of any time component (matches the report).
        var upperBound = today.AddDays(daysAhead + 1);

        var batches = await db.InventoryBatches
            .Where(b => b.RemainingQty > 0 && b.ExpiryDate != null && b.ExpiryDate < upperBound)
            .Select(b => new Row(
                b.WarehouseId,
                b.Warehouse.Name,
                b.ProductId,
                b.Product.Name,
                b.BatchNumber,
                b.ExpiryDate!.Value,
                b.RemainingQty))
            .ToListAsync(ct);

        var drafts = new List<NotificationDraft>();

        foreach (var grp in batches
                     .Select(b => (b, expired: b.ExpiryDate.Date < today))
                     .GroupBy(x => (x.b.WarehouseId, x.b.WarehouseName, x.expired)))
        {
            var (warehouseId, warehouseName, expired) = grp.Key;
            var items = grp
                .Select(x => x.b)
                .OrderBy(b => b.ExpiryDate)
                .ToList();

            drafts.Add(Build(expired, warehouseId, warehouseName, items, today, daysAhead));
        }

        return drafts;
    }

    private static NotificationDraft Build(
        bool expired, Guid warehouseId, string warehouseName, List<Row> items, DateTime today, int daysAhead)
    {
        var count = items.Count;
        var (type, severity, title, message) = expired
            ? (NotificationTypes.ExpiryExpired, NotificationSeverity.Critical,
               "أصناف منتهية الصلاحية", $"{count} تشغيلة منتهية الصلاحية في {warehouseName}")
            : (NotificationTypes.ExpiryExpiring, NotificationSeverity.Warning,
               "أصناف قاربت على الانتهاء", $"{count} تشغيلة تنتهي خلال {daysAhead} يوم في {warehouseName}");

        var metadata = JsonSerializer.Serialize(items
            .Take(50)
            .Select(b => new
            {
                b.ProductId,
                b.ProductName,
                b.BatchNumber,
                ExpiryDate = b.ExpiryDate,
                DaysRemaining = (int)(b.ExpiryDate.Date - today).TotalDays,
                Qty = b.RemainingQty,
            }));

        return new NotificationDraft
        {
            Type = type,
            Category = NotificationCategories.Expiry,
            Severity = severity,
            Title = title,
            Message = message,
            ReferenceType = "Warehouse",
            ReferenceId = warehouseId,
            GroupKey = $"exp:{type}:{warehouseId}",
            Count = count,
            Metadata = metadata,
            // Member identity = product + expiry day, so a newly-expiring batch re-alerts even
            // for a product already in the group.
            Members = items.Select(b => $"{b.ProductId}|{b.ExpiryDate:yyyyMMdd}").ToList(),
        };
    }

    private readonly record struct Row(
        Guid WarehouseId,
        string WarehouseName,
        Guid ProductId,
        string ProductName,
        string? BatchNumber,
        DateTime ExpiryDate,
        decimal RemainingQty);
}
