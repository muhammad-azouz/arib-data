using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class BackfillLegacyInvoiceLineDiscriminator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // InvoiceLine is TPH, discriminated by a "Discriminator" string column pinned to
            // pre-rename class names (SaleEntry/PurchaseEntry/ReSaleEntry/RePurchaseEntry/
            // OrderEntry — see AribContext.OnModelCreating's own comment). Rows written before
            // the base class was renamed InvoiceLine (it was BillEntry) — or, in principle, by
            // EF's bare-base-type convention under the *current* name — never got their
            // Discriminator column updated to match, because that rename only changed how
            // *new* rows are tagged going forward. Any such row throws
            // InvalidOperationException("No discriminators matched...") the moment anything
            // materializes it as a typed entity — found live on a real customer database where
            // a quarter of all invoice lines (1,721 of 6,875) still carried "BillEntry",
            // silently ready to crash the first time anything touched an old invoice.
            //
            // Backfilled from the parent Invoice's own Type — an unambiguous, already-correct
            // source — not guessed. Order (300) has no OrderLine value pinned above, so it maps
            // to the base type's own current-name value ("InvoiceLine"); no legacy order rows
            // were observed, but the case is handled rather than left to throw a second way.
            migrationBuilder.Sql("""
                UPDATE l
                SET l.Discriminator = CASE i.Type
                    WHEN 100 THEN 'SaleEntry'
                    WHEN 101 THEN 'ReSaleEntry'
                    WHEN 200 THEN 'PurchaseEntry'
                    WHEN 201 THEN 'RePurchaseEntry'
                    ELSE 'InvoiceLine'
                END
                FROM InvoiceLines l
                INNER JOIN Invoices i ON i.Id = l.InvoiceId
                WHERE l.Discriminator IN ('BillEntry', 'InvoiceLine');
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Deliberately not reversed: which rows were "BillEntry" before Up ran is not
            // recoverable, and reverting to a value the current model can't materialize would
            // just reintroduce the crash this migration exists to fix.
        }
    }
}
