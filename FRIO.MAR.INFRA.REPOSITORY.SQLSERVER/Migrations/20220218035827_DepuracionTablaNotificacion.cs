using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class DepuracionTablaNotificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoTicket",
                table: "Notificaciones");

            migrationBuilder.DropColumn(
                name: "IdTicket",
                table: "Notificaciones");

            migrationBuilder.DropColumn(
                name: "IdTicketDetalle",
                table: "Notificaciones");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoTicket",
                table: "Notificaciones",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdTicket",
                table: "Notificaciones",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdTicketDetalle",
                table: "Notificaciones",
                type: "bigint",
                nullable: true);
        }
    }
}
