using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class ProductoIVA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IVA",
                table: "Producto",
                newName: "IvaPorcentaje");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "TipoIdentificacion",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IvaCodigo",
                table: "Producto",
                type: "varchar(50)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IvaCodigo",
                table: "Producto");

            migrationBuilder.RenameColumn(
                name: "IvaPorcentaje",
                table: "Producto",
                newName: "IVA");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "TipoIdentificacion",
                type: "varchar(25)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);
        }
    }
}
