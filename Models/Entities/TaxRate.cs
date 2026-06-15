using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class TaxRate
{
    public TaxRate(string name, double rate, bool isFixed = false)
    {
        Name = name;
        Rate = rate;
        IsFixed = isFixed;
    }

    public int Id { get; set; }
    [MaxLength(50)] public string Name { get; set; }
    public double Rate { get; set; }
    public bool IsFixed { get; set; }
}