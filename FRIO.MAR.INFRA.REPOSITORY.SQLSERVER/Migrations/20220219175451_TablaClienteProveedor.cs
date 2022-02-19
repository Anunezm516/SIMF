using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaClienteProveedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<long>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioEliminacion = table.Column<long>(nullable: false),
                    FechaEliminacion = table.Column<DateTime>(nullable: false),
                    Estado = table.Column<bool>(nullable: false),
                    TipoIdentificacion = table.Column<int>(nullable: false),
                    Identificacion = table.Column<string>(maxLength: 25, nullable: true),
                    RazonSocial = table.Column<string>(maxLength: 100, nullable: true),
                    NombreComercial = table.Column<string>(maxLength: 100, nullable: true),
                    Direccion = table.Column<string>(maxLength: 300, nullable: true),
                    CorreoElectronico = table.Column<string>(maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    ProveedorId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<long>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    UsuarioEliminacion = table.Column<long>(nullable: false),
                    FechaEliminacion = table.Column<DateTime>(nullable: false),
                    Estado = table.Column<bool>(nullable: false),
                    TipoIdentificacion = table.Column<int>(nullable: false),
                    Identificacion = table.Column<string>(maxLength: 25, nullable: true),
                    RazonSocial = table.Column<string>(maxLength: 100, nullable: true),
                    NombreComercial = table.Column<string>(maxLength: 100, nullable: true),
                    Direccion = table.Column<string>(maxLength: 300, nullable: true),
                    CorreoElectronico = table.Column<string>(maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.ProveedorId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Proveedores");
        }
    }
}
