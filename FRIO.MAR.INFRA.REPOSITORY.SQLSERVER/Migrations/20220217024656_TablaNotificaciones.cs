using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaNotificaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    IdNotificacion = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    Mensaje = table.Column<string>(unicode: false, nullable: false),
                    TipoNotificacion = table.Column<int>(nullable: false),
                    TipoCriticidad = table.Column<int>(nullable: false),
                    EsNotificacionLeida = table.Column<bool>(nullable: false),
                    FechaNotificacionLeida = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdTicket = table.Column<long>(nullable: true),
                    IdTicketDetalle = table.Column<long>(nullable: true),
                    CodigoTicket = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    IdUsuario = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    Estado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVNotificacion", x => x.IdNotificacion);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notificaciones");
        }
    }
}
