using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioRoupasAPI.Migrations
{
    /// <inheritdoc />
    public partial class SaleValueColumnAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "SaleValue",
                table: "ProductsSales",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleValue",
                table: "ProductsSales");
        }
    }
}
