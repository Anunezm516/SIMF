using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class DB : Migration
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
                    SitioWeb = table.Column<string>(unicode: false, maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccesoUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bodega",
                columns: table => new
                {
                    BodegaId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<string>(type: "varchar(15)", nullable: true),
                    UsuarioCreacion = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<long>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioEliminacion = table.Column<long>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Nombre = table.Column<string>(type: "varchar(100)", nullable: true),
                    Codigo = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodega", x => x.BodegaId);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<string>(type: "varchar(15)", nullable: true),
                    UsuarioCreacion = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<long>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioEliminacion = table.Column<long>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    TipoIdentificacion = table.Column<string>(type: "varchar(3)", nullable: true),
                    Identificacion = table.Column<string>(type: "varchar(25)", nullable: true),
                    RazonSocial = table.Column<string>(type: "varchar(100)", nullable: true),
                    NombreComercial = table.Column<string>(type: "varchar(100)", nullable: true),
                    Direccion = table.Column<string>(type: "varchar(300)", nullable: true),
                    CorreoElectronico = table.Column<string>(type: "varchar(100)", nullable: true),
                    Telefono = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

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
                    IdUsuario = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    Estado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVNotificacion", x => x.IdNotificacion);
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
                name: "Producto",
                columns: table => new
                {
                    ProductoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<string>(type: "varchar(15)", nullable: true),
                    UsuarioCreacion = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<long>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioEliminacion = table.Column<long>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    TipoProducto = table.Column<int>(nullable: false),
                    Codigo = table.Column<string>(type: "varchar(10)", nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(100)", nullable: true),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    IvaPorcentaje = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    IvaCodigo = table.Column<string>(type: "varchar(50)", nullable: true),
                    Marca = table.Column<string>(type: "varchar(50)", nullable: true),
                    Modelo = table.Column<string>(type: "varchar(50)", nullable: true),
                    UnidadMedida = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    ProveedorId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<string>(type: "varchar(15)", nullable: true),
                    UsuarioCreacion = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<long>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioEliminacion = table.Column<long>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    TipoIdentificacion = table.Column<string>(type: "varchar(3)", nullable: true),
                    Identificacion = table.Column<string>(type: "varchar(25)", nullable: true),
                    RazonSocial = table.Column<string>(type: "varchar(100)", nullable: true),
                    NombreComercial = table.Column<string>(type: "varchar(100)", nullable: true),
                    Direccion = table.Column<string>(type: "varchar(300)", nullable: true),
                    CorreoElectronico = table.Column<string>(type: "varchar(100)", nullable: true),
                    Telefono = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.ProveedorId);
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
                name: "Sucursal",
                columns: table => new
                {
                    SucursalId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<string>(type: "varchar(15)", nullable: true),
                    UsuarioCreacion = table.Column<long>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<long>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    UsuarioEliminacion = table.Column<long>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Nombre = table.Column<string>(type: "varchar(100)", nullable: true),
                    Codigo = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursal", x => x.SucursalId);
                });

            migrationBuilder.CreateTable(
                name: "TipoIdentificacion",
                columns: table => new
                {
                    TipoIdentificacionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "varchar(5)", nullable: true),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoIdentificacion", x => x.TipoIdentificacionId);
                });

            migrationBuilder.CreateTable(
                name: "TipoImpuesto",
                columns: table => new
                {
                    TipoImpuestoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: true),
                    Codigo = table.Column<string>(type: "varchar(4)", nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoImpuesto", x => x.TipoImpuestoId);
                });

            migrationBuilder.CreateTable(
                name: "UnidadMedida",
                columns: table => new
                {
                    UnidadMedidaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Simbolo = table.Column<string>(type: "varchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "varchar(max)", nullable: true),
                    Comentario = table.Column<string>(type: "varchar(max)", nullable: true),
                    Estado = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadMedida", x => x.UnidadMedidaId);
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
                name: "SucursalBodega",
                columns: table => new
                {
                    SucursalBodegaId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SucursalId = table.Column<long>(nullable: false),
                    BodegaId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SucursalBodega", x => x.SucursalBodegaId);
                    table.ForeignKey(
                        name: "FK_SucursalBodega_Bodega_BodegaId",
                        column: x => x.BodegaId,
                        principalTable: "Bodega",
                        principalColumn: "BodegaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SucursalBodega_Sucursal_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursal",
                        principalColumn: "SucursalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Impuesto",
                columns: table => new
                {
                    ImpuestoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: true),
                    Porcentaje = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    Estado = table.Column<bool>(nullable: false),
                    TipoImpuestoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impuesto", x => x.ImpuestoId);
                    table.ForeignKey(
                        name: "FK_Impuesto_TipoImpuesto_TipoImpuestoId",
                        column: x => x.TipoImpuestoId,
                        principalTable: "TipoImpuesto",
                        principalColumn: "TipoImpuestoId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Impuesto_TipoImpuestoId",
                table: "Impuesto",
                column: "TipoImpuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermiso_IdPermiso",
                table: "RolPermiso",
                column: "IdPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermiso_IdRol",
                table: "RolPermiso",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_SucursalBodega_BodegaId",
                table: "SucursalBodega",
                column: "BodegaId");

            migrationBuilder.CreateIndex(
                name: "IX_SucursalBodega_SucursalId",
                table: "SucursalBodega",
                column: "SucursalId");

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
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Correo");

            migrationBuilder.DropTable(
                name: "Impuesto");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "Parametros");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "RolPermiso");

            migrationBuilder.DropTable(
                name: "SucursalBodega");

            migrationBuilder.DropTable(
                name: "TipoIdentificacion");

            migrationBuilder.DropTable(
                name: "UnidadMedida");

            migrationBuilder.DropTable(
                name: "UsuarioRol");

            migrationBuilder.DropTable(
                name: "TipoImpuesto");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Bodega");

            migrationBuilder.DropTable(
                name: "Sucursal");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
