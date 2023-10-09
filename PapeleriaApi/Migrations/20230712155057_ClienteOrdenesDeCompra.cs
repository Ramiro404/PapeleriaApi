using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PapeleriaApi.Migrations
{
    /// <inheritdoc />
    public partial class ClienteOrdenesDeCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "OrdenesDeCompra",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesDeCompra_ClienteId",
                table: "OrdenesDeCompra",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesDeCompra_Clientes_ClienteId",
                table: "OrdenesDeCompra",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesDeCompra_Clientes_ClienteId",
                table: "OrdenesDeCompra");

            migrationBuilder.DropIndex(
                name: "IX_OrdenesDeCompra_ClienteId",
                table: "OrdenesDeCompra");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "OrdenesDeCompra");
        }
    }
}
