using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestVeicular.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Vendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Servicos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Servicos");
        }
    }
}
