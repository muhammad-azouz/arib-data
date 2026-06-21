using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class Treasury
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [Required] [StringLength(50)] public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsDefault { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal Balance { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    public Guid AccountId { get; set; }
    public decimal OpeningBalance { get; set; }
    public Guid CloseAccountId { get; set; }
    [StringLength(200)] public string? Note { get; set; }
}
