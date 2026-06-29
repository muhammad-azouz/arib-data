using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class DropDeadNotificationColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppNotifications_BranchId_IsRead_IsResolved",
                table: "AppNotifications");

            migrationBuilder.DropIndex(
                name: "IX_AppNotifications_BranchId_IsResolved_IsDismissed_CreatedAt",
                table: "AppNotifications");

            migrationBuilder.DropColumn(
                name: "IsDismissed",
                table: "AppNotifications");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "AppNotifications");

            migrationBuilder.CreateIndex(
                name: "IX_AppNotifications_BranchId_IsResolved_CreatedAt",
                table: "AppNotifications",
                columns: new[] { "BranchId", "IsResolved", "CreatedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppNotifications_BranchId_IsResolved_CreatedAt",
                table: "AppNotifications");

            migrationBuilder.AddColumn<bool>(
                name: "IsDismissed",
                table: "AppNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "AppNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_AppNotifications_BranchId_IsRead_IsResolved",
                table: "AppNotifications",
                columns: new[] { "BranchId", "IsRead", "IsResolved" });

            migrationBuilder.CreateIndex(
                name: "IX_AppNotifications_BranchId_IsResolved_IsDismissed_CreatedAt",
                table: "AppNotifications",
                columns: new[] { "BranchId", "IsResolved", "IsDismissed", "CreatedAt" });
        }
    }
}
