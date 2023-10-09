using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PapeleriaApi.Migrations
{
    /// <inheritdoc />
    public partial class ProductoOrdenesDeCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "OrdenesDeCompra",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeCompra_ProductoId",
                table: "OrdenesDeCompra",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesDeCompra_Productos_ProductoId",
                table: "OrdenesDeCompra",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesDeCompra_Productos_ProductoId",
                table: "OrdenesDeCompra");

            migrationBuilder.DropIndex(
                name: "IX_OrdenesDeCompra_ProductoId",
                table: "OrdenesDeCompra");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "OrdenesDeCompra");
        }
    }
}
