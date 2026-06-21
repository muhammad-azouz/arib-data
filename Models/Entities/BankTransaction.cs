using System;

namespace AribONE.Models.Entities;

public class BankTransaction
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid BankId { get; set; }
    public Bank Bank { get; set; } = null!;
    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }
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
    public Guid CurrencyId { get; set; }
    public Currency Currency { get; set; } = null!;
    public decimal CurrencyVal { get; set; }
    public Guid BranchId { get; set; }
    public required string Ship { get; set; }
}
