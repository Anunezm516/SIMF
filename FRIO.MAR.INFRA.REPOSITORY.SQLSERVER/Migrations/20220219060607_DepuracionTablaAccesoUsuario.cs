using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class DepuracionTablaAccesoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCompania",
                table: "AccesoUsuario");

            migrationBuilder.AlterColumn<string>(
                name: "SitioWeb",
                table: "AccesoUsuario",
                unicode: false,
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SitioWeb",
                table: "AccesoUsuario",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdCompania",
                table: "AccesoUsuario",
                type: "bigint",
                nullable: true);
        }
    }
}
