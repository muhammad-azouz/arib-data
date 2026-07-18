using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class PartnerLedgerEntry : IShiftScoped
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? PartnerId { get; set; }
    public Partner? Partner { get; set; }

    public decimal Credit { get; set; }

    public decimal Debit { get; set; }

    public decimal Balance { get; set; }

    public Dealing Dealing { get; set; }

    public decimal Total { get; set; }

    public decimal Pay { get; set; }

    public decimal Remain { get; set; }

    public decimal Discount { get; set; }

    public decimal Extra { get; set; }

    public decimal Tax { get; set; }

    [MaxLength(1000)] public string? Note { get; set; }

    public Guid RegNum { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid BranchId { get; set; }

    /// <summary>Owning shift in Shift Mode; null in Open Safe mode. Stamped by
    /// ShiftIdInterceptor. Captures credit sales / on-account payments per shift.</summary>
    public Guid? ShiftId { get; set; }
}
