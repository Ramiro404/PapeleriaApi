using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PapeleriaApi.Migrations
{
    /// <inheritdoc />
    public partial class VendedorOrdenesDeCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VendedorId",
                table: "OrdenesDeCompra",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeCompra_VendedorId",
                table: "OrdenesDeCompra",
                column: "VendedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesDeCompra_Vendedores_VendedorId",
                table: "OrdenesDeCompra",
                column: "VendedorId",
                principalTable: "Vendedores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesDeCompra_Vendedores_VendedorId",
                table: "OrdenesDeCompra");

            migrationBuilder.DropIndex(
                name: "IX_OrdenesDeCompra_VendedorId",
                table: "OrdenesDeCompra");

            migrationBuilder.DropColumn(
                name: "VendedorId",
                table: "OrdenesDeCompra");
        }
    }
}
