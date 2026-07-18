using System;

namespace AribONE.Models.Entities;

public class EWalletTransaction : IShiftScoped
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid EWalletId { get; set; }
    public EWallet EWallet { get; set; } = null!;
    public Guid? PartnerId { get; set; }
    public Partner? Partner { get; set; }
    public decimal Credit { get; set; }
    public decimal Debit { get; set; }
    public decimal Value { get; set; }
    public decimal Balance { get; set; }
    public Dealing Dealing { get; set; }
    public required string State { get; set; }
    public bool IsActive { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public string? Note { get; set; }
    public Guid RegNum { get; set; }
    public required string Pc { get; set; }
    public Guid BranchId { get; set; }
    public required string Ship { get; set; }

    /// <summary>Owning shift in Shift Mode; null in Open Safe mode.
    /// Stamped by ShiftIdInterceptor.</summary>
    public Guid? ShiftId { get; set; }
}
