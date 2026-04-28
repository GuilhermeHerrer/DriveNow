using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveNowApi.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoFotos2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_Agencias_AgenciaId",
                table: "Veiculos");

            migrationBuilder.AlterColumn<int>(
                name: "AgenciaId",
                table: "Veiculos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_Agencias_AgenciaId",
                table: "Veiculos",
                column: "AgenciaId",
                principalTable: "Agencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_Agencias_AgenciaId",
                table: "Veiculos");

            migrationBuilder.AlterColumn<int>(
                name: "AgenciaId",
                table: "Veiculos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_Agencias_AgenciaId",
                table: "Veiculos",
                column: "AgenciaId",
                principalTable: "Agencias",
                principalColumn: "Id");
        }
    }
}
