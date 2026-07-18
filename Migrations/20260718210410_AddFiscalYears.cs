using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class AddFiscalYears : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FiscalYears",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClosingRegNum = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NetProfit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalYears", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PostingAccounts",
                columns: new[] { "Role", "AccountId", "LabelAr", "LabelEn" },
                values: new object[] { "RetainedEarnings", new Guid("00000001-0000-7000-a000-000000000129"), "أرباح (خسائر) مرحلة", "Retained Earnings" });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYears_StartDate_EndDate",
                table: "FiscalYears",
                columns: new[] { "StartDate", "EndDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYears_Status",
                table: "FiscalYears",
                column: "Status");

            // Prerequisite fix (spec-fiscal-year.md Open Question 2): user-created
            // accounts never had Class set by UpsertAccountViewModel, so they carry
            // the default 0 — which the fiscal close's Revenue/Expense sweep can't
            // classify. Derive it the same way SeedData.cs does: leading digit of Num.
            migrationBuilder.Sql(@"
UPDATE Accounts
SET Class = CAST(LEFT(CAST(Num AS varchar(10)), 1) AS int)
WHERE Class = 0;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FiscalYears");

            migrationBuilder.DeleteData(
                table: "PostingAccounts",
                keyColumn: "Role",
                keyValue: "RetainedEarnings");
        }
    }
}
