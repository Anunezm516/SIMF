using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablasComprasFactura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CFactura",
                columns: table => new
                {
                    FacturaId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<long>(nullable: false),
                    ProveedorId = table.Column<long>(nullable: false),
                    SucursalId = table.Column<long>(nullable: false),
                    NumeroDocumento = table.Column<string>(type: "varchar(100)", nullable: true),
                    Identificacion = table.Column<string>(type: "varchar(25)", nullable: true),
                    RazonSocial = table.Column<string>(type: "varchar(100)", nullable: true),
                    NombreComercial = table.Column<string>(type: "varchar(100)", nullable: true),
                    Telefono = table.Column<string>(type: "varchar(25)", nullable: true),
                    CorreoElectronico = table.Column<string>(type: "varchar(100)", nullable: true),
                    FechaEmision = table.Column<DateTime>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Ip = table.Column<string>(type: "varchar(25)", nullable: true),
                    Estado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CFactura", x => x.FacturaId);
                });

            migrationBuilder.CreateTable(
                name: "CFacturaDetalle",
                columns: table => new
                {
                    FacturaDetalleId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<long>(nullable: false),
                    SucursalId = table.Column<long>(nullable: false),
                    BodegaId = table.Column<long>(nullable: false),
                    Codigo = table.Column<string>(type: "varchar(25)", nullable: true),
                    CodigoSeguimiento = table.Column<string>(type: "varchar(30)", nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(300)", nullable: true),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    IvaPorcentaje = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    IvaValor = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    FacturaId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CFacturaDetalle", x => x.FacturaDetalleId);
                    table.ForeignKey(
                        name: "FK_CFacturaDetalle_CFactura_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "CFactura",
                        principalColumn: "FacturaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CFacturaFormaPago",
                columns: table => new
                {
                    FacturaFormaPagoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormaPagoId = table.Column<long>(nullable: false),
                    CodigoFormaPago = table.Column<string>(type: "varchar(20)", nullable: true),
                    DescripcionFormaPago = table.Column<string>(type: "varchar(100)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Observacion = table.Column<string>(type: "varchar(300)", nullable: true),
                    FacturaId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CFacturaFormaPago", x => x.FacturaFormaPagoId);
                    table.ForeignKey(
                        name: "FK_CFacturaFormaPago_CFactura_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "CFactura",
                        principalColumn: "FacturaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CFacturaDetalle_FacturaId",
                table: "CFacturaDetalle",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_CFacturaFormaPago_FacturaId",
                table: "CFacturaFormaPago",
                column: "FacturaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CFacturaDetalle");

            migrationBuilder.DropTable(
                name: "CFacturaFormaPago");

            migrationBuilder.DropTable(
                name: "CFactura");
        }
    }
}
