using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AribONE.Models.Entities;

public class UserOpenDay
{
    public int Id { get; set; }

    public DateTime Dt { get; set; }

    [StringLength(50)] public string Name { get; set; }

    public int DayIndex { get; set; }

    public decimal PrevValue { get; set; }

    public decimal Debit { get; set; }

    public decimal Credit { get; set; }

    public decimal Balance { get; set; }

    public decimal CloseValue { get; set; }

    public bool IsClosed { get; set; }

    public DateTime? CloseDt { get; set; }

    public int BranchId { get; set; }

    public int PeriodId { get; set; }

    public bool IsFound { get; set; }

    [Column(TypeName = "text")] public string Note { get; set; }
}