using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AribONE.Models.Entities;

public class CustomerDealing
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public DateTime Dt { get; set; }

    public int Num { get; set; }

    public int CustomerId { get; set; }

    [StringLength(50)] public string Main { get; set; }

    [StringLength(50)] public string Currency { get; set; }

    public decimal CurrencyVal { get; set; }

    public decimal Total { get; set; }

    public decimal Value { get; set; }

    public bool IsCredit { get; set; }

    public int RegNum { get; set; }

    [Column(TypeName = "text")] public string Note { get; set; }

    [StringLength(50)] public string Dealing { get; set; }

    [StringLength(50)] public string MyUser { get; set; }

    public int BranchId { get; set; }
}