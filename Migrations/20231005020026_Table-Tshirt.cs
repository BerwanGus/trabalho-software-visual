using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioRoupasAPI.Migrations
{
    /// <inheritdoc />
    public partial class TableTshirt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColarType",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColarType",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Products");
        }
    }
}
