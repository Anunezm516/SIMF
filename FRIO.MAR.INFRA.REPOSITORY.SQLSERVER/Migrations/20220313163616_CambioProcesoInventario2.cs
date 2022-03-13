using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class CambioProcesoInventario2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCompania",
                table: "InventarioConfiguracionesGenerales");

            migrationBuilder.AlterColumn<bool>(
                name: "DescontarStockAutomatico",
                table: "InventarioConfiguracionesGenerales",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ControlInventarioSucursal",
                table: "InventarioConfiguracionesGenerales",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ControlInventarioEmision",
                table: "InventarioConfiguracionesGenerales",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "DescontarStockAutomatico",
                table: "InventarioConfiguracionesGenerales",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "ControlInventarioSucursal",
                table: "InventarioConfiguracionesGenerales",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "ControlInventarioEmision",
                table: "InventarioConfiguracionesGenerales",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<long>(
                name: "IdCompania",
                table: "InventarioConfiguracionesGenerales",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
