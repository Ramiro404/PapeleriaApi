using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PapeleriaApi.Migrations
{
    /// <inheritdoc />
    public partial class IdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Vendedores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Vendedores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Vendedores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Vendedores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Vendedores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Vendedores",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Vendedores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Vendedores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Vendedores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Vendedores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Vendedores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Vendedores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Vendedores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Vendedores",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Vendedores");
        }
    }
}
