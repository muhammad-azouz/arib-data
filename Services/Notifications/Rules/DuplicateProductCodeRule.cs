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
/// Active products whose <see cref="Product.ProductCode"/> is shared with at least one
/// other active product. A duplicate code causes silent mis-scans and wrong stock movements,
/// so this fires at Critical severity. One grouped notification per branch; member identity
/// is the product id, so a newly-duplicated product re-alerts.
/// </summary>
public sealed class DuplicateProductCodeRule : INotificationRule
{
    public IReadOnlyCollection<string> OwnedTypes { get; } =
    [
        NotificationTypes.InventoryDuplicateCode,
    ];

    public string Category => NotificationCategories.Inventory;

    public async Task<IReadOnlyList<NotificationDraft>> EvaluateAsync(AribContext db, CancellationToken ct)
    {
        var branchId = AribContext.BranchIdProvider?.Invoke() ?? Guid.Empty;

        // Two-step: GroupBy + SelectMany after a Where doesn't always translate to SQL.
        // Step 1: find which codes are duplicated.
        var dupCodes = await db.Products
            .Where(p => p.IsActive && p.ProductCode > 0)
            .GroupBy(p => p.ProductCode)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToListAsync(ct);

        if (dupCodes.Count == 0)
            return [];

        // Step 2: fetch all products that carry one of those codes.
        var duplicates = await db.Products
            .Where(p => p.IsActive && dupCodes.Contains(p.ProductCode))
            .Select(p => new { p.Id, p.Name, p.ProductCode })
            .OrderBy(p => p.ProductCode)
            .ThenBy(p => p.Name)
            .ToListAsync(ct);

        var metadata = JsonSerializer.Serialize(duplicates
            .Take(50)
            .Select(p => new { p.ProductCode, ProductName = p.Name }));

        return
        [
            new NotificationDraft
            {
                Type = NotificationTypes.InventoryDuplicateCode,
                Category = NotificationCategories.Inventory,
                Severity = NotificationSeverity.Critical,
                Title = "تكرار رقم الصنف",
                Message = $"{duplicates.Count} منتج يشترك في رقم صنف مكرر",
                ReferenceType = "Product",
                GroupKey = $"inv:dupcode:{branchId}",
                Count = duplicates.Count,
                Metadata = metadata,
                Members = duplicates.Select(p => p.Id.ToString()).ToList(),
            },
        ];
    }
}
