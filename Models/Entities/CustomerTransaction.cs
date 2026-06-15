using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class CustomerTransaction
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }

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
    public User User { get; set; }

    public Guid BranchId { get; set; }
}
