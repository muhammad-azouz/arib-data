using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryEnabled = table.Column<bool>(type: "bit", nullable: false),
                    ExpiryEnabled = table.Column<bool>(type: "bit", nullable: false),
                    FinanceEnabled = table.Column<bool>(type: "bit", nullable: false),
                    SystemEnabled = table.Column<bool>(type: "bit", nullable: false),
                    ExpiryDaysAhead = table.Column<int>(type: "int", nullable: false),
                    InstallmentDueSoonDays = table.Column<int>(type: "int", nullable: false),
                    BackupStaleDays = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSettings_BranchId",
                table: "NotificationSettings",
                column: "BranchId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationSettings");
        }
    }
}
