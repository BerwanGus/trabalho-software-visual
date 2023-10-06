using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioRoupasAPI.Migrations
{
    /// <inheritdoc />
    public partial class Change_ProductType_and_Brand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ColarType",
                table: "Products",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "Products",
                newName: "BrandId");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Products",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TypeName = table.Column<string>(type: "TEXT", nullable: false),
                    Style = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Products",
                newName: "ColarType");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Products",
                newName: "Brand");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
