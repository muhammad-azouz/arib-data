namespace AribONE.Models;

/// <summary>Wire-visible ticket status (tasks/spec-ariblink-gateway.md D4/D8, OQ-8). Read-only to
/// the terminal in v1 — no role gates a transition. <see cref="Locked"/> is reserved by D4 and is
/// never assigned anywhere in this codebase.</summary>
public enum TerminalOrderStatus
{
    Open = 0,
    Locked = 1,
    Paid = 2,
    Cancelled = 3,
}
