namespace AribONE.Models.Entities;

/// <summary>Lifecycle state of a <see cref="FiscalYear"/>. Years are never deleted.</summary>
public enum FiscalYearStatus
{
    Open = 0,
    Closed = 1,
}
