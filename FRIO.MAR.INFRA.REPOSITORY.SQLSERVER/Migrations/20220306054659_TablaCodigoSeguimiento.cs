using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaCodigoSeguimiento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CodigoSeguimiento",
                columns: table => new
                {
                    CodigoSeguimientoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(nullable: true),
                    ProductoClienteId = table.Column<long>(nullable: false),
                    FechaRegistro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodigoSeguimiento", x => x.CodigoSeguimientoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodigoSeguimiento");
        }
    }
}
