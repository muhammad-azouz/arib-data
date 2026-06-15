using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class Account
{
    public Guid Id { get; set; }
    public Guid ParentId { get; set; }
    public Guid RootId { get; set; }
    public int Num { get; set; }
    public int TypeId { get; set; }

    [StringLength(100)] public string NameAr { get; set; } = string.Empty;
    [StringLength(100)] public string NameEn { get; set; } = string.Empty;

    public bool IsParent { get; set; }

    public AccountType Type { get; set; }

    /// <summary>
    /// Universal-numbering classification (Asset/Liability/Equity/Expense/Revenue),
    /// derived from the leading digit of <see cref="Num"/>. Used to locate the
    /// standard account buckets without hardcoding ids.
    /// </summary>
    public AccountClass Class { get; set; }

    public bool IsActive { get; set; } = true;
    [StringLength(3)] public string Currency { get; set; } = string.Empty;
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal Balance { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [StringLength(200)] public string Note { get; set; } = string.Empty;

    public Guid BranchId { get; set; }
}
