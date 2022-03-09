using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaFacturador2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Facturador",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEliminacion",
                table: "Facturador",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Facturador",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "Facturador",
                type: "varchar(15)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UsuarioCreacion",
                table: "Facturador",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UsuarioEliminacion",
                table: "Facturador",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UsuarioModificacion",
                table: "Facturador",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Facturador");

            migrationBuilder.DropColumn(
                name: "FechaEliminacion",
                table: "Facturador");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Facturador");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "Facturador");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "Facturador");

            migrationBuilder.DropColumn(
                name: "UsuarioEliminacion",
                table: "Facturador");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "Facturador");
        }
    }
}
