using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class ProductoIVA2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abreviatura",
                table: "UnidadMedida");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "UnidadMedida");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "UnidadMedida",
                type: "varchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comentario",
                table: "UnidadMedida",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Simbolo",
                table: "UnidadMedida",
                type: "varchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comentario",
                table: "UnidadMedida");

            migrationBuilder.DropColumn(
                name: "Simbolo",
                table: "UnidadMedida");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "UnidadMedida",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Abreviatura",
                table: "UnidadMedida",
                type: "varchar(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "UnidadMedida",
                type: "varchar(5)",
                nullable: true);
        }
    }
}
