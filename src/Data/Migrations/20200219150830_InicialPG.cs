using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace data.Migrations
{
    public partial class InicialPG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuscaConsolidada",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UrlProduto = table.Column<string>(maxLength: 255, nullable: false),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    Descricao = table.Column<string>(maxLength: 255, nullable: false),
                    Preco = table.Column<decimal>(nullable: false),
                    PrecoOrdenacao = table.Column<int>(nullable: false),
                    UrlImagem = table.Column<string>(maxLength: 255, nullable: false),
                    Categoria = table.Column<string>(maxLength: 50, nullable: false),
                    Origem = table.Column<string>(maxLength: 50, nullable: false),
                    Tamanhos = table.Column<string>(maxLength: 255, nullable: false),
                    MenorPreco = table.Column<bool>(nullable: false),
                    MaiorPreco = table.Column<bool>(nullable: false),
                    ProdutoNovo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuscaConsolidada", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistroBusca",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataHora = table.Column<DateTime>(nullable: false),
                    NomeSiteOrigem = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroBusca", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roupas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    Descricao = table.Column<string>(maxLength: 255, nullable: false),
                    Preco = table.Column<decimal>(nullable: false),
                    UrlProduto = table.Column<string>(maxLength: 255, nullable: false),
                    UrlImagem = table.Column<string>(maxLength: 255, nullable: false),
                    Categoria = table.Column<string>(maxLength: 50, nullable: false),
                    BuscaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roupas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roupas_RegistroBusca_BuscaId",
                        column: x => x.BuscaId,
                        principalTable: "RegistroBusca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoupasTamanho",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoupaId = table.Column<int>(nullable: false),
                    Tamanho = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoupasTamanho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoupasTamanho_Roupas_RoupaId",
                        column: x => x.RoupaId,
                        principalTable: "Roupas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roupas_BuscaId",
                table: "Roupas",
                column: "BuscaId");

            migrationBuilder.CreateIndex(
                name: "IX_RoupasTamanho_RoupaId",
                table: "RoupasTamanho",
                column: "RoupaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuscaConsolidada");

            migrationBuilder.DropTable(
                name: "RoupasTamanho");

            migrationBuilder.DropTable(
                name: "Roupas");

            migrationBuilder.DropTable(
                name: "RegistroBusca");
        }
    }
}
