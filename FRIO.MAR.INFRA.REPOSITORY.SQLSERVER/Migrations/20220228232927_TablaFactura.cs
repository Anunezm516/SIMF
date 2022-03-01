using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaFactura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CodigoTransferencia",
                table: "InventarioMovimientoSalida",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodigoTransferencia",
                table: "InventarioMovimientoEntrada",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    FacturaId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<long>(nullable: false),
                    ClienteId = table.Column<long>(nullable: false),
                    NumeroDocumento = table.Column<string>(type: "varchar(100)", nullable: true),
                    Identificacion = table.Column<string>(type: "varchar(25)", nullable: true),
                    RazonSocial = table.Column<string>(type: "varchar(100)", nullable: true),
                    NombreComercial = table.Column<string>(type: "varchar(100)", nullable: true),
                    Telefono = table.Column<string>(type: "varchar(25)", nullable: true),
                    CorreoElectronico = table.Column<string>(type: "varchar(100)", nullable: true),
                    FechaEmision = table.Column<DateTime>(nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Ip = table.Column<string>(type: "varchar(25)", nullable: true),
                    Estado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factura", x => x.FacturaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoTransferencia",
                table: "InventarioMovimientoSalida",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodigoTransferencia",
                table: "InventarioMovimientoEntrada",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
