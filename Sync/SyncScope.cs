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
    /// InventoryBatches.LandedUnitCost; no other shape change.</summary>
    public const int SchemaVersion = 9;

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
    /// <c>Invoices</c>/<c>InvoiceLines</c>. <c>OrderFulfillments</c> backs the
    /// HQ-ordering workflow (D13: HQ writes Order rows, the branch answers with
    /// fulfillment rows).
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
        "OrderFulfillments",
        "Shifts",
        "InvoicePayments",
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
        ("OrderFulfillments", "BranchId"),
        // v4: shift management
        ("Shifts", "BranchId"),
        ("InvoicePayments", "BranchId"),
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

        return setup;
    }
}
