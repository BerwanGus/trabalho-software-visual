using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioRoupasAPI.Migrations
{
    /// <inheritdoc />
    public partial class PurchasesQuantityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Purchases_Quantity",
                table: "Clients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Purchases_Quantity",
                table: "Clients",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
