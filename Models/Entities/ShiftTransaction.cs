using System;
using Microsoft.EntityFrameworkCore;

namespace AribONE.Models.Entities;

public class ShiftTransaction
{
    public int Id { get; set; }
    public int Num { get; set; }
    public int BranchId { get; set; }
    [Comment("user id")] public int OpenBy { get; set; }

    [Comment("the device who opens the shift")]
    public required string Pc { get; set; }

    public DateTime OpenIn { get; set; }
    public DateTime CloseIn { get; set; }
    public decimal OpenMoney { get; set; }
    public decimal CloseMoney { get; set; }
    public decimal ExpectedMoney { get; set; }
    public decimal ActualMoney { get; set; }
    public decimal Difference { get; set; }
}