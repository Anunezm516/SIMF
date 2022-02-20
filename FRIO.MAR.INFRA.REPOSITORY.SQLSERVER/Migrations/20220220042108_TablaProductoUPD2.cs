using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaProductoUPD2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Producto",
                newName: "UnidadMedida");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Producto",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IVA",
                table: "Producto",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Producto",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modelo",
                table: "Producto",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoProducto",
                table: "Producto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TipoIdentificacion",
                columns: table => new
                {
                    TipoIdentificacionId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "varchar(5)", nullable: true),
                    Nombre = table.Column<string>(type: "varchar(25)", nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoIdentificacion", x => x.TipoIdentificacionId);
                });

            migrationBuilder.CreateTable(
                name: "UnidadMedida",
                columns: table => new
                {
                    UnidadMedidaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "varchar(5)", nullable: true),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: true),
                    Abreviatura = table.Column<string>(type: "varchar(25)", nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadMedida", x => x.UnidadMedidaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoIdentificacion");

            migrationBuilder.DropTable(
                name: "UnidadMedida");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "IVA",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Modelo",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "TipoProducto",
                table: "Producto");

            migrationBuilder.RenameColumn(
                name: "UnidadMedida",
                table: "Producto",
                newName: "Nombre");
        }
    }
}
