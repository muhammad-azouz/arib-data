using System;
using AribONE.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AribONE;

/// <summary>
/// Lets <c>dotnet ef</c> build an <see cref="AribContext"/> against this library
/// directly — no startup project needed. The SQL Server migrations live in this
/// same assembly as the context, so EF needs no MigrationsAssembly override. Set
/// <c>ARIB_DESIGN_CS</c> to target a real database when applying migrations;
/// add/scaffold operations work against the default placeholder. The PostgreSQL
/// migrations live in <c>AribONE.Data.Migrations.Postgres</c> and have their own
/// design-time factory there.
/// </summary>
public sealed class AribContextDesignTimeFactory : IDesignTimeDbContextFactory<AribContext>
{
    public AribContext CreateDbContext(string[] args)
    {
        var cs = Environment.GetEnvironmentVariable("ARIB_DESIGN_CS")
                 ?? "Server=localhost;Database=arib_designtime;Trusted_Connection=True;TrustServerCertificate=True";
        var opts = new DbContextOptionsBuilder<AribContext>().UseSqlServer(cs).Options;
        return new AribContext(opts);
    }
}
