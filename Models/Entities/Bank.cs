using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class Bank
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Branch { get; set; }
    [Required] public string AccountNum { get; set; }
    public decimal Credit { get; set; }
    public decimal Debit { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Address { get; set; }
    public string Phone1 { get; set; }
    public string Phone2 { get; set; }
    public string Phone3 { get; set; }
    public string WebSite { get; set; }
    public string Mail { get; set; }
    public bool IsActive { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid AccountId { get; set; }
    public Account Account { get; set; }

    public Guid CurrencyId { get; set; }
    public Currency Currency { get; set; }
    public decimal Value { get; set; }

    public Guid ToId { get; set; }
    public Guid? RegNum { get; set; }
    public decimal OpenBalance { get; set; }
    public decimal Total { get; set; }
}
