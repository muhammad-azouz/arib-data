using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AribONE.Migrations
{
    /// <inheritdoc />
    public partial class AddProductCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductCode",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("""
                WITH Numbered AS (
                    SELECT Id, ROW_NUMBER() OVER (ORDER BY CreatedAt ASC) AS Seq
                    FROM Products
                )
                UPDATE Products
                SET Products.ProductCode = Numbered.Seq
                FROM Products
                INNER JOIN Numbered ON Numbered.Id = Products.Id;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "Products");
        }
    }
}
