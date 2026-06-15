using System;

namespace AribONE.Models.Entities;

public class Group
{
    public Guid Id { get; set; }
    public Guid ParentId { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public int Num { get; set; }
}
