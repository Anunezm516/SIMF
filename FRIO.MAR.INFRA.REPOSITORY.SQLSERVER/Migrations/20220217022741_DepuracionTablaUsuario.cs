using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class DepuracionTablaUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bloqueado",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "IdTelegram",
                table: "Usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Bloqueado",
                table: "Usuario",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdTelegram",
                table: "Usuario",
                type: "varchar(25)",
                unicode: false,
                maxLength: 25,
                nullable: true);
        }
    }
}
