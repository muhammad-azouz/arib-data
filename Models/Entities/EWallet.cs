using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class EWallet
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    [Required] public required string Provider { get; set; }
    [Required] public required string Number { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal Balance { get; set; }
    public decimal OpenBalance { get; set; }
    public Guid AccountId { get; set; }
    public Account Account { get; set; } = null!;
    public Guid ComplementaryId { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
    public Guid? RegNum { get; set; }
}
