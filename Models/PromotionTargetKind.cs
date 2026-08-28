namespace AribONE.Models;

/// <summary>
/// Discriminator for a PromotionTarget row: whether its <c>RefId</c> points at a Product
/// or a Group. The reference is polymorphic and therefore carries no FK by construction —
/// the same "correlation id, not an enforced FK" posture Invoice.OriginalInvoiceId takes.
///
/// A Group target cascades to every descendant group (spec D3), expanded once at cache
/// load rather than walked per line.
///
/// Explicit numeric values: persisted as ints, so member order is load-bearing.
/// </summary>
public enum PromotionTargetKind
{
    Product = 0,
    Group = 1,
}
