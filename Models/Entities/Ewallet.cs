using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class EWallet
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    [Required] public string Provider { get; set; }
    [Required] public string Number { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal Balance { get; set; }
    public decimal OpenBalance { get; set; }
    public Guid AccountId { get; set; }
    public Account Account { get; set; }
    public Guid ComplementaryId { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; }
    public Guid? RegNum { get; set; }
}
