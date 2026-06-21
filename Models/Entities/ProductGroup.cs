using System;
using System.Diagnostics.CodeAnalysis;

namespace AribONE.Models.Entities;

public class ProductGroup : Group
{
    public int ProductCount { get; set; }
    // public virtual ICollection<Product>? Products { get; set; }

    public ProductGroup()
    {
    }

    [SetsRequiredMembers]
    public ProductGroup(Guid id, Guid parentId, string name, bool isActive, int num, int productCount) : this()
    {
        Id = id;
        ParentId = parentId;
        Name = name;
        IsActive = isActive;
        Num = num;
        ProductCount = productCount;
    }
}
