namespace AribONE.Models;

/// <summary>
/// Top-level classification of a chart-of-accounts account, following the
/// universal account-numbering convention encoded by the leading digit of
/// <see cref="Entities.Account.Num"/>:
/// 1xxx = Asset, 2xxx = Liability, 3xxx = Equity, 4xxx = Expense, 5xxx = Revenue.
///
/// Use this to identify the standard account buckets (e.g. the Revenue/Expense
/// roots) instead of hardcoding account ids — it stays correct per-tenant and
/// across the fresh-install seed and in-place migrated databases.
/// </summary>
public enum AccountClass
{
    Asset = 1,
    Liability = 2,
    Equity = 3,
    Expense = 4,
    Revenue = 5,
}
