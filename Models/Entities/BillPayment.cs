using System;

namespace AribONE.Models.Entities;

public class BillPayment
{
    public Guid Id { get; set; }
    public Guid BillId { get; set; }
    public Bill Bill { get; set; } = null!;

    public Guid CashRegNum { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }
    public Guid BranchId { get; set; }
}
