using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoldadosDoImperador.Migrations
{
    /// <inheritdoc />
    public partial class Refatoracao_Treinamento_MN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treinamentos_Soldados_SoldadoId",
                table: "Treinamentos");

            migrationBuilder.DropIndex(
                name: "IX_Treinamentos_SoldadoId",
                table: "Treinamentos");

            migrationBuilder.DropColumn(
                name: "SoldadoId",
                table: "Treinamentos");

            migrationBuilder.CreateTable(
                name: "TreinamentosParticipantes",
                columns: table => new
                {
                    TreinamentoId = table.Column<int>(type: "int", nullable: false),
                    SoldadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreinamentosParticipantes", x => new { x.TreinamentoId, x.SoldadoId });
                    table.ForeignKey(
                        name: "FK_TreinamentosParticipantes_Soldados_SoldadoId",
                        column: x => x.SoldadoId,
                        principalTable: "Soldados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TreinamentosParticipantes_Treinamentos_TreinamentoId",
                        column: x => x.TreinamentoId,
                        principalTable: "Treinamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreinamentosParticipantes_SoldadoId",
                table: "TreinamentosParticipantes",
                column: "SoldadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreinamentosParticipantes");

            migrationBuilder.AddColumn<int>(
                name: "SoldadoId",
                table: "Treinamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Treinamentos_SoldadoId",
                table: "Treinamentos",
                column: "SoldadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Treinamentos_Soldados_SoldadoId",
                table: "Treinamentos",
                column: "SoldadoId",
                principalTable: "Soldados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
