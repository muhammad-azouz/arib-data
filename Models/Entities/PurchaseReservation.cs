namespace AribONE.Models.Entities;

/// <summary>
/// A supplier-side reservation — items parked to buy later (tasks/spec-purchase-reservation.md).
/// An empty subclass on purpose: <c>Invoice.Type</c> is the TPH discriminator, so a second
/// direction on the same <see cref="Reservation"/> shape needs its own discriminator value, not a
/// second <c>Type</c> on <see cref="Reservation"/>. Lines stay <see cref="ReservationLine"/> and
/// draw-downs stay <see cref="ReservationFulfillment"/>; readers pick a side with
/// <c>SaleSide()</c> / <c>PurchaseSide()</c>.
/// </summary>
public class PurchaseReservation : Reservation
{
}
