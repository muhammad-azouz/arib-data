using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <summary>
    /// Per-branch opt-in that stops a sale drawing more stock than the warehouse has
    /// (Branch.PreventNegativeStock, default false = unchanged oversell behaviour).
    /// <para>
    /// Deliberately <b>no</b> Postgres counterpart and <b>no</b> SyncScope.SchemaVersion
    /// bump: Branches is cloud-authoritative and never DMS-synced (see SyncScope's v15/v17
    /// notes and Promotions.BranchId's comment), so a column added here changes no sync
    /// surface and needs no fleet flag-day. That is precisely why the flag lives on Branch
    /// rather than Warehouse, which is a Tier-B synced table and would have forced one.
    /// </para>
    /// </summary>
    public partial class AddPreventNegativeStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PreventNegativeStock",
                table: "Branches",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreventNegativeStock",
                table: "Branches");
        }
    }
}
