using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventarioRoupasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRelations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Products",
                newName: "Type_Id");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Products",
                newName: "Brand_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                newName: "IX_Products_Brand_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Type_Id",
                table: "Products",
                column: "Type_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_Brand_Id",
                table: "Products",
                column: "Brand_Id",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_Type_Id",
                table: "Products",
                column: "Type_Id",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_Brand_Id",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_Type_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Type_Id",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Type_Id",
                table: "Products",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "Brand_Id",
                table: "Products",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Brand_Id",
                table: "Products",
                newName: "IX_Products_BrandId");

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeId",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id");
        }
    }
}
