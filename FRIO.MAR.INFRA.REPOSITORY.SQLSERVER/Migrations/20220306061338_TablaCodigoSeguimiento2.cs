using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaCodigoSeguimiento2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "CodigoSeguimiento",
                type: "varchar(30)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "CodigoSeguimiento",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldNullable: true);
        }
    }
}
