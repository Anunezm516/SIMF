using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaInventario2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCompania",
                table: "InventarioVenta");

            migrationBuilder.DropColumn(
                name: "IdCompania",
                table: "InventarioProveedor");

            migrationBuilder.DropColumn(
                name: "IdCompania",
                table: "InventarioMovimientoSalida");

            migrationBuilder.DropColumn(
                name: "IdCompania",
                table: "InventarioMovimientoEntrada");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioModificacion",
                table: "InventarioVenta",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioCreacion",
                table: "InventarioVenta",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "InventarioVenta",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Estado",
                table: "InventarioVenta",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioModificacion",
                table: "InventarioProveedor",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioCreacion",
                table: "InventarioProveedor",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "InventarioProveedor",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Estado",
                table: "InventarioProveedor",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioModificacion",
                table: "InventarioMovimientoSalida",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioCreacion",
                table: "InventarioMovimientoSalida",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "InventarioMovimientoSalida",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Estado",
                table: "InventarioMovimientoSalida",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioModificacion",
                table: "InventarioMovimientoEntrada",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioCreacion",
                table: "InventarioMovimientoEntrada",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "InventarioMovimientoEntrada",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Estado",
                table: "InventarioMovimientoEntrada",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "UsuarioModificacion",
                table: "InventarioVenta",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioCreacion",
                table: "InventarioVenta",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "InventarioVenta",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<bool>(
                name: "Estado",
                table: "InventarioVenta",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<long>(
                name: "IdCompania",
                table: "InventarioVenta",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioModificacion",
                table: "InventarioProveedor",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioCreacion",
                table: "InventarioProveedor",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "InventarioProveedor",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<bool>(
                name: "Estado",
                table: "InventarioProveedor",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<long>(
                name: "IdCompania",
                table: "InventarioProveedor",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioModificacion",
                table: "InventarioMovimientoSalida",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioCreacion",
                table: "InventarioMovimientoSalida",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "InventarioMovimientoSalida",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<bool>(
                name: "Estado",
                table: "InventarioMovimientoSalida",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<long>(
                name: "IdCompania",
                table: "InventarioMovimientoSalida",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioModificacion",
                table: "InventarioMovimientoEntrada",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioCreacion",
                table: "InventarioMovimientoEntrada",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaCreacion",
                table: "InventarioMovimientoEntrada",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<bool>(
                name: "Estado",
                table: "InventarioMovimientoEntrada",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<long>(
                name: "IdCompania",
                table: "InventarioMovimientoEntrada",
                type: "bigint",
                nullable: true);
        }
    }
}
