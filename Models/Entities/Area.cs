using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class Area
{
    public Guid Id { get; set; }

    public Guid? ParentId { get; set; }

    [StringLength(50)] public string? Country { get; set; }

    [StringLength(50)] public string? State { get; set; }

    [StringLength(50)] public string? City { get; set; }

    [StringLength(50)] public string? Village { get; set; }

    [StringLength(50)] public string? Tag { get; set; }

    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
