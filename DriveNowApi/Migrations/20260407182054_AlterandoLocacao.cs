using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveNowApi.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoLocacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "Locacoes");

            migrationBuilder.DropColumn(
                name: "IdVeiculo",
                table: "Locacoes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "Locacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdVeiculo",
                table: "Locacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
