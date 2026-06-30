using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AribONE.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AribONE.Interceptors;

/// <summary>
/// Stamps the ambient open shift's id on every Added <see cref="IShiftScoped"/> row
/// that has not already had one set, and — when it actually tagged any row —
/// re-validates at commit time that the shift is still open. The host sets
/// <see cref="AribONE.Repositories.AribContext.ShiftIdProvider"/> once at startup
/// (<c>() =&gt; ShiftContext.Current?.CurrentShiftId</c>).
///
/// In Open Safe mode — or when no shift is open on this workstation — the provider
/// returns null and this is a no-op, so anchor rows keep a null ShiftId exactly as
/// before. When the provider itself is null (gateway, design-time) it is also a
/// no-op. Mirrors <see cref="BranchIdInterceptor"/>.
///
/// The commit-time guard closes the in-flight race: a cashier may have opened a money
/// flow while the shift was open, but if a supervisor force-closed it (or a duplicate
/// workstation id closed it) before Save, this aborts the whole SaveChanges so no row
/// is ever tagged to a closed shift. It runs only when at least one row was tagged
/// (i.e. in Shift Mode with an open ambient shift), so normal Open Safe writes pay
/// nothing.
/// </summary>
public sealed class ShiftIdInterceptor : SaveChangesInterceptor
{
    private static int StampShiftId(DbContext context, Guid shiftId)
    {
        var stamped = 0;
        foreach (var entry in context.ChangeTracker.Entries<IShiftScoped>())
        {
            if (entry.State != EntityState.Added)
                continue;

            if (entry.Entity.ShiftId is null)
            {
                entry.Entity.ShiftId = shiftId;
                stamped++;
            }
        }

        return stamped;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        if (eventData.Context is { } ctx && TryStamp(ctx, out var shiftId) && !IsShiftOpen(shiftId))
            AbortClosedShift();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is { } ctx && TryStamp(ctx, out var shiftId)
            && !await IsShiftOpenAsync(shiftId, cancellationToken))
            AbortClosedShift();
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    /// <summary>Stamps the ambient shift on Added rows; returns true (with the id) when
    /// at least one row was tagged and therefore needs the commit-time open check.</summary>
    private static bool TryStamp(DbContext ctx, out Guid shiftId)
    {
        shiftId = Guid.Empty;
        var provider = Repositories.AribContext.ShiftIdProvider;
        if (provider is null)
            return false;

        var current = provider();
        if (current is null || current == Guid.Empty)
            return false;

        shiftId = current.Value;
        return StampShiftId(ctx, shiftId) > 0;
    }

    private static bool IsShiftOpen(Guid shiftId)
    {
        using var db = new Repositories.AribContext();
        return db.Shifts.AsNoTracking()
            .Where(s => s.Id == shiftId)
            .Select(s => (int?)s.Status)
            .FirstOrDefault() == (int)ShiftStatus.Open;
    }

    private static async Task<bool> IsShiftOpenAsync(Guid shiftId, CancellationToken ct)
    {
        await using var db = new Repositories.AribContext();
        return await db.Shifts.AsNoTracking()
            .Where(s => s.Id == shiftId)
            .Select(s => (int?)s.Status)
            .FirstOrDefaultAsync(ct) == (int)ShiftStatus.Open;
    }

    private static void AbortClosedShift()
    {
        // Let the host drop the stale ShiftContext so the gate blocks and the banner
        // updates, then abort the save — nothing gets written to a closed shift.
        Repositories.AribContext.ShiftClosedCallback?.Invoke();
        throw new InvalidOperationException(
            "تم إغلاق الوردية من جهاز آخر. لا يمكن حفظ العملية على وردية مغلقة. افتح وردية جديدة.");
    }
}
