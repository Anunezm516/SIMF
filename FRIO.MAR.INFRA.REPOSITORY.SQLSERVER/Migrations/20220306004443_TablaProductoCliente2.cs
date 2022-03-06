using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaProductoCliente2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IvaCodigo",
                table: "ProductoCliente");

            migrationBuilder.DropColumn(
                name: "IvaPorcentaje",
                table: "ProductoCliente");

            migrationBuilder.DropColumn(
                name: "PrecioUnitario",
                table: "ProductoCliente");

            migrationBuilder.DropColumn(
                name: "TipoProducto",
                table: "ProductoCliente");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "ProductoCliente",
                type: "varchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "ProductoCliente",
                type: "varchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "ProductoCliente");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "ProductoCliente",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IvaCodigo",
                table: "ProductoCliente",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IvaPorcentaje",
                table: "ProductoCliente",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioUnitario",
                table: "ProductoCliente",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TipoProducto",
                table: "ProductoCliente",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
