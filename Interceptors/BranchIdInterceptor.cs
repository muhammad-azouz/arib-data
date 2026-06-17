using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AribONE.Interceptors;

/// <summary>
/// Stamps BranchId on every Added entity that has a Guid BranchId column and
/// has not already had one set. The host sets <see cref="AribONE.Repositories.AribContext.BranchIdProvider"/>
/// once at startup; if the provider is null (gateway, design-time) this interceptor is a no-op.
/// </summary>
public sealed class BranchIdInterceptor : SaveChangesInterceptor
{
    private static void StampBranchId(DbContext context, Guid branchId)
    {
        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.State != EntityState.Added)
                continue;

            var prop = entry.Metadata.FindProperty("BranchId");
            if (prop is null || prop.ClrType != typeof(Guid))
                continue;

            var current = entry.Property(prop).CurrentValue;
            if (current is Guid g && g == Guid.Empty)
                entry.Property(prop).CurrentValue = branchId;
        }
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        if (eventData.Context is { } ctx)
            Stamp(ctx);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is { } ctx)
            Stamp(ctx);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void Stamp(DbContext ctx)
    {
        var provider = Repositories.AribContext.BranchIdProvider;
        if (provider is null)
            return;

        var branchId = provider();
        if (branchId == Guid.Empty)
            return;

        StampBranchId(ctx, branchId);
    }
}