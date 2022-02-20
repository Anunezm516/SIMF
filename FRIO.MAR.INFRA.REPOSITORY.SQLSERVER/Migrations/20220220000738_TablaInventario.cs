using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaInventario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Impuesto_TipoImpuesto_TipoImpuestoId",
                table: "Impuesto");

            migrationBuilder.AlterColumn<int>(
                name: "TipoImpuestoId",
                table: "Impuesto",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Bodega",
                columns: table => new
                {
                    BodegaId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodega", x => x.BodegaId);
                });

            migrationBuilder.CreateTable(
                name: "Sucursal",
                columns: table => new
                {
                    SucursalId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursal", x => x.SucursalId);
                });

            migrationBuilder.CreateTable(
                name: "SucursalBodega",
                columns: table => new
                {
                    SucursalBodegaId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SucursalId = table.Column<long>(nullable: false),
                    BodegaId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SucursalBodega", x => x.SucursalBodegaId);
                    table.ForeignKey(
                        name: "FK_SucursalBodega_Bodega_BodegaId",
                        column: x => x.BodegaId,
                        principalTable: "Bodega",
                        principalColumn: "BodegaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SucursalBodega_Sucursal_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursal",
                        principalColumn: "SucursalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SucursalBodega_BodegaId",
                table: "SucursalBodega",
                column: "BodegaId");

            migrationBuilder.CreateIndex(
                name: "IX_SucursalBodega_SucursalId",
                table: "SucursalBodega",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Impuesto_TipoImpuesto_TipoImpuestoId",
                table: "Impuesto",
                column: "TipoImpuestoId",
                principalTable: "TipoImpuesto",
                principalColumn: "TipoImpuestoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Impuesto_TipoImpuesto_TipoImpuestoId",
                table: "Impuesto");

            migrationBuilder.DropTable(
                name: "SucursalBodega");

            migrationBuilder.DropTable(
                name: "Bodega");

            migrationBuilder.DropTable(
                name: "Sucursal");

            migrationBuilder.AlterColumn<int>(
                name: "TipoImpuestoId",
                table: "Impuesto",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Impuesto_TipoImpuesto_TipoImpuestoId",
                table: "Impuesto",
                column: "TipoImpuestoId",
                principalTable: "TipoImpuesto",
                principalColumn: "TipoImpuestoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
