using System;
using System.Threading;

namespace AribONE.Interceptors;

/// <summary>
/// Ambient scope that tells <see cref="ShiftIdInterceptor"/> to skip stamping
/// <c>ShiftId</c> on the current <c>SaveChanges</c> — used by flows whose cash
/// moves through the branch safe rather than the cashier's drawer (e.g. a
/// purchase invoice, or an expense/cash-in-out explicitly sourced from the
/// safe). Backed by <see cref="AsyncLocal{T}"/> so it flows correctly through
/// async saves without leaking across unrelated concurrent operations.
/// </summary>
public static class ShiftTagging
{
    private static readonly AsyncLocal<bool> _suppressed = new();

    public static bool IsSuppressed => _suppressed.Value;

    public static IDisposable Suppress() => new SuppressionScope();

    private sealed class SuppressionScope : IDisposable
    {
        private readonly bool _previous;

        public SuppressionScope()
        {
            _previous = _suppressed.Value;
            _suppressed.Value = true;
        }

        public void Dispose() => _suppressed.Value = _previous;
    }
}
