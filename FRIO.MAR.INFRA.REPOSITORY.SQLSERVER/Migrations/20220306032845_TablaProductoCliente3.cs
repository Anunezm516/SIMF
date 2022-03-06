using Microsoft.EntityFrameworkCore.Migrations;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Migrations
{
    public partial class TablaProductoCliente3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClienteId",
                table: "ProductoCliente",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ProductoCliente_ClienteId",
                table: "ProductoCliente",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoCliente_Clientes_ClienteId",
                table: "ProductoCliente",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductoCliente_Clientes_ClienteId",
                table: "ProductoCliente");

            migrationBuilder.DropIndex(
                name: "IX_ProductoCliente_ClienteId",
                table: "ProductoCliente");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "ProductoCliente");
        }
    }
}
