using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaFormaPagoFactura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacturaFormaPago",
                columns: table => new
                {
                    FacturaFormaPagoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormaPagoId = table.Column<long>(nullable: false),
                    CodigoFormaPago = table.Column<string>(type: "varchar(20)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Observacion = table.Column<string>(type: "varchar(300)", nullable: true),
                    FacturaId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaFormaPago", x => x.FacturaFormaPagoId);
                    table.ForeignKey(
                        name: "FK_FacturaFormaPago_Factura_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Factura",
                        principalColumn: "FacturaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacturaFormaPago_FacturaId",
                table: "FacturaFormaPago",
                column: "FacturaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacturaFormaPago");
        }
    }
}
