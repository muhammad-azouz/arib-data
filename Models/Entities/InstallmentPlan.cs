using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class InstallmentPlan
{
    public Guid Id { get; set; }
    public int Num { get; set; }

    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public Guid? SourceBillId { get; set; }

    public decimal Principal { get; set; }
    public int Count { get; set; }
    public DateTime StartDate { get; set; }
    public int IntervalMonths { get; set; } = 1;

    public InstallmentRoundingMode RoundingMode { get; set; } = InstallmentRoundingMode.Rounded;
    public decimal RoundingStep { get; set; } = 100m;
    public InstallmentRemainderTarget RemainderTarget { get; set; } = InstallmentRemainderTarget.Last;

    public InstallmentPlanStatus Status { get; set; } = InstallmentPlanStatus.Active;

    [MaxLength(500)] public string? Note { get; set; }

    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public Guid BranchId { get; set; }

    public ICollection<InstallmentItem> Installments { get; set; } = [];
}
