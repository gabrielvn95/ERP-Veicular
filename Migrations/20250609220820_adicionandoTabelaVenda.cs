using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestVeicular.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoTabelaVenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Clientes_ClienteIdCliente",
                table: "Vendas");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Veiculos_VeiculoIdVeiculo",
                table: "Vendas");

            migrationBuilder.RenameColumn(
                name: "VeiculoIdVeiculo",
                table: "Vendas",
                newName: "VeiculoId");

            migrationBuilder.RenameColumn(
                name: "ClienteIdCliente",
                table: "Vendas",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendas_VeiculoIdVeiculo",
                table: "Vendas",
                newName: "IX_Vendas_VeiculoId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendas_ClienteIdCliente",
                table: "Vendas",
                newName: "IX_Vendas_ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Clientes_ClienteId",
                table: "Vendas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Veiculos_VeiculoId",
                table: "Vendas",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "IdVeiculo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Clientes_ClienteId",
                table: "Vendas");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Veiculos_VeiculoId",
                table: "Vendas");

            migrationBuilder.RenameColumn(
                name: "VeiculoId",
                table: "Vendas",
                newName: "VeiculoIdVeiculo");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Vendas",
                newName: "ClienteIdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_Vendas_VeiculoId",
                table: "Vendas",
                newName: "IX_Vendas_VeiculoIdVeiculo");

            migrationBuilder.RenameIndex(
                name: "IX_Vendas_ClienteId",
                table: "Vendas",
                newName: "IX_Vendas_ClienteIdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Clientes_ClienteIdCliente",
                table: "Vendas",
                column: "ClienteIdCliente",
                principalTable: "Clientes",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Veiculos_VeiculoIdVeiculo",
                table: "Vendas",
                column: "VeiculoIdVeiculo",
                principalTable: "Veiculos",
                principalColumn: "IdVeiculo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
