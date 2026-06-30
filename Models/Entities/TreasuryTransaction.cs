using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class TreasuryTransaction : IShiftScoped
{
    public Guid Id { get; set; }
    public Guid TreasuryId { get; set; }
    public Treasury Treasury { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int Num { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal Balance { get; set; }
    public required string Dealing { get; set; }
    public Dealing Deal { get; set; }
    [MaxLength(300)] public string? Note { get; set; }
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
    public required string Ship { get; set; }
    public decimal Total { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid RegNum { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    /// <summary>Owning shift in Shift Mode; null in Open Safe mode. Stamped by
    /// ShiftIdInterceptor. Cash reconciliation sums these per shift.</summary>
    public Guid? ShiftId { get; set; }
}
