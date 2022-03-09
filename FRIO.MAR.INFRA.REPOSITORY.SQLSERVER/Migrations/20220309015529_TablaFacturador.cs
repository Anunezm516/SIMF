using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaFacturador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facturador",
                columns: table => new
                {
                    FacturadorId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "varchar(100)", nullable: true),
                    Identificacion = table.Column<string>(type: "varchar(20)", nullable: true),
                    TipoIdentificacion = table.Column<int>(nullable: false),
                    Telefono = table.Column<string>(type: "varchar(50)", nullable: true),
                    CorreoElectronico = table.Column<string>(type: "varchar(100)", nullable: true),
                    Direccion = table.Column<string>(type: "varchar(500)", nullable: true),
                    Logo = table.Column<string>(type: "varchar(max)", nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturador", x => x.FacturadorId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facturador");
        }
    }
}
