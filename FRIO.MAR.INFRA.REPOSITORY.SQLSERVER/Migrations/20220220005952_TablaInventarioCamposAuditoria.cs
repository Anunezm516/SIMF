using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaInventarioCamposAuditoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Sucursal",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Sucursal",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Sucursal",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEliminacion",
                table: "Sucursal",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Sucursal",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "Sucursal",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UsuarioCreacion",
                table: "Sucursal",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UsuarioEliminacion",
                table: "Sucursal",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UsuarioModificacion",
                table: "Sucursal",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Bodega",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Bodega",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Bodega",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEliminacion",
                table: "Bodega",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Bodega",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "Bodega",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UsuarioCreacion",
                table: "Bodega",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UsuarioEliminacion",
                table: "Bodega",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UsuarioModificacion",
                table: "Bodega",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Sucursal");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Sucursal");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Sucursal");

            migrationBuilder.DropColumn(
                name: "FechaEliminacion",
                table: "Sucursal");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Sucursal");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "Sucursal");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "Sucursal");

            migrationBuilder.DropColumn(
                name: "UsuarioEliminacion",
                table: "Sucursal");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "Sucursal");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Bodega");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Bodega");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Bodega");

            migrationBuilder.DropColumn(
                name: "FechaEliminacion",
                table: "Bodega");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Bodega");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "Bodega");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "Bodega");

            migrationBuilder.DropColumn(
                name: "UsuarioEliminacion",
                table: "Bodega");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "Bodega");
        }
    }
}
