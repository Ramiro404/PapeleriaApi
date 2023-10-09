using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PapeleriaApi.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RolId",
                table: "Vendedores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendedores_RolId",
                table: "Vendedores",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendedores_Rol_RolId",
                table: "Vendedores",
                column: "RolId",
                principalTable: "Rol",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendedores_Rol_RolId",
                table: "Vendedores");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropIndex(
                name: "IX_Vendedores_RolId",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "RolId",
                table: "Vendedores");
        }
    }
}
