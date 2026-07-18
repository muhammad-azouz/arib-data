using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class BankAccount
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Branch { get; set; }
    [Required] public required string AccountNum { get; set; }
    public decimal Credit { get; set; }
    public decimal Debit { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string Address { get; set; }
    public required string Phone1 { get; set; }
    public required string Phone2 { get; set; }
    public required string Phone3 { get; set; }
    public required string WebSite { get; set; }
    public required string Mail { get; set; }
    public bool IsActive { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid AccountId { get; set; }
    public Account Account { get; set; } = null!;

    public Guid CurrencyId { get; set; }
    public Currency Currency { get; set; } = null!;
    public decimal Value { get; set; }

    public Guid ToId { get; set; }
    public Guid? RegNum { get; set; }
    public decimal OpenBalance { get; set; }
    public decimal Total { get; set; }
}
