using System;

namespace AribONE.Models.Entities;

public class InstallmentPayment
{
    public Guid Id { get; set; }
    public Guid InstallmentItemId { get; set; }
    public InstallmentItem InstallmentItem { get; set; } = null!;

    public Guid CashRegNum { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }
    public Guid BranchId { get; set; }
}
