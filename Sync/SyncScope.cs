using System.Data;
using System.Linq;
using Dotmim.Sync;

namespace AribONE.Services.Sync;

/// <summary>
/// Single source of truth for the Dotmim.Sync scope (roadmap D1; locked
/// decisions D6/D9/D10). Both the per-shard gateway (provisions central tenant
/// DBs) and the branch app's sync client build their <see cref="SyncSetup"/>
/// from here, so the two sides can never drift.
///
/// Deliberately OUT of scope:
///  - <c>Companies</c>, <c>Branches</c> — cloud-authoritative, cached locally
///    by <see cref="TenantActivationService"/> (D6); never DMS-synced.
///  - <c>Accounts.{Debit,Credit,Balance}</c> — stored aggregates on shared
///    rows (D10): every node recomputes them from its own journal entries;
///    syncing them would let last-writer-wins corrupt ledger totals.
/// </summary>
public static class SyncScope
{
    /// <summary>One logical scope per branch DB; the BranchId filter parameter
    /// (D2) makes each branch's view of it distinct.</summary>
    public const string ScopeName = "arib_branch";

    /// <summary>Bumped on every schema change that touches a synced table.
    /// The gateway refuses clients whose version differs (D11) so an outdated
    /// branch can never write an old shape into central.
    /// Enforcement is deliberately EXACT equality (no backward-compatibility band):
    /// a tolerance window (C2 / MinSupportedSchemaVersion) was considered and
    /// intentionally deferred — it is only safe once migrations within the band are
    /// guaranteed additive, which we don't commit to yet. Until then a version bump
    /// is a fleet-wide flag day and a stale branch is told to update (HTTP 426).
    /// v2: gave the warehouse/order-scoped tables their own BranchId column so
    /// every synced row is pinned to the token's branch (no more join filters).
    /// v3: renamed ProductTransaction→InventoryMovement and DailyProductCost→
    /// WeightedAverageCost, and added InventoryBatches/InventoryBatchConsumptions
    /// for per-batch FIFO/LIFO/FEFO costing + expiry.
    /// v4: Shift Management — added the Shifts table and BillPayments to the synced
    /// branch tier, plus a nullable ShiftId column on the seven other anchor tables
    /// (Bills/TreasuriesTransactions/BankTransactions/EWalletTransactions/
    /// RevenueExpenses/CustomerTransactions/InventoryAdjustments).
    /// v5: added nullable PreviousBalance/EndingBalance snapshot columns to Bills
    /// for the ledger-based Previous/Ending Balance receipt feature.
    /// v6: product-type split — added nullable Products.PurchaseAccountId and
    /// relaxed Products.{SalesAccountId,StockAccountId,SalesCostAccountId} to
    /// nullable so Sales/Purchase Service rows carry only their relevant account.
    /// v7: accounting schema rename (tasks/spec-rename.md) — table/entity names only,
    /// zero shape change: AccountOperands→PostingAccounts, Bills→Invoices,
    /// BillEntries→InvoiceLines, BillPayments→InvoicePayments, JournalEntries→
    /// GeneralLedgerEntries, Customers→Partners, CustomerTransactions→
    /// PartnerLedgerEntries, Cashes→PaymentVouchers, RevenueExpenses→
    /// ExpenseIncomeVouchers, Banks→BankAccounts. A fleet flag-day in principle,
    /// but zero production tenants existed at rename time (2026-07-18).
    /// v8: Fiscal Year (tasks/spec-fiscal-year.md) — added the company-wide
    /// FiscalYears table to the master tier.
    /// v9: Inventory cost/GL reconciliation Phase 1
    /// (tasks/spec-inventory-cost-reconciliation.md) — added nullable
    /// InventoryBatches.LandedUnitCost. Folded into the same unpushed flag-day
    /// (tasks/spec-inventory-cost-restatement.md OQ2, decided 2026-07-23):
    /// widened WeightedAverageCosts.Price and WarehousesProductInventories.UnitCost
    /// from (18,2) to (18,4) — per-unit costs, not money, matching
    /// InventoryBatches.UnitCost's existing precision.
    /// v10: stock transfer between warehouses (tasks/spec-warehouse-transfer.md) — added
    /// StockTransfers/StockTransferLines/StockTransferLayers to the branch tier, each with
    /// the two-sided <see cref="TwoSidedBranchTables"/> filter rather than an OwnColumnFilters
    /// entry, since a transfer document must be visible to both its sending and receiving
    /// branch (single-column equality can't express that).
    /// v11: one-click invoice return (tasks/spec-invoice-return.md) — added SaleLineReturns
    /// (the Sale→SalesReturn return ledger, structural twin of ReservationFulfillments) to the
    /// branch tier with its own BranchId column/filter.
    /// v12: one-click purchase return (tasks/spec-purchase-return.md) — added
    /// PurchaseLineReturns (the Purchase→PurchaseReturn return ledger, structural twin of
    /// SaleLineReturns) to the branch tier with its own BranchId column/filter.
    /// v13: order management (tasks/spec-order-management.md) — added Orders/OrderLines
    /// to the branch tier, each with its own BranchId column/filter. Single-branch
    /// ownership (D7: a transfer closes the origin row and inserts a new one at the
    /// destination, never moves a row between branches), so both use OwnColumnFilters,
    /// not TwoSidedBranchTables.
    /// v14: reservation audit &amp; management (tasks/spec-reservation-audit-management.md) —
    /// added DocumentAuditEntries, the generic append-only document audit trail, to the branch
    /// tier with its own BranchId column/filter; an audit trail that never reaches central
    /// cannot be audited from HQ, which is the point of it (D8). The same flag day carries a
    /// rename of ReservationFulfillments.OrderLineId → ReservationLineId (D11b): OrderLine now
    /// names an unrelated v13 entity, so the old column pointed readers at the wrong table.
    /// The rename rides here deliberately — on its own it would have cost a second flag day
    /// for a purely cosmetic fix, which is why it had waited.
    /// v15: delivery couriers &amp; automatic delivery pricing
    /// (tasks/spec-delivery-couriers.md) — added Couriers and DeliveryTariffs to the branch
    /// tier, each with its own BranchId column/filter (single-branch ownership, D5/OQ1, so
    /// OwnColumnFilters and not TwoSidedBranchTables). The same flag day carries five nullable
    /// columns on Orders (CourierId/FailedByCourierId/FailedAt/FailedReason/FailedCount) and
    /// Partners.DeliveryFee, the per-customer fee override. Branches.DefaultDeliveryFee rides
    /// along too but is not a sync surface — Branches is cloud-authoritative and never DMS-synced.
    /// <b>Adding tables changes the scope shape</b>, so the v15 rollout must reprovision every
    /// tenant with <c>overwrite: true</c>: a plain re-provision returns success while silently
    /// leaving the scope at the old table count, with no _tracking tables for the two new ones
    /// and therefore no sync of them at all. <see cref="BranchTables"/> declares
    /// <c>Couriers</c>/<c>DeliveryTariffs</c> ahead of <c>Orders</c>/<c>OrderLines</c> — see the
    /// comment there — because Orders' new FK to Couriers made array order load-bearing for
    /// apply-time correctness, not just documentation; that reordering also needs the same
    /// <c>overwrite: true</c> reprovision to take effect on an already-provisioned tenant.</summary>
    public const int SchemaVersion = 15;

    /// <summary>
    /// Tier A (D9a): masters, replicated in full to every branch.
    /// <c>BankAccounts</c> is tenant-wide (no BranchId column) and FK-required by
    /// the branch-filtered <c>BankTransactions</c>, so it lives here even though
    /// D9's shorthand listed only the transactions.
    /// </summary>
    public static readonly string[] MasterTables =
    [
        "Products",
        "Groups",
        "Currencies",
        "UnitOfMeasures",
        "Areas",
        "Barcodes",
        "ProductDefaults",
        "Images",
        "Accounts",
        "PostingAccounts",
        "BankAccounts",
        "Users",
        "Roles",
        "Permissions",
        "UserRoles",
        "RolePermissions",
        "FiscalYears",
    ];

    /// <summary>
    /// Tier B (D9b): branch documents, BranchId-filtered at the gateway (D2).
    /// Every table here carries its own BranchId column (v2 added it to the
    /// warehouse/order-scoped tables that used to be join-filtered), so each
    /// filters on itself. Invoice subtypes (Sale/Purchase/…) share the TPH tables
    /// <c>Invoices</c>/<c>InvoiceLines</c>. <c>ReservationFulfillments</c> backs the
    /// in-store reservation workflow (renamed from <c>OrderFulfillments</c> in T1 of
    /// tasks/spec-order-management.md, to free the <c>Order</c>/<c>OrderLine</c> names
    /// for the unrelated order-management feature added in v13).
    /// </summary>
    public static readonly string[] BranchTables =
    [
        "Invoices",
        "InvoiceLines",
        "GeneralLedgerEntries",
        "Partners",
        "PartnerLedgerEntries",
        "PaymentVouchers",
        "Treasuries",
        "TreasuriesTransactions",
        "BankTransactions",
        "EWallets",
        "EWalletTransactions",
        "Warehouses",
        "WarehousesProductInventories",
        "InventoryMovements",
        "InventoryBatches",
        "InventoryBatchConsumptions",
        "ProductOpeningBalances",
        "InventoryAdjustments",
        "ExpenseIncomeVouchers",
        "WeightedAverageCosts",
        "ReservationFulfillments",
        "Shifts",
        "InvoicePayments",
        // v10: stock transfer between warehouses — two-sided filter, see TwoSidedBranchTables.
        "StockTransfers",
        "StockTransferLines",
        "StockTransferLayers",
        // v11: Sale→SalesReturn return ledger.
        "SaleLineReturns",
        // v12: Purchase→PurchaseReturn return ledger.
        "PurchaseLineReturns",
        // v15: delivery couriers & per-zone delivery pricing. Single-branch ownership
        // (a courier and a zone price both belong to one branch), so own-column filters.
        // Declared here, ahead of Orders/OrderLines below, deliberately out of schema-version
        // order: Order.CourierId/FailedByCourierId carry a hard FK to Couriers, and an upload
        // batch is applied table-by-table in this array's declared order (not a full
        // relation-driven topological sort) — Couriers after Orders meant the very first order
        // dispatched with a brand-new courier upserted Orders before its Couriers row existed
        // centrally, hit FK_Orders_Couriers_CourierId, rolled back the whole batch before ever
        // reaching Couriers' own insert, and wedged sync permanently (every retry re-fails the
        // same way, with OrderLines also failing downstream on FK_OrderLines_Orders_OrderId).
        // Reproduced 2026-08-23 against the local dev stack; fixed by reordering, not by a
        // schema change, so no SchemaVersion bump — existing tenants just need overwrite:true.
        "Couriers",
        "DeliveryTariffs",
        // v13: order management — single-branch ownership (D7), own-column filter.
        "Orders",
        "OrderLines",
        // v14: generic append-only document audit trail. Append-only means no update or
        // delete ever reaches it, so ServerWins conflict resolution has nothing to resolve.
        "DocumentAuditEntries",
    ];

    /// <summary>
    /// The Accounts columns that sync — every physical column except the D10
    /// aggregates <c>Debit</c>/<c>Credit</c>/<c>Balance</c>. Those three need a
    /// SQL DEFAULT so remote inserts (which omit them) succeed; see
    /// <c>AribContext.OnModelCreating</c>.
    /// </summary>
    public static readonly string[] AccountColumns =
    [
        "Id",
        "ParentId",
        "RootId",
        "Num",
        "TypeId",
        "NameAr",
        "NameEn",
        "IsParent",
        "Type",
        "Class",
        "IsActive",
        "Currency",
        "CreatedAt",
        "Note",
        "BranchId",
    ];

    /// <summary>
    /// Tier-B tables visible to both the branch that dispatched them and the branch they were
    /// sent to (D4) — a stock transfer document must sync to both sides, which single-column
    /// equality (<see cref="OwnColumnFilters"/>) cannot express. Every row on these tables
    /// carries its own <c>FromBranchId</c>/<c>ToBranchId</c> pair (denormalized onto the line
    /// and layer tables too), so each filters on itself with no join.
    /// </summary>
    public static readonly string[] TwoSidedBranchTables =
    [
        "StockTransfers",
        "StockTransferLines",
        "StockTransferLayers",
    ];

    /// <summary>All synced tables, both tiers.</summary>
    public static string[] AllTables => [.. MasterTables, .. BranchTables];

    /// <summary>The filter parameter every sync session must supply (D2);
    /// the gateway takes its value from the sync token, never the client.</summary>
    public const string BranchIdParameter = "BranchId";

    /// <summary>
    /// Tier-B tables filtered on their own branch column (D2).
    /// <c>InventoryMovements.BranchId</c> was historically misspelled
    /// <c>BrunchId</c>; renamed 2026-06-12 (entity + column — the rename
    /// migration and the int→GUID conversion script must both carry it).
    /// </summary>
    public static readonly (string Table, string Column)[] OwnColumnFilters =
    [
        ("Invoices", "BranchId"),
        ("InvoiceLines", "BranchId"),
        ("GeneralLedgerEntries", "BranchId"),
        ("Partners", "BranchId"),
        ("PartnerLedgerEntries", "BranchId"),
        ("PaymentVouchers", "BranchId"),
        ("Treasuries", "BranchId"),
        ("TreasuriesTransactions", "BranchId"),
        ("BankTransactions", "BranchId"),
        ("EWallets", "BranchId"),
        ("EWalletTransactions", "BranchId"),
        ("Warehouses", "BranchId"),
        ("InventoryMovements", "BranchId"),
        ("ExpenseIncomeVouchers", "BranchId"),
        // v2: own BranchId column added (were warehouse/order join-filtered).
        ("WarehousesProductInventories", "BranchId"),
        ("WeightedAverageCosts", "BranchId"),
        ("InventoryBatches", "BranchId"),
        ("InventoryBatchConsumptions", "BranchId"),
        ("InventoryAdjustments", "BranchId"),
        ("ProductOpeningBalances", "BranchId"),
        ("ReservationFulfillments", "BranchId"),
        // v4: shift management
        ("Shifts", "BranchId"),
        ("InvoicePayments", "BranchId"),
        // v11: one-click invoice return
        ("SaleLineReturns", "BranchId"),
        // v12: one-click purchase return
        ("PurchaseLineReturns", "BranchId"),
        // v13: order management
        ("Orders", "BranchId"),
        ("OrderLines", "BranchId"),
        // v14: document audit trail
        ("DocumentAuditEntries", "BranchId"),
        // v15: delivery couriers & zone pricing
        ("Couriers", "BranchId"),
        ("DeliveryTariffs", "BranchId"),
    ];

    /// <summary>Builds the canonical <see cref="SyncSetup"/>: both tiers, the
    /// D10 Accounts column exclusion and the D2 BranchId filters.</summary>
    public static SyncSetup Build()
    {
        var setup = new SyncSetup(AllTables);
        setup.Tables["Accounts"].Columns.AddRange(AccountColumns);

        foreach (var (table, column) in OwnColumnFilters)
        {
            var filter = new SetupFilter(table);
            filter.AddParameter(BranchIdParameter, DbType.Guid);
            filter.AddWhere(column, table, BranchIdParameter);
            setup.Filters.Add(filter);
        }

        // D4: OR across FromBranchId/ToBranchId. AddWhere only expresses column = parameter
        // (ANDed together), so the two-sided match needs AddCustomWhere instead. Dotmim.Sync
        // replaces {{{...}}} with a provider-quoted identifier ([...] on SQL Server, "..." on
        // Postgres) in the generated SQL. "base" is the fixed alias it gives the filtered table
        // in every stored procedure/command it builds — deliberately UNquoted here: on SQL
        // Server the alias itself is written bracket-quoted ([base]) but bare "base" still
        // resolves to it (brackets are quoting syntax, not part of the identifier); on Postgres
        // the alias is generated bare, so a literal "[base]" hits the parser as invalid syntax
        // (42601) the moment a filter with a custom where — e.g. this one — runs against a
        // Postgres-central DB. Keep this alias bracket-free.
        foreach (var table in TwoSidedBranchTables)
        {
            var filter = new SetupFilter(table);
            filter.AddParameter(BranchIdParameter, DbType.Guid);
            filter.AddCustomWhere(
                "base.{{{FromBranchId}}} = @" + BranchIdParameter +
                " OR base.{{{ToBranchId}}} = @" + BranchIdParameter);
            setup.Filters.Add(filter);
        }

        return setup;
    }
}
