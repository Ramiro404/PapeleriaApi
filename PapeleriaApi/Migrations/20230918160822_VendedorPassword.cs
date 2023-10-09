using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PapeleriaApi.Migrations
{
    /// <inheritdoc />
    public partial class VendedorPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApelidoPaterno",
                table: "Vendedores",
                newName: "ApellidoPaterno");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApellidoPaterno",
                table: "Vendedores",
                newName: "ApelidoPaterno");
        }
    }
}
