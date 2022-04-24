using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class AddTipoPersona : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoPersona",
                table: "Proveedores",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoPersona",
                table: "Clientes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoPersona",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "TipoPersona",
                table: "Clientes");
        }
    }
}
