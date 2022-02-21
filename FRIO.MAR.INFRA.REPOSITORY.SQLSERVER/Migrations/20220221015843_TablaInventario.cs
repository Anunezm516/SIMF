using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaInventario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "SucursalBodega",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "SucursalBodega",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEliminacion",
                table: "SucursalBodega",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "SucursalBodega",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "SucursalBodega",
                type: "varchar(15)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UsuarioCreacion",
                table: "SucursalBodega",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UsuarioEliminacion",
                table: "SucursalBodega",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UsuarioModificacion",
                table: "SucursalBodega",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SucursalId",
                table: "Bodega",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InventarioConfiguracionesGenerales",
                columns: table => new
                {
                    IdInventarioConfiguracionesGenerales = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCompania = table.Column<long>(nullable: false),
                    DescontarStockAutomatico = table.Column<bool>(nullable: true),
                    ControlInventarioSucursal = table.Column<bool>(nullable: true),
                    ControlInventarioEmision = table.Column<bool>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Invent__2D482365639E2D77", x => x.IdInventarioConfiguracionesGenerales);
                });

            migrationBuilder.CreateTable(
                name: "InventarioMovimientoEntrada",
                columns: table => new
                {
                    IdInventarioMovimiento = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<long>(nullable: true),
                    IdInventarioBodega = table.Column<long>(nullable: true),
                    IdSucursal = table.Column<long>(nullable: true),
                    IdProveedor = table.Column<long>(nullable: true),
                    Cantidad = table.Column<decimal>(type: "decimal(18, 6)", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18, 6)", nullable: true),
                    NumeroFactura = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Cufe = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Motivo = table.Column<string>(unicode: false, maxLength: 5000, nullable: true),
                    CodigoTransferencia = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    TipoInventario = table.Column<int>(nullable: true),
                    TipoMovimiento = table.Column<int>(nullable: true),
                    IdCompania = table.Column<long>(nullable: true),
                    IP = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    UsuarioCreacion = table.Column<long>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<long>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioEliminacion = table.Column<long>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    Estado = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Invent__4D66004A55C73884", x => x.IdInventarioMovimiento);
                    table.ForeignKey(
                        name: "FK__Inventa__IdInv__0AFD888E",
                        column: x => x.IdInventarioBodega,
                        principalTable: "Bodega",
                        principalColumn: "BodegaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Inventa__IdPro__0A096455",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Inventa__IdSuc__0BF1ACC7",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "SucursalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventarioMovimientoSalida",
                columns: table => new
                {
                    IdInventarioMovimiento = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<long>(nullable: true),
                    IdInventarioBodega = table.Column<long>(nullable: true),
                    IdSucursal = table.Column<long>(nullable: true),
                    IdCliente = table.Column<long>(nullable: true),
                    Cantidad = table.Column<decimal>(type: "decimal(18, 6)", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18, 6)", nullable: true),
                    NumeroFactura = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Cufe = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Motivo = table.Column<string>(unicode: false, maxLength: 5000, nullable: true),
                    CodigoTransferencia = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    TipoInventario = table.Column<int>(nullable: true),
                    TipoMovimiento = table.Column<int>(nullable: true),
                    IdCompania = table.Column<long>(nullable: true),
                    IP = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    UsuarioCreacion = table.Column<long>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<long>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioEliminacion = table.Column<long>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    Estado = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Invent__4D66004AE5E880D4", x => x.IdInventarioMovimiento);
                    table.ForeignKey(
                        name: "FK__Inventa__IdInv__0638D371",
                        column: x => x.IdInventarioBodega,
                        principalTable: "Bodega",
                        principalColumn: "BodegaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Inventa__IdPro__0544AF38",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Inventa__IdSuc__072CF7AA",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "SucursalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventarioProveedor",
                columns: table => new
                {
                    IdInventarioProveedor = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<long>(nullable: true),
                    IdInventarioBodega = table.Column<long>(nullable: true),
                    UnidadMedida = table.Column<string>(unicode: false, maxLength: 5, nullable: true),
                    CantidadDescripcion = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    StockActual = table.Column<decimal>(type: "decimal(18, 6)", nullable: true),
                    IdCompania = table.Column<long>(nullable: true),
                    IP = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    UsuarioCreacion = table.Column<long>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<long>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioEliminacion = table.Column<long>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    Estado = table.Column<bool>(nullable: true),
                    IdSucursal = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Invent__17576659E0B70AEB", x => x.IdInventarioProveedor);
                    table.ForeignKey(
                        name: "FK__Inventa__IdInv__1392CE8F",
                        column: x => x.IdInventarioBodega,
                        principalTable: "Bodega",
                        principalColumn: "BodegaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Inventa__IdPro__129EAA56",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Inventa__IdSuc__22D5121F",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "SucursalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventarioVenta",
                columns: table => new
                {
                    IdInventarioVenta = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<long>(nullable: true),
                    IdInventarioBodega = table.Column<long>(nullable: true),
                    UnidadMedida = table.Column<string>(unicode: false, maxLength: 5, nullable: true),
                    CantidadDescripcion = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    StockActual = table.Column<decimal>(type: "decimal(18, 6)", nullable: true),
                    IdCompania = table.Column<long>(nullable: true),
                    IP = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    UsuarioCreacion = table.Column<long>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioModificacion = table.Column<long>(nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioEliminacion = table.Column<long>(nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    Estado = table.Column<bool>(nullable: true),
                    IdSucursal = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Invent__3FAD48D7775D9CA1", x => x.IdInventarioVenta);
                    table.ForeignKey(
                        name: "FK__Inventa__IdInv__0FC23DAB",
                        column: x => x.IdInventarioBodega,
                        principalTable: "Bodega",
                        principalColumn: "BodegaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Inventa__IdPro__0ECE1972",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Inventa__IdSuc__21E0EDE6",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "SucursalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bodega_SucursalId",
                table: "Bodega",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioMovimientoEntrada_IdInventarioBodega",
                table: "InventarioMovimientoEntrada",
                column: "IdInventarioBodega");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioMovimientoEntrada_IdProducto",
                table: "InventarioMovimientoEntrada",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioMovimientoEntrada_IdSucursal",
                table: "InventarioMovimientoEntrada",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioMovimientoSalida_IdInventarioBodega",
                table: "InventarioMovimientoSalida",
                column: "IdInventarioBodega");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioMovimientoSalida_IdProducto",
                table: "InventarioMovimientoSalida",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioMovimientoSalida_IdSucursal",
                table: "InventarioMovimientoSalida",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioProveedor_IdInventarioBodega",
                table: "InventarioProveedor",
                column: "IdInventarioBodega");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioProveedor_IdProducto",
                table: "InventarioProveedor",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioProveedor_IdSucursal",
                table: "InventarioProveedor",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioVenta_IdInventarioBodega",
                table: "InventarioVenta",
                column: "IdInventarioBodega");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioVenta_IdProducto",
                table: "InventarioVenta",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioVenta_IdSucursal",
                table: "InventarioVenta",
                column: "IdSucursal");

            migrationBuilder.AddForeignKey(
                name: "FK_Bodega_Sucursal_SucursalId",
                table: "Bodega",
                column: "SucursalId",
                principalTable: "Sucursal",
                principalColumn: "SucursalId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bodega_Sucursal_SucursalId",
                table: "Bodega");

            migrationBuilder.DropTable(
                name: "InventarioConfiguracionesGenerales");

            migrationBuilder.DropTable(
                name: "InventarioMovimientoEntrada");

            migrationBuilder.DropTable(
                name: "InventarioMovimientoSalida");

            migrationBuilder.DropTable(
                name: "InventarioProveedor");

            migrationBuilder.DropTable(
                name: "InventarioVenta");

            migrationBuilder.DropIndex(
                name: "IX_Bodega_SucursalId",
                table: "Bodega");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "SucursalBodega");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "SucursalBodega");

            migrationBuilder.DropColumn(
                name: "FechaEliminacion",
                table: "SucursalBodega");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "SucursalBodega");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "SucursalBodega");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacion",
                table: "SucursalBodega");

            migrationBuilder.DropColumn(
                name: "UsuarioEliminacion",
                table: "SucursalBodega");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacion",
                table: "SucursalBodega");

            migrationBuilder.DropColumn(
                name: "SucursalId",
                table: "Bodega");
        }
    }
}
