using System;

namespace AribONE.Models.Entities;

public enum ExpenseIncomeVoucherDealing
{
    Revenue = 1000,
    Expenses = 1001,
}

public class ExpenseIncomeVoucher : IShiftScoped
{
    public Guid Id { get; set; }
    public DateTime CreateAt { get; set; }
    public ExpenseIncomeVoucherDealing Dealing { get; set; }
    public int Num { get; set; }
    public string? Note { get; set; }

    public Guid AccountId { get; set; }
    public Account Account { get; set; } = null!;

    public Guid? TreasuryId { get; set; }
    public Treasury? Treasury { get; set; }

    public Guid? BankId { get; set; }
    public BankAccount? Bank { get; set; }

    public Guid? EwalletId { get; set; }
    public EWallet? Ewallet { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public decimal Value { get; set; }
    public decimal Total { get; set; }

    public Guid RegNum { get; set; }

    public int? WorkId { get; set; }

    public Guid CurrencyId { get; set; }
    public Currency Currency { get; set; } = null!;
    public decimal CurrencyVal { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    /// <summary>Owning shift in Shift Mode; null in Open Safe mode.
    /// Stamped by ShiftIdInterceptor.</summary>
    public Guid? ShiftId { get; set; }
}
