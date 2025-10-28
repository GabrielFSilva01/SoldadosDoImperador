using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoldadosDoImperador.Migrations
{
    /// <inheritdoc />
    public partial class Refatoracao_Missao_MN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missoes_Soldados_SoldadoId",
                table: "Missoes");

            migrationBuilder.DropIndex(
                name: "IX_Missoes_SoldadoId",
                table: "Missoes");

            migrationBuilder.DropColumn(
                name: "SoldadoId",
                table: "Missoes");

            migrationBuilder.AlterColumn<decimal>(
                name: "Peso",
                table: "ItensBatalha",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldPrecision: 6,
                oldScale: 3);

            migrationBuilder.CreateTable(
                name: "MissoesParticipantes",
                columns: table => new
                {
                    MissaoId = table.Column<int>(type: "int", nullable: false),
                    SoldadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissoesParticipantes", x => new { x.MissaoId, x.SoldadoId });
                    table.ForeignKey(
                        name: "FK_MissoesParticipantes_Missoes_MissaoId",
                        column: x => x.MissaoId,
                        principalTable: "Missoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MissoesParticipantes_Soldados_SoldadoId",
                        column: x => x.SoldadoId,
                        principalTable: "Soldados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissoesParticipantes_SoldadoId",
                table: "MissoesParticipantes",
                column: "SoldadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissoesParticipantes");

            migrationBuilder.AddColumn<int>(
                name: "SoldadoId",
                table: "Missoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Peso",
                table: "ItensBatalha",
                type: "decimal(6,3)",
                precision: 6,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Missoes_SoldadoId",
                table: "Missoes",
                column: "SoldadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Missoes_Soldados_SoldadoId",
                table: "Missoes",
                column: "SoldadoId",
                principalTable: "Soldados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
