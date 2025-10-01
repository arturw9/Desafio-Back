using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelasTitulosParcelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Titulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroTitulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeDevedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CpfDevedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PercentualJuros = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PercentualMulta = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parcelas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroParcela = table.Column<int>(type: "int", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorParcela = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TituloModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcelas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcelas_Titulos_TituloModelId",
                        column: x => x.TituloModelId,
                        principalTable: "Titulos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parcelas_TituloModelId",
                table: "Parcelas",
                column: "TituloModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parcelas");

            migrationBuilder.DropTable(
                name: "Titulos");
        }
    }
}
