namespace AribONE.Models.Entities;

/// <summary>Lifecycle state of a <see cref="Shift"/>. Shifts are never deleted.</summary>
public enum ShiftStatus
{
    Open = 0,
    Closed = 1,
}
