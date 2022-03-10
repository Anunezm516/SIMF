using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaAdjuntosFactura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CFacturaAdjunto",
                columns: table => new
                {
                    FacturaAdjuntoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", nullable: true),
                    Ruta = table.Column<string>(type: "varchar(max)", nullable: true),
                    ImagenBase64 = table.Column<string>(type: "varchar(max)", nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    FacturaId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CFacturaAdjunto", x => x.FacturaAdjuntoId);
                    table.ForeignKey(
                        name: "FK_CFacturaAdjunto_CFactura_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "CFactura",
                        principalColumn: "FacturaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacturaAdjunto",
                columns: table => new
                {
                    FacturaAdjuntoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", nullable: true),
                    Ruta = table.Column<string>(type: "varchar(max)", nullable: true),
                    ImagenBase64 = table.Column<string>(type: "varchar(max)", nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    FacturaId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaAdjunto", x => x.FacturaAdjuntoId);
                    table.ForeignKey(
                        name: "FK_FacturaAdjunto_Factura_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Factura",
                        principalColumn: "FacturaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CFacturaAdjunto_FacturaId",
                table: "CFacturaAdjunto",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaAdjunto_FacturaId",
                table: "FacturaAdjunto",
                column: "FacturaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CFacturaAdjunto");

            migrationBuilder.DropTable(
                name: "FacturaAdjunto");
        }
    }
}
