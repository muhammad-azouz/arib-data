using System;

namespace AribONE.Models.Entities;

/// <summary>
/// Marker for "origin" financial rows that a Shift owns when Shift Mode is on.
/// <see cref="AribONE.Interceptors.ShiftIdInterceptor"/> stamps
/// <see cref="ShiftId"/> from the ambient open shift on every Added row whose
/// value is still null. In Open Safe mode the provider returns null and the
/// column stays null — identical to pre-shift behaviour.
///
/// Derived/double-entry rows (GeneralLedgerEntry, InventoryMovement, InventoryBatch)
/// are deliberately NOT shift-scoped; reports join them back through RegNum/InvoiceId
/// to avoid duplicating shift attribution.
/// </summary>
public interface IShiftScoped
{
    Guid? ShiftId { get; set; }
}
