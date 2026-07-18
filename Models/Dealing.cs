namespace AribONE.Models;

public enum Dealing
{
    Sale = 100,
    ReSale = 101,
    Purchase = 200,
    RePurchase = 201,
    Order = 300,
    CashIn = 400,
    CashOut = 401,
    EWalletIn = 500,
    EWalletOut = 501,
    BankIn = 600,
    BankOut = 601,
    OpenBalance = 700,
    CashDiscount = 800,
    PreviousBalance = 900,
    Revenue = 1000,
    Expenses = 1001,
    CloseTreasury = 1100,
    InventoryAdjustment = 2000,
    YearClose = 2100
}