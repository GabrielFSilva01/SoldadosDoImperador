using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoldadosDoImperador.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoldadoId",
                table: "Treinamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoldadoId",
                table: "Ordens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoldadoId",
                table: "Missoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoldadoId",
                table: "ItensBatalha",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Treinamentos_SoldadoId",
                table: "Treinamentos",
                column: "SoldadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordens_SoldadoId",
                table: "Ordens",
                column: "SoldadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Missoes_SoldadoId",
                table: "Missoes",
                column: "SoldadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensBatalha_SoldadoId",
                table: "ItensBatalha",
                column: "SoldadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensBatalha_Soldados_SoldadoId",
                table: "ItensBatalha",
                column: "SoldadoId",
                principalTable: "Soldados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Missoes_Soldados_SoldadoId",
                table: "Missoes",
                column: "SoldadoId",
                principalTable: "Soldados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ordens_Soldados_SoldadoId",
                table: "Ordens",
                column: "SoldadoId",
                principalTable: "Soldados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Treinamentos_Soldados_SoldadoId",
                table: "Treinamentos",
                column: "SoldadoId",
                principalTable: "Soldados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensBatalha_Soldados_SoldadoId",
                table: "ItensBatalha");

            migrationBuilder.DropForeignKey(
                name: "FK_Missoes_Soldados_SoldadoId",
                table: "Missoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ordens_Soldados_SoldadoId",
                table: "Ordens");

            migrationBuilder.DropForeignKey(
                name: "FK_Treinamentos_Soldados_SoldadoId",
                table: "Treinamentos");

            migrationBuilder.DropIndex(
                name: "IX_Treinamentos_SoldadoId",
                table: "Treinamentos");

            migrationBuilder.DropIndex(
                name: "IX_Ordens_SoldadoId",
                table: "Ordens");

            migrationBuilder.DropIndex(
                name: "IX_Missoes_SoldadoId",
                table: "Missoes");

            migrationBuilder.DropIndex(
                name: "IX_ItensBatalha_SoldadoId",
                table: "ItensBatalha");

            migrationBuilder.DropColumn(
                name: "SoldadoId",
                table: "Treinamentos");

            migrationBuilder.DropColumn(
                name: "SoldadoId",
                table: "Ordens");

            migrationBuilder.DropColumn(
                name: "SoldadoId",
                table: "Missoes");

            migrationBuilder.DropColumn(
                name: "SoldadoId",
                table: "ItensBatalha");
        }
    }
}
