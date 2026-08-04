using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AribONE.Models.Entities;

public class ReservationLine : InvoiceLine
{
    public ICollection<ReservationFulfillment> Fulfillments { get; set; } = null!;

    [NotMapped]
    public decimal NetReleasedQty => Fulfillments
        ?.Sum(f => f.Type == FulfillmentType.Release ? f.Qty : -f.Qty) ?? 0;

    [NotMapped] public decimal RemainingQty => Qty - NetReleasedQty;
}
