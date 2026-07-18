using System;
using AribONE.Interceptors;
using AribONE.Models;
using AribONE.Models.Entities;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace AribONE.Repositories;

public class AribContext : DbContext
{
    // Parameterless constructor (optional, for runtime if you use OnConfiguring)
    public AribContext()
    {
    }

    // Constructor for design-time and dependency injection
    public AribContext(DbContextOptions<AribContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Supplies the connection string for the parameterless <see cref="AribContext"/>
    /// ctor (the ~170 <c>new AribContext()</c> call sites). The host sets this once at
    /// startup — the desktop app points it at <c>App.DatabaseHelper.ConnectionString</c>;
    /// the gateway and design-time tooling use the <see cref="DbContextOptions"/> ctor
    /// instead and never touch it. Keeps this UI-free library independent of the app.
    /// </summary>
    public static Func<string>? ConnectionStringProvider { get; set; }

    /// <summary>
    /// Supplies the active branch id for the <see cref="BranchIdInterceptor"/>. The
    /// desktop app sets this to <c>() =&gt; Preference.Instance.Branch?.Id ?? Guid.Empty</c>
    /// at startup (Branch is null until login / first-run company setup completes, so the
    /// null-coalesce makes the interceptor skip stamping rather than throw). When the
    /// provider itself is null (gateway, design-time) the interceptor is a no-op.
    /// </summary>
    public static Func<Guid>? BranchIdProvider { get; set; }

    /// <summary>
    /// Supplies the current open shift id for the <see cref="ShiftIdInterceptor"/>.
    /// The desktop app sets this to <c>() =&gt; ShiftContext.Current?.CurrentShiftId</c>
    /// at startup. Returns null in Open Safe mode or when no shift is open on this
    /// workstation, so anchor rows keep a null ShiftId (identical to pre-shift
    /// behaviour). When the provider itself is null (gateway, design-time) the
    /// interceptor is a no-op.
    /// </summary>
    public static Func<Guid?>? ShiftIdProvider { get; set; }

    /// <summary>
    /// Invoked by <see cref="AribONE.Interceptors.ShiftIdInterceptor"/> when a save is
    /// aborted because the ambient shift was closed on another terminal (force-close or
    /// a shared/duplicate workstation id). The desktop host sets this to drop the stale
    /// <c>ShiftContext</c> so the gate blocks and the banner flips to "closed". Null
    /// (gateway, design-time) = no-op.
    /// </summary>
    public static Action? ShiftClosedCallback { get; set; }

    // Entities
    public DbSet<Group> Groups { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<PostingAccount> PostingAccounts { get; set; }
    public DbSet<GeneralLedgerEntry> GeneralLedgerEntries { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Treasury> Treasuries { get; set; }
    public DbSet<TreasuryTransaction> TreasuriesTransactions { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<WarehouseProductInventory> WarehousesProductInventories { get; set; }
    public DbSet<WeightedAverageCost> WeightedAverageCosts { get; set; }
    public DbSet<InventoryMovement> InventoryMovements { get; set; }
    public DbSet<InventoryBatch> InventoryBatches { get; set; }
    public DbSet<InventoryBatchConsumption> InventoryBatchConsumptions { get; set; }
    public DbSet<ProductOpeningBalance> ProductOpeningBalances { get; set; }
    public DbSet<Product> Products { get; set; }

    public DbSet<ProductBarcode> Barcodes { get; set; }
    public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }

    public DbSet<ProductDefault> ProductDefaults { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<PartnerLedgerEntry> PartnerLedgerEntries { get; set; }

    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceLine> InvoiceLines { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<PurchaseLine> PurchaseLines { get; set; }

    public DbSet<PurchaseReturn> PurchaseReturns { get; set; }
    public DbSet<PurchaseReturnLine> PurchaseReturnLines { get; set; }

    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleLine> SaleLines { get; set; }

    public DbSet<SalesReturn> SalesReturns { get; set; }
    public DbSet<SalesReturnLine> SalesReturnLines { get; set; }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    public DbSet<OrderFulfillment> OrderFulfillments { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    public DbSet<PaymentVoucher> PaymentVouchers { get; set; }

    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<BankTransaction> BankTransactions { get; set; }

    public DbSet<EWallet> EWallets { get; set; }
    public DbSet<EWalletTransaction> EWalletTransactions { get; set; }

    public DbSet<ExpenseIncomeVoucher> ExpenseIncomeVouchers { get; set; }

    public DbSet<InventoryAdjustment> InventoryAdjustments { get; set; }

    public DbSet<InstallmentPlan> InstallmentPlans { get; set; }
    public DbSet<InstallmentItem> InstallmentItems { get; set; }
    public DbSet<InstallmentPayment> InstallmentPayments { get; set; }

    public DbSet<InvoicePayment> InvoicePayments { get; set; }

    // Derived, local-only notifications (NOT in SyncScope — see AppNotification).
    public DbSet<AppNotification> AppNotifications { get; set; }

    // Per-user read/dismiss for the above (NOT in SyncScope — see NotificationReadState).
    public DbSet<NotificationReadState> NotificationReadStates { get; set; }

    // Branch-wide notification config (NOT in SyncScope — see NotificationSetting).
    public DbSet<NotificationSetting> NotificationSettings { get; set; }

    public DbSet<Shift> Shifts { get; set; }

    public DbSet<FiscalYear> FiscalYears { get; set; }

    // Mapped to the SQL Server scalar UDF dbo.NormalizeArabic in OnModelCreating,
    // but ONLY on SQL Server — Postgres has no such function and the gateway never
    // calls it against a Postgres central, so the mapping is gated by provider
    // rather than declared with a [DbFunction] attribute (which would register it
    // unconditionally for every provider).
    public static string NormalizeArabic(string input)
        => throw new NotSupportedException();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            var cs = ConnectionStringProvider?.Invoke()
                     ?? throw new InvalidOperationException(
                         "AribContext.ConnectionStringProvider has not been set. " +
                         "The host must assign it once at startup before any " +
                         "parameterless new AribContext() is used.");
            options.UseSqlServer(cs);
            options.AddInterceptors(new BranchIdInterceptor(), new ShiftIdInterceptor());
#if DEBUG
            options.EnableSensitiveDataLogging();
#endif
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Provider-conditional bits. SQL Server is the default/desktop path and
        // keeps its exact existing shape; Postgres (gateway central only) gets the
        // equivalents. Database.IsNpgsql()/IsSqlServer() read the configured
        // provider during model build — the standard EF multi-provider pattern.
        if (Database.IsNpgsql())
        {
            // The Postgres central's Arabic ordering comes from the database's
            // default ICU 'ar' collation, set once at CREATE DATABASE time by the
            // gateway dialect — so no model-level collation (which on Npgsql has no
            // clean migration target for an already-created database) is needed.

            // Npgsql maps DateTime -> "timestamp with time zone" and throws when a
            // value has DateTimeKind.Local (entities use DateTime.Now). Mirror SQL
            // Server's datetime2 with "timestamp without time zone" so DMS-applied
            // Local-kind values write without the UTC-only guard (Dotmim.Sync#919).
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                foreach (var property in entityType.GetProperties())
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                        property.SetColumnType("timestamp without time zone");
        }
        else
        {
            modelBuilder.UseCollation("Arabic_CI_AS");

            // dbo.NormalizeArabic is a SQL Server scalar UDF; map it only here.
            modelBuilder.HasDbFunction(
                typeof(AribContext).GetMethod(nameof(NormalizeArabic))!)
                .HasName("NormalizeArabic").HasSchema("dbo");
        }

        // Debit/Credit/Balance are excluded from the sync column list (D10), so
        // DMS inserts arriving from other nodes omit them — without a SQL
        // DEFAULT those inserts fail. Balances are recomputed locally instead.
        modelBuilder.Entity<Account>().Property(x => x.Debit).HasDefaultValue(0m);
        modelBuilder.Entity<Account>().Property(x => x.Credit).HasDefaultValue(0m);
        modelBuilder.Entity<Account>().Property(x => x.Balance).HasDefaultValue(0m);

        modelBuilder.Entity<Group>()
            .HasDiscriminator<string>("Kind")
            .HasValue<ProductGroup>("Product")
            .HasValue<PartnerGroup>("Customer");

        modelBuilder.Entity<Product>()
            .HasMany(x => x.Warehouses)
            .WithMany(x => x.Products)
            .UsingEntity<WarehouseProductInventory>();

        modelBuilder.Entity<WarehouseProductInventory>()
            .Property(x => x.TotalQty).HasPrecision(18, 3);
        modelBuilder.Entity<WarehouseProductInventory>()
            .Property(x => x.LastInQty).HasPrecision(18, 3);
        modelBuilder.Entity<WarehouseProductInventory>()
            .Property(x => x.LastOutQty).HasPrecision(18, 3);

        modelBuilder.Entity<WeightedAverageCost>()
            .Property(x => x.Qty).HasPrecision(18, 3);

        modelBuilder.Entity<ProductOpeningBalance>()
            .Property(x => x.Qty).HasPrecision(18, 3);

        modelBuilder.Entity<InventoryMovement>()
            .Property(x => x.InQty).HasPrecision(18, 3);
        modelBuilder.Entity<InventoryMovement>()
            .Property(x => x.OutQty).HasPrecision(18, 3);
        // Discriminator value strings ("Product"/"Customer") are stored DB data —
        // unchanged by the PartnerGroup rename above.

        // FIFO/LIFO/FEFO cost-and-expiry layers. Quantities 18,3; per-unit cost 18,4
        // (matches InvoiceLine.ItemCost). Batches deplete by ExpiryDate (FEFO) or
        // ReceivedDate (FIFO/LIFO); indexed for both the availability scan and expiry
        // reporting. SourceRegNum/RegNum index the batch back to the bill that
        // created/consumed it for reversal.
        modelBuilder.Entity<InventoryBatch>()
            .Property(x => x.InitialQty).HasPrecision(18, 3);
        modelBuilder.Entity<InventoryBatch>()
            .Property(x => x.RemainingQty).HasPrecision(18, 3);
        modelBuilder.Entity<InventoryBatch>()
            .Property(x => x.UnitCost).HasPrecision(18, 4);
        modelBuilder.Entity<InventoryBatch>()
            .HasIndex(x => new { x.ProductId, x.WarehouseId, x.RemainingQty });
        modelBuilder.Entity<InventoryBatch>()
            .HasIndex(x => x.ExpiryDate);
        modelBuilder.Entity<InventoryBatch>()
            .HasIndex(x => x.SourceRegNum);
        modelBuilder.Entity<InventoryBatch>()
            .HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<InventoryBatch>()
            .HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<InventoryBatch>()
            .HasOne(x => x.Warehouse).WithMany().HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<InventoryBatchConsumption>()
            .Property(x => x.Qty).HasPrecision(18, 3);
        modelBuilder.Entity<InventoryBatchConsumption>()
            .Property(x => x.UnitCost).HasPrecision(18, 4);
        modelBuilder.Entity<InventoryBatchConsumption>()
            .HasIndex(x => x.RegNum);
        modelBuilder.Entity<InventoryBatchConsumption>()
            .HasOne(x => x.Batch).WithMany().HasForeignKey(x => x.BatchId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Invoice>()
            .HasDiscriminator(x => x.Type)
            .HasValue<Sale>(InvoiceType.Sale)
            .HasValue<SalesReturn>(InvoiceType.SalesReturn)
            .HasValue<Purchase>(InvoiceType.Purchase)
            .HasValue<PurchaseReturn>(InvoiceType.PurchaseReturn)
            .HasValue<Order>(InvoiceType.Order);

        // Pins the TPH discriminator to the pre-rename class names — EF's default
        // convention writes the CLR type name, which would silently drift to the new
        // class names (SaleLine, etc.) for newly-inserted rows otherwise, corrupting
        // the invariant that stored Discriminator values are byte-identical pre/post
        // rename (existing rows already contain "SaleEntry"/"PurchaseEntry"/etc.).
        modelBuilder.Entity<InvoiceLine>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<SaleLine>("SaleEntry")
            .HasValue<PurchaseLine>("PurchaseEntry")
            .HasValue<SalesReturnLine>("ReSaleEntry")
            .HasValue<PurchaseReturnLine>("RePurchaseEntry")
            .HasValue<OrderLine>("OrderEntry");

        // modelBuilder.Entity<Invoice>().Property("Type").HasMaxLength(3)

        modelBuilder.Entity<InvoiceLine>()
            .HasOne(x => x.Branch)
            .WithMany()
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<InvoiceLine>()
            .HasOne(x => x.Unit)
            .WithMany()
            .HasForeignKey(x => x.UnitId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<InvoiceLine>()
            .HasOne(x => x.Warehouse)
            .WithMany()
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<InvoiceLine>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderFulfillment>()
            .Property(x => x.Qty).HasPrecision(18, 3);

        modelBuilder.Entity<TreasuryTransaction>()
            .HasOne(x => x.Treasury)
            .WithMany()
            .HasForeignKey(x => x.TreasuryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PaymentVoucher>()
            .HasOne(x => x.Currency)
            .WithMany()
            .HasForeignKey(x => x.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BankTransaction>()
            .HasOne(x => x.Bank)
            .WithMany()
            .HasForeignKey(x => x.BankId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ExpenseIncomeVoucher>()
            .HasOne(x => x.Currency)
            .WithMany()
            .HasForeignKey(x => x.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        // user relations
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);

        modelBuilder.Entity<RolePermission>()
            .HasOne(rp => rp.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(rp => rp.PermissionId);

        modelBuilder.Entity<InventoryAdjustment>()
            .Property(x => x.DiffQty).HasPrecision(18, 3);
        modelBuilder.Entity<InventoryAdjustment>()
            .Property(x => x.NewQty).HasPrecision(18, 3);
        modelBuilder.Entity<InventoryAdjustment>()
            .Property(x => x.NowQty).HasPrecision(18, 3);

        // Every branch-filtered sync table carries its own BranchId so the gateway
        // pins each synced row to the token's branch (D2): a branch can never push a
        // row into another branch's slice. These four hung off Warehouse/OrderEntry
        // and were join-filtered before; the column lets them filter on themselves
        // like the rest. Restrict (not the convention default Cascade) keeps Branch
        // off the multiple-cascade-paths SQL Server rejects (Branch→Warehouse→row).
        modelBuilder.Entity<WarehouseProductInventory>()
            .HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<WeightedAverageCost>()
            .HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<ProductOpeningBalance>()
            .HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<InventoryAdjustment>()
            .HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<OrderFulfillment>()
            .HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        // InstallmentPlan money columns (Principal, RoundingStep, Amount, PaidAmount)
        // inherit the global decimal(18,2) money convention — no precision override.
        modelBuilder.Entity<InstallmentPlan>()
            .HasOne(x => x.Partner).WithMany()
            .HasForeignKey(x => x.PartnerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<InstallmentItem>()
            .HasOne(x => x.Plan).WithMany(p => p.Installments)
            .HasForeignKey(x => x.InstallmentPlanId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<InstallmentPayment>()
            .HasOne(x => x.InstallmentItem).WithMany(i => i.Payments)
            .HasForeignKey(x => x.InstallmentItemId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<InvoicePayment>()
            .Property(x => x.Amount).HasPrecision(18, 2);
        modelBuilder.Entity<InvoicePayment>()
            .HasOne(x => x.Invoice).WithMany()
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Derived, local-only notifications (deliberately absent from SyncScope, so no
        // _dms_sync trigger and no schema-version bump). DedupKey is the reconciliation
        // identity — unique so a rule re-run upserts instead of duplicating. The two
        // composite indexes back the drawer list query and the unread-count badge.
        modelBuilder.Entity<AppNotification>()
            .HasIndex(x => x.DedupKey).IsUnique();
        modelBuilder.Entity<AppNotification>()
            .HasIndex(x => new { x.BranchId, x.IsResolved, x.CreatedAt });

        // Per-user read/dismiss side table (also local-only). One row per (notification,
        // user); the unique index is the upsert key. Cascade-delete with the notification so
        // pruning a resolved notification cleans up its read rows.
        modelBuilder.Entity<NotificationReadState>()
            .HasIndex(x => new { x.NotificationId, x.UserId }).IsUnique();
        modelBuilder.Entity<NotificationReadState>()
            .HasOne(x => x.Notification).WithMany()
            .HasForeignKey(x => x.NotificationId)
            .OnDelete(DeleteBehavior.Cascade);

        // Branch-wide notification settings — one row per branch (also local-only).
        modelBuilder.Entity<NotificationSetting>()
            .HasIndex(x => x.BranchId).IsUnique();

        // Shift Management. A shift tags existing financial rows (IShiftScoped) on
        // the branch's shared treasury; it is branch-filtered & synced (SyncScope v4).
        // Restrict FKs keep Branch off SQL Server's multiple-cascade-paths rejection.
        // All Shift decimals are money → they inherit the global decimal(18,2)
        // convention (ConfigureConventions); no per-property precision override.
        modelBuilder.Entity<Shift>()
            .HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Shift>()
            .HasOne(x => x.OpenedByUser).WithMany().HasForeignKey(x => x.OpenedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
        // The ambient open-shift lookup: one open shift per (branch, workstation).
        modelBuilder.Entity<Shift>()
            .HasIndex(x => new { x.BranchId, x.WorkstationId, x.Status });

        // Fiscal Year. Company-wide calendar (no BranchId); NetProfit is money →
        // inherits the global decimal(18,2) convention, no override.
        modelBuilder.Entity<FiscalYear>()
            .HasIndex(x => new { x.StartDate, x.EndDate }).IsUnique();
        modelBuilder.Entity<FiscalYear>()
            .HasIndex(x => x.Status);

        // ShiftId tag indexes — every shift report aggregates by these, so they must
        // stay fast on large databases.
        modelBuilder.Entity<Invoice>().HasIndex(x => x.ShiftId);
        modelBuilder.Entity<TreasuryTransaction>().HasIndex(x => x.ShiftId);
        modelBuilder.Entity<BankTransaction>().HasIndex(x => x.ShiftId);
        modelBuilder.Entity<EWalletTransaction>().HasIndex(x => x.ShiftId);
        modelBuilder.Entity<ExpenseIncomeVoucher>().HasIndex(x => x.ShiftId);
        modelBuilder.Entity<PartnerLedgerEntry>().HasIndex(x => x.ShiftId);
        modelBuilder.Entity<InventoryAdjustment>().HasIndex(x => x.ShiftId);
        modelBuilder.Entity<InvoicePayment>().HasIndex(x => x.ShiftId);

        // seed data here
        modelBuilder.Seed();

        // Client-side GUID v7 generation for every Guid primary key.
        // Offline branches assign sortable, collision-free ids at Add-time
        // (no DB default, plain uniqueidentifier column).
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var key = entityType.FindPrimaryKey();
            if (key is null)
                continue;

            foreach (var property in key.Properties)
            {
                if (property.ClrType == typeof(Guid))
                {
                    property.SetValueGeneratorFactory((_, _) => new GuidV7ValueGenerator());
                    property.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd;
                }
            }
        }

        modelBuilder.Entity<ProductBarcode>()
            .HasIndex(pb => pb.Code)
            .IsUnique();

        // DMS sync provisioning adds insert/update/delete triggers to every table in
        // the sync scope (SyncScope.AllTables). SQL Server forbids EF Core's default
        // OUTPUT-without-INTO write clause on a table that has triggers, which breaks
        // every POS write to a synced table once the branch DB is provisioned. Declaring
        // the triggers to EF flips it to the trigger-safe OUTPUT…INTO path. The names are
        // metadata only — DMS owns the actual triggers; EF never creates or manages them.
        var syncedTables = new System.Collections.Generic.HashSet<string>(
            Services.Sync.SyncScope.AllTables);
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.BaseType is not null || entityType.IsOwned())
                continue; // TPH-derived/owned types share another entity's table mapping
            var tableName = entityType.GetTableName();
            if (tableName is not null && syncedTables.Contains(tableName))
                modelBuilder.Entity(entityType.ClrType)
                    .ToTable(tb => tb.HasTrigger($"{tableName}_dms_sync"));
        }

        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(
        ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(18, 2);
    }
}