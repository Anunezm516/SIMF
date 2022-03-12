using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class CamposSecuencial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PuntoEmision",
                table: "Factura",
                type: "varchar(5)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Secuencial",
                table: "Factura",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Sucursal",
                table: "Factura",
                type: "varchar(5)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PuntoEmision",
                table: "Factura");

            migrationBuilder.DropColumn(
                name: "Secuencial",
                table: "Factura");

            migrationBuilder.DropColumn(
                name: "Sucursal",
                table: "Factura");
        }
    }
}
