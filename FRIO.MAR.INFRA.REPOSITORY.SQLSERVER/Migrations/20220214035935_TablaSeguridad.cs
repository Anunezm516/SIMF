using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaSeguridad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccesoUsuario",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Ip = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    IdUsuario = table.Column<long>(nullable: false),
                    IdCompania = table.Column<long>(nullable: true),
                    SitioWeb = table.Column<string>(unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccesoUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parametros",
                columns: table => new
                {
                    Codigo = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Valor = table.Column<string>(unicode: false, nullable: false),
                    Descripcion = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    UsuarioCreacion = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificacion = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioEliminacion = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametros", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    IdPermiso = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<long>(nullable: true),
                    Descripcion = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Url = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Ip = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioCreacion = table.Column<long>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<long>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioEliminacion = table.Column<long>(nullable: true),
                    Estado = table.Column<int>(nullable: true),
                    IdPadre = table.Column<long>(nullable: true),
                    NombreAbreviado = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Icono = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SPPermis__0D626EC845308855", x => x.IdPermiso);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    IdRol = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Ip = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioCreacion = table.Column<long>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<long>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioEliminacion = table.Column<long>(nullable: true),
                    Estado = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rol__2A49584CB35FDE7B", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    CorreoElectronico = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Nombre = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Apellido = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    IdTelegram = table.Column<string>(unicode: false, maxLength: 25, nullable: true),
                    FechaUltimaConexion = table.Column<DateTime>(type: "datetime", nullable: true),
                    IntentosFallidos = table.Column<int>(nullable: true),
                    FechaActualizarPassword = table.Column<DateTime>(type: "datetime", nullable: true),
                    Ip = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioCreacion = table.Column<long>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<long>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioEliminacion = table.Column<long>(nullable: true),
                    Estado = table.Column<int>(nullable: true),
                    Bloqueado = table.Column<bool>(nullable: true),
                    Password = table.Column<string>(unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SPUsuari__5B65BF97D476A28D", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "RolPermiso",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRol = table.Column<long>(nullable: true),
                    IdPermiso = table.Column<long>(nullable: true),
                    Estado = table.Column<bool>(nullable: true),
                    UsuarioEliminacion = table.Column<long>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioCreacion = table.Column<long>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermiso", x => x.Id);
                    table.ForeignKey(
                        name: "FK__RolPerm__IdPer__3F466844",
                        column: x => x.IdPermiso,
                        principalTable: "Permisos",
                        principalColumn: "IdPermiso",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__RolPerm__IdRol__3E52440B",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRol",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<long>(nullable: true),
                    IdRol = table.Column<long>(nullable: true),
                    Estado = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRol", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Usuario__IdRol__3C69FB99",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Usuario__IdUsu__3B75D760",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolPermiso_IdPermiso",
                table: "RolPermiso",
                column: "IdPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermiso_IdRol",
                table: "RolPermiso",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_IdRol",
                table: "UsuarioRol",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_IdUsuario",
                table: "UsuarioRol",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccesoUsuario");

            migrationBuilder.DropTable(
                name: "Parametros");

            migrationBuilder.DropTable(
                name: "RolPermiso");

            migrationBuilder.DropTable(
                name: "UsuarioRol");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
