using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

public class Image
{
    public Guid Id { get; set; }
    [MaxLength(2097152)] public required byte[] ImageData { get; set; }
    public ImageKind Kind { get; set; }
}
