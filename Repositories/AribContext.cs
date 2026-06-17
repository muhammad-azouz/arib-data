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

    // Entities
    public DbSet<Group> Groups { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountOperand> AccountOperands { get; set; }
    public DbSet<JournalEntry> JournalEntries { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Treasury> Treasuries { get; set; }
    public DbSet<TreasuryTransaction> TreasuriesTransactions { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<WarehouseProductInventory> WarehousesProductInventories { get; set; }
    public DbSet<DailyProductCost> DailyProductCosts { get; set; }
    public DbSet<ProductTransaction> ProductTransactions { get; set; }
    public DbSet<ProductOpeningBalance> ProductOpeningBalances { get; set; }
    public DbSet<Product> Products { get; set; }

    public DbSet<ProductBarcode> Barcodes { get; set; }
    public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }

    public DbSet<ProductDefault> ProductDefaults { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerTransaction> CustomerTransactions { get; set; }

    public DbSet<Bill> Bills { get; set; }
    public DbSet<BillEntry> BillEntries { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<PurchaseEntry> PurchaseEntries { get; set; }

    public DbSet<RePurchase> RePurchases { get; set; }
    public DbSet<RePurchaseEntry> RePurchaseEntries { get; set; }

    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleEntry> SaleEntries { get; set; }

    public DbSet<ReSale> ReSales { get; set; }
    public DbSet<ReSaleEntry> ReSaleEntries { get; set; }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderEntry> OrderEntries { get; set; }
    public DbSet<OrderFulfillment> OrderFulfillments { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    public DbSet<Cash> Cashes { get; set; }

    public DbSet<Bank> Banks { get; set; }
    public DbSet<BankTransaction> BankTransactions { get; set; }

    public DbSet<EWallet> EWallets { get; set; }
    public DbSet<EWalletTransaction> EWalletTransactions { get; set; }

    public DbSet<RevenueExpenses> RevenueExpenses { get; set; }

    public DbSet<InventoryAdjustment> InventoryAdjustments { get; set; }

    [DbFunction("NormalizeArabic", "dbo")]
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
            options.AddInterceptors(new BranchIdInterceptor());
#if DEBUG
            options.EnableSensitiveDataLogging();
#endif
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_CI_AS");

        // Debit/Credit/Balance are excluded from the sync column list (D10), so
        // DMS inserts arriving from other nodes omit them — without a SQL
        // DEFAULT those inserts fail. Balances are recomputed locally instead.
        modelBuilder.Entity<Account>().Property(x => x.Debit).HasDefaultValue(0m);
        modelBuilder.Entity<Account>().Property(x => x.Credit).HasDefaultValue(0m);
        modelBuilder.Entity<Account>().Property(x => x.Balance).HasDefaultValue(0m);

        modelBuilder.Entity<Group>()
            .HasDiscriminator<string>("Kind")
            .HasValue<ProductGroup>("Product")
            .HasValue<CustomerGroup>("Customer");

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

        modelBuilder.Entity<DailyProductCost>()
            .Property(x => x.Qty).HasPrecision(18, 3);

        modelBuilder.Entity<ProductOpeningBalance>()
            .Property(x => x.Qty).HasPrecision(18, 3);

        modelBuilder.Entity<ProductTransaction>()
            .Property(x => x.InQty).HasPrecision(18, 3);
        modelBuilder.Entity<ProductTransaction>()
            .Property(x => x.OutQty).HasPrecision(18, 3);

        modelBuilder.Entity<Bill>()
            .HasDiscriminator(x => x.Type)
            .HasValue<Sale>(BillType.Sale)
            .HasValue<ReSale>(BillType.ReSale)
            .HasValue<Purchase>(BillType.Purchase)
            .HasValue<RePurchase>(BillType.RePurchase)
            .HasValue<Order>(BillType.Order);

        modelBuilder.Entity<BillEntry>()
            .Property<string>("Discriminator"); // Exposes existing column

        // modelBuilder.Entity<Bill>().Property("Type").HasMaxLength(3)

        modelBuilder.Entity<BillEntry>()
            .HasOne(x => x.Branch)
            .WithMany()
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BillEntry>()
            .HasOne(x => x.Unit)
            .WithMany()
            .HasForeignKey(x => x.UnitId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BillEntry>()
            .HasOne(x => x.Warehouse)
            .WithMany()
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BillEntry>()
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

        modelBuilder.Entity<Cash>()
            .HasOne(x => x.Currency)
            .WithMany()
            .HasForeignKey(x => x.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BankTransaction>()
            .HasOne(x => x.Bank)
            .WithMany()
            .HasForeignKey(x => x.BankId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RevenueExpenses>()
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
        modelBuilder.Entity<DailyProductCost>()
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