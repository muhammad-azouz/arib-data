using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class GeneralLedgerEntry
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid AccountId { get; set; }
    public Account Account { get; set; } = null!;

    public Guid? PartnerId { get; set; }
    public Partner? Partner { get; set; }

    public decimal Debit { get; set; }

    public decimal Credit { get; set; }

    public decimal Balance { get; set; }

    [Required] public Dealing Dealing { get; set; }

    public Guid RegNum { get; set; }

    public bool Active { get; set; } = true;

    public bool IsDeleted { get; set; }

    [MaxLength(100)] public string Ship { get; set; } = string.Empty;

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
}
