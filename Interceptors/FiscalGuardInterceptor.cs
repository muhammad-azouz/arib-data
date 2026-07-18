using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AribONE.Models;
using AribONE.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AribONE.Interceptors;

/// <summary>
/// Gates every Added/Modified/Deleted <see cref="GeneralLedgerEntry"/> and
/// <see cref="Invoice"/> row against the fiscal-year calendar (roadmap: Fiscal Year).
/// A row dated inside a Closed year — or before the calendar's first year — throws
/// <see cref="FiscalYearClosedException"/>, aborting the whole SaveChanges (the same
/// contract as <see cref="ShiftIdInterceptor"/>'s commit-time guard). Rows whose
/// <c>Dealing == Dealing.YearClose</c> (the closing/reversal batch itself) always bypass.
///
/// No-op when <see cref="Repositories.AribContext.FiscalCalendarProvider"/> is null or
/// returns an empty list — the pre-setup window before the first
/// <c>FiscalYearService.EnsureCalendarAsync</c> backfill, exactly like <c>ShiftGate</c>
/// in Open Safe mode. Dotmim.Sync applies rows via its own ADO.NET pipeline, never
/// through <see cref="Repositories.AribContext.SaveChanges"/>, so synced rows never
/// reach this interceptor.
/// </summary>
public sealed class FiscalGuardInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        if (eventData.Context is { } ctx) Check(ctx);
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is { } ctx) await CheckAsync(ctx, cancellationToken);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void Check(DbContext ctx)
    {
        var calendar = Repositories.AribContext.FiscalCalendarProvider?.Invoke();
        if (calendar is null || calendar.Count == 0)
            return; // pre-setup — no calendar exists yet

        foreach (var date in DatesToGate(ctx))
        {
            var year = FindInCalendar(calendar, date)
                       ?? Repositories.AribContext.FiscalYearResolver?.Invoke(date);
            ThrowIfBlocked(year);
        }
    }

    private static async Task CheckAsync(DbContext ctx, CancellationToken ct)
    {
        var calendar = Repositories.AribContext.FiscalCalendarProvider?.Invoke();
        if (calendar is null || calendar.Count == 0)
            return; // pre-setup — no calendar exists yet

        foreach (var date in DatesToGate(ctx))
        {
            var cached = FindInCalendar(calendar, date);
            var year = cached ?? (Repositories.AribContext.FiscalYearResolverAsync is { } resolve
                ? await resolve(date, ct)
                : null);
            ThrowIfBlocked(year);
        }
    }

    /// <summary>Null (date before the calendar's first year, or before any year — the
    /// resolver only returns null in that case) and Closed both block the save.</summary>
    private static void ThrowIfBlocked(FiscalYear? year)
    {
        if (year is null)
            throw FiscalYearClosedException.ForDateBeforeCalendar();
        if (year.Status == FiscalYearStatus.Closed)
            throw FiscalYearClosedException.ForClosedYear();
    }

    private static IEnumerable<DateTime> DatesToGate(DbContext ctx)
    {
        foreach (var entry in ctx.ChangeTracker.Entries<GeneralLedgerEntry>())
        {
            if (entry.State is not (EntityState.Added or EntityState.Modified or EntityState.Deleted))
                continue;
            if (entry.Entity.Dealing == Dealing.YearClose)
                continue;
            yield return entry.Entity.CreatedAt;
        }

        foreach (var entry in ctx.ChangeTracker.Entries<Invoice>())
        {
            if (entry.State is not (EntityState.Added or EntityState.Modified or EntityState.Deleted))
                continue;
            yield return entry.Entity.CreatedAt;
        }
    }

    private static FiscalYear? FindInCalendar(IReadOnlyList<FiscalYear> calendar, DateTime date)
    {
        var d = date.Date;
        return calendar.FirstOrDefault(y => d >= y.StartDate.Date && d <= y.EndDate.Date);
    }
}
