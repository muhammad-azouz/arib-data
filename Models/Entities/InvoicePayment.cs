using System;

namespace AribONE.Models.Entities;

public class InvoicePayment : IShiftScoped
{
    public Guid Id { get; set; }
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;

    public Guid CashRegNum { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }
    public Guid BranchId { get; set; }

    /// <summary>Owning shift in Shift Mode; null in Open Safe mode. Stamped by
    /// ShiftIdInterceptor. Lets "payments collected this shift" be a direct query
    /// regardless of the settling tender.</summary>
    public Guid? ShiftId { get; set; }
}
