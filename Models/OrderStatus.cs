namespace AribONE.Models;

/// <summary>The call-centre/branch order status machine (tasks/spec-order-management.md D3).
/// Forward-only except <c>OutForDelivery → Ready</c> (a failed delivery, D15).
/// <see cref="Delivered"/> is a consequence of saving the Sale (D6), never set by hand;
/// <see cref="Transferred"/> is a consequence of a transfer (D7), never set by hand.</summary>
public enum OrderStatus
{
    New = 0,
    Preparing = 1,
    Ready = 2,
    OutForDelivery = 3,
    Delivered = 4,
    Cancelled = 5,
    Transferred = 6,
}
