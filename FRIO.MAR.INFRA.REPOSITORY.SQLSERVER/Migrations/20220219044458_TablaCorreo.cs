using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaCorreo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Correo",
                columns: table => new
                {
                    IdCorreo = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaIngreso = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaEnvio = table.Column<DateTime>(type: "datetime", nullable: true),
                    Correos = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Copias = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Asunto = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Mensaje = table.Column<string>(unicode: false, maxLength: 5000, nullable: true),
                    Tipo = table.Column<int>(nullable: true),
                    Adjunto = table.Column<int>(nullable: true),
                    Error = table.Column<string>(nullable: true),
                    Estado = table.Column<int>(nullable: true),
                    IntentosEnvio = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Correo__872F8EAEDC7FA84E", x => x.IdCorreo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Correo");
        }
    }
}
