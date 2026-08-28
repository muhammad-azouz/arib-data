namespace AribONE.Models;

/// <summary>
/// Which level of a bill a promotion reduces (spec D4). Item promotions come off
/// individual lines and roll into <c>Invoice.ItemDiscount</c>; Bill promotions come off
/// the whole bill and land in <c>Invoice.BillDiscount</c>. The two are evaluated
/// independently, so one bill can carry both.
///
/// Explicit numeric values: these are persisted as ints, so reordering the members
/// would silently re-interpret every stored row.
/// </summary>
public enum PromotionLevel
{
    Item = 0,
    Bill = 1,
}
