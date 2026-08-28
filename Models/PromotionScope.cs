namespace AribONE.Models;

/// <summary>
/// What an item-level promotion targets (spec D3). Meaningless at
/// <see cref="PromotionLevel.Bill"/>, which always applies to the whole bill.
///
/// <c>AllProducts</c> is storewide and carries <b>zero</b> PromotionTarget rows — the
/// absence of targets, never a magic row — so an accidentally-emptied target list can
/// never silently become "everything". Validation refuses Products/Groups with an empty
/// list, which is only a safe rule because AllProducts is a separate value.
///
/// Explicit numeric values: persisted as ints, so member order is load-bearing.
/// </summary>
public enum PromotionScope
{
    AllProducts = 0,
    Products = 1,
    Groups = 2,
}
