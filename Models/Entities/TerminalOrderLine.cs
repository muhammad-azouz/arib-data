using System;
using System.ComponentModel.DataAnnotations;

namespace AribONE.Models.Entities;

/// <summary>One product/qty row on a <see cref="TerminalOrder"/> (tasks/spec-ariblink-gateway.md
/// §8.9). Every display field is a <b>snapshot taken at add time</b> — <see cref="ProductName"/>,
/// <see cref="CategoryName"/>, <see cref="UnitPrice"/>, <see cref="UnitName"/> — so a later price
/// change, rename or category move never alters an existing line, and the till loads the ticket at
/// the price the customer was quoted (D12), not today's catalog price.</summary>
public class TerminalOrderLine
{
    public Guid Id { get; set; }

    public Guid TerminalOrderId { get; set; }
    public TerminalOrder TerminalOrder { get; set; } = null!;

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    [MaxLength(200)] public required string ProductName { get; set; }
    [MaxLength(200)] public string? CategoryName { get; set; }
    [MaxLength(50)] public required string UnitName { get; set; }

    public decimal UnitPrice { get; set; }
    public decimal Qty { get; set; }
    public decimal LineTotal { get; set; }

    /// <summary>Denormalized from the terminal that added the line (D3) — no FK, matching
    /// <see cref="TerminalOrder.SectionId"/>/<see cref="TerminalOrder.TerminalId"/>.</summary>
    [MaxLength(64)] public required string AddedBySectionId { get; set; }
    [MaxLength(100)] public required string AddedBySectionName { get; set; }

    public Guid AddedByUserId { get; set; }
    public User AddedByUser { get; set; } = null!;

    [MaxLength(64)] public required string AddedByTerminalId { get; set; }

    public DateTime AddedAt { get; set; }
}
