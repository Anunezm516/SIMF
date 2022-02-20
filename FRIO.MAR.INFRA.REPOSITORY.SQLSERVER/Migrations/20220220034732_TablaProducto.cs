using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    ProductoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<long>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioEliminacion = table.Column<long>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Nombre = table.Column<string>(type: "varchar(3)", nullable: true),
                    Codigo = table.Column<string>(type: "varchar(3)", nullable: true),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.ProductoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Producto");
        }
    }
}
