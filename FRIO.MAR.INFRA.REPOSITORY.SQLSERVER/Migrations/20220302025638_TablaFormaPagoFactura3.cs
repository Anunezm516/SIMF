using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaFormaPagoFactura3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescripcionFormaPago",
                table: "FacturaFormaPago",
                type: "varchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescripcionFormaPago",
                table: "FacturaFormaPago");
        }
    }
}
