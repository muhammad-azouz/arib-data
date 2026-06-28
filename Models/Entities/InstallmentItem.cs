using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AribONE.Models.Entities;

public class InstallmentItem
{
    public Guid Id { get; set; }
    public Guid InstallmentPlanId { get; set; }
    public InstallmentPlan Plan { get; set; } = null!;

    public int Sequence { get; set; }
    public DateTime DueDate { get; set; }
    public decimal Amount { get; set; }
    public decimal PaidAmount { get; set; }

    [NotMapped] public decimal Remaining => Amount - PaidAmount;
    [NotMapped] public bool IsPaid => PaidAmount >= Amount;
    [NotMapped] public bool IsPartial => PaidAmount > 0 && PaidAmount < Amount;
    [NotMapped] public bool IsOverdue => !IsPaid && DueDate.Date < DateTime.Today;
    [NotMapped] public string StatusText =>
        IsPaid ? "مدفوع" :
        IsOverdue ? "متأخر" :
        IsPartial ? "جزئى" :
        "معلق";

    public DateTime CreatedAt { get; set; }
    public Guid BranchId { get; set; }

    public ICollection<InstallmentPayment> Payments { get; set; } = [];
}
