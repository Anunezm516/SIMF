using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaProductoCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Producto",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProductoCliente",
                columns: table => new
                {
                    ProductoClienteId = table.Column<long>(nullable: false)
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
                    Codigo = table.Column<string>(type: "varchar(20)", nullable: true),
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
                    table.PrimaryKey("PK_ProductoCliente", x => x.ProductoClienteId);
                });

            migrationBuilder.CreateTable(
                name: "ProductoClienteImagen",
                columns: table => new
                {
                    ProductoClienteImagenId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", nullable: true),
                    Ruta = table.Column<string>(type: "varchar(max)", nullable: true),
                    ImagenBase64 = table.Column<string>(type: "varchar(max)", nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    ProductoClienteId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoClienteImagen", x => x.ProductoClienteImagenId);
                    table.ForeignKey(
                        name: "FK_ProductoClienteImagen_ProductoCliente_ProductoClienteId",
                        column: x => x.ProductoClienteId,
                        principalTable: "ProductoCliente",
                        principalColumn: "ProductoClienteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductoClienteImagen_ProductoClienteId",
                table: "ProductoClienteImagen",
                column: "ProductoClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductoClienteImagen");

            migrationBuilder.DropTable(
                name: "ProductoCliente");

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Producto",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);
        }
    }
}
