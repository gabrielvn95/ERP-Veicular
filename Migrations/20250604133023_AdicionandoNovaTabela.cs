using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestVeicular.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoNovaTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Clientes_ClienteIdCliente",
                table: "Servicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Veiculos_VeiculoIdVeiculo",
                table: "Servicos");

            migrationBuilder.RenameColumn(
                name: "VeiculoIdVeiculo",
                table: "Servicos",
                newName: "VeiculoId");

            migrationBuilder.RenameColumn(
                name: "ClienteIdCliente",
                table: "Servicos",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Servicos_VeiculoIdVeiculo",
                table: "Servicos",
                newName: "IX_Servicos_VeiculoId");

            migrationBuilder.RenameIndex(
                name: "IX_Servicos_ClienteIdCliente",
                table: "Servicos",
                newName: "IX_Servicos_ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Clientes_ClienteId",
                table: "Servicos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Veiculos_VeiculoId",
                table: "Servicos",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "IdVeiculo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Clientes_ClienteId",
                table: "Servicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Veiculos_VeiculoId",
                table: "Servicos");

            migrationBuilder.RenameColumn(
                name: "VeiculoId",
                table: "Servicos",
                newName: "VeiculoIdVeiculo");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Servicos",
                newName: "ClienteIdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_Servicos_VeiculoId",
                table: "Servicos",
                newName: "IX_Servicos_VeiculoIdVeiculo");

            migrationBuilder.RenameIndex(
                name: "IX_Servicos_ClienteId",
                table: "Servicos",
                newName: "IX_Servicos_ClienteIdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Clientes_ClienteIdCliente",
                table: "Servicos",
                column: "ClienteIdCliente",
                principalTable: "Clientes",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Veiculos_VeiculoIdVeiculo",
                table: "Servicos",
                column: "VeiculoIdVeiculo",
                principalTable: "Veiculos",
                principalColumn: "IdVeiculo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
