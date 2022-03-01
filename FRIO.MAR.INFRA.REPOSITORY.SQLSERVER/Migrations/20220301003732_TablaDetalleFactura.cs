using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaDetalleFactura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacturaDetalle",
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
                    table.PrimaryKey("PK_FacturaDetalle", x => x.FacturaDetalleId);
                    table.ForeignKey(
                        name: "FK_FacturaDetalle_Factura_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Factura",
                        principalColumn: "FacturaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacturaDetalle_FacturaId",
                table: "FacturaDetalle",
                column: "FacturaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacturaDetalle");
        }
    }
}
