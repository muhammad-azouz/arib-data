using System;

namespace AribONE.Models.Entities;

public enum CashType
{
    Receive,
    Pay
}

public enum PaymentMethod
{
    Cash,
    EWallet,
    BankTransfer,
}

public class Cash
{
    public Guid Id { get; set; }
    public int Num { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public Guid AccountId { get; set; }
    public Guid DiscountId { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public Guid CurrencyId { get; set; }
    public Currency Currency { get; set; } = null!;
    public decimal CurrencyVal { get; set; }

    public decimal Value { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public string? Note { get; set; }
    public Guid RegNum { get; set; }
    public CashType Type { get; set; }
    public Dealing Dealing { get; set; }

    public Guid? BankId { get; set; }
    public Bank? Bank { get; set; }

    public Guid? EWalletId { get; set; }
    public EWallet? EWallet { get; set; }

    public Guid? TreasuryId { get; set; }
    public Treasury? Treasury { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
