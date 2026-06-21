using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class Installment
{
    public int Id { get; set; }

    public DateTime NowDt { get; set; }

    public DateTime Dt { get; set; }

    public int CustomerId { get; set; }

    public decimal Day { get; set; }

    public decimal Month { get; set; }

    public decimal Value { get; set; }

    public bool Paid { get; set; }

    public decimal Discount { get; set; }

    public int RegNum { get; set; }

    public decimal Total { get; set; }

    public DateTime? ReceivedDt { get; set; }

    [StringLength(250)] public required string Note { get; set; }

    public decimal Money { get; set; }

    public int ReNum { get; set; }

    public int ReRegNum { get; set; }

    [StringLength(50)] public required string MyUser { get; set; }

    public decimal Counts { get; set; }

    public decimal LstMonth { get; set; }

    public decimal Monthly { get; set; }

    public int Num { get; set; }

    public decimal PaidMoney { get; set; }

    public int BranchId { get; set; }

    public decimal InstallValue { get; set; }

    public decimal InstallProfit { get; set; }

    public decimal PayMonth { get; set; }

    public decimal PayDay { get; set; }
}