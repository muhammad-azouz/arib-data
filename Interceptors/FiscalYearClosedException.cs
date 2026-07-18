using System;

namespace AribONE.Interceptors;

/// <summary>Thrown by <see cref="FiscalGuardInterceptor"/> when a save would post or
/// modify a GeneralLedgerEntry/Invoice row dated outside any Open fiscal year.</summary>
public sealed class FiscalYearClosedException : Exception
{
    private FiscalYearClosedException(string message) : base(message) { }

    public static FiscalYearClosedException ForClosedYear() =>
        new("لا يمكن إضافة أو تعديل حركة في سنة مالية مغلقة.");

    public static FiscalYearClosedException ForDateBeforeCalendar() =>
        new("تاريخ الحركة يسبق بداية السنوات المالية المسجلة.");
}
