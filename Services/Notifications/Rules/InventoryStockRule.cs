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
/// Low-stock / out-of-stock / negative-inventory notifications from
/// <see cref="WarehouseProductInventory"/> vs <see cref="Product.ReOrder"/>. Emits one
/// grouped notification per (warehouse, condition) — "N products below minimum" — with
/// the affected items in <c>Metadata</c> for an expandable card, rather than one row per
/// product. Deep-links to the warehouse.
/// </summary>
public sealed class InventoryStockRule : INotificationRule
{
    public IReadOnlyCollection<string> OwnedTypes { get; } =
    [
        NotificationTypes.InventoryLowStock,
        NotificationTypes.InventoryOutOfStock,
        NotificationTypes.InventoryNegative,
    ];

    public string Category => NotificationCategories.Inventory;

    private enum Condition { Negative, OutOfStock, LowStock }

    public async Task<IReadOnlyList<NotificationDraft>> EvaluateAsync(AribContext db, CancellationToken ct)
    {
        // SQL-side filter so a large catalog never materializes its well-stocked rows:
        // out/negative, or at/under the per-product reorder level. The threshold compares
        // via float (ReOrder is double) which EF translates to CAST(TotalQty AS float);
        // Classify re-checks in memory to assign the exact condition.
        var rows = await db.WarehousesProductInventories
            .Where(w => w.Product.IsActive
                        && (w.TotalQty <= 0m || (double)w.TotalQty <= w.Product.ReOrder))
            .Select(w => new Row(
                w.WarehouseId,
                w.Warehouse.Name,
                w.ProductId,
                w.Product.Name,
                w.TotalQty,
                w.Product.ReOrder))
            .ToListAsync(ct);

        var drafts = new List<NotificationDraft>();

        foreach (var grp in rows
                     .Select(r => (r, c: Classify(r)))
                     .Where(x => x.c is not null)
                     .GroupBy(x => (x.r.WarehouseId, x.r.WarehouseName, Condition: x.c!.Value)))
        {
            var (warehouseId, warehouseName, condition) = grp.Key;
            var items = grp
                .Select(x => x.r)
                .OrderBy(r => r.TotalQty)
                .ToList();

            drafts.Add(Build(condition, warehouseId, warehouseName, items));
        }

        return drafts;
    }

    private static Condition? Classify(Row r)
    {
        if (r.TotalQty < 0m) return Condition.Negative;
        if (r.TotalQty == 0m) return Condition.OutOfStock;
        if (r.ReOrder > 0 && (double)r.TotalQty <= r.ReOrder) return Condition.LowStock;
        return null;
    }

    private static NotificationDraft Build(
        Condition condition, Guid warehouseId, string warehouseName, List<Row> items)
    {
        var count = items.Count;
        var (type, severity, title, message) = condition switch
        {
            Condition.Negative => (
                NotificationTypes.InventoryNegative, NotificationSeverity.Critical,
                "مخزون سالب", $"{count} منتج برصيد سالب في {warehouseName}"),
            Condition.OutOfStock => (
                NotificationTypes.InventoryOutOfStock, NotificationSeverity.Critical,
                "نفاد المخزون", $"{count} منتج نفد من مخزن {warehouseName}"),
            _ => (
                NotificationTypes.InventoryLowStock, NotificationSeverity.Warning,
                "انخفاض المخزون", $"{count} منتج تحت حد إعادة الطلب في {warehouseName}"),
        };

        var metadata = JsonSerializer.Serialize(items
            .Take(50)
            .Select(r => new { r.ProductId, r.ProductName, Qty = r.TotalQty, r.ReOrder }));

        return new NotificationDraft
        {
            Type = type,
            Category = NotificationCategories.Inventory,
            Severity = severity,
            Title = title,
            Message = message,
            ReferenceType = "Warehouse",
            ReferenceId = warehouseId,
            GroupKey = $"inv:{type}:{warehouseId}",
            Count = count,
            Metadata = metadata,
            // Member identity = the affected products, so a newly-negative product re-alerts.
            Members = items.Select(r => r.ProductId.ToString()).ToList(),
        };
    }

    private readonly record struct Row(
        Guid WarehouseId,
        string WarehouseName,
        Guid ProductId,
        string ProductName,
        decimal TotalQty,
        double ReOrder);
}
