using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioATS.Data.Migrations
{
    public partial class DesafioInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidaturas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    RecrutadorId = table.Column<string>(type: "char(36)", nullable: false),
                    VagaId = table.Column<string>(type: "char(36)", nullable: false),
                    TituloVaga = table.Column<string>(type: "varchar(100)", nullable: false),
                    CandidatoId = table.Column<string>(type: "char(36)", nullable: false),
                    CandidatoNome = table.Column<string>(type: "varchar(200)", nullable: false),
                    CandidatoEmail = table.Column<string>(type: "varchar(200)", nullable: false),
                    CandidatoTelefone = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidaturas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Curriculos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(100)", nullable: false),
                    CandidatoId = table.Column<string>(type: "char(36)", nullable: false),
                    CandidatoNome = table.Column<string>(type: "varchar(200)", nullable: false),
                    Resumo = table.Column<string>(type: "text", nullable: true),
                    FotoPerfil = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vagas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    RecrutadorId = table.Column<string>(type: "char(36)", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(100)", nullable: false),
                    RequitosTecnicos = table.Column<string>(type: "text", nullable: false),
                    RequitosDesejaveis = table.Column<string>(type: "text", nullable: true),
                    Atividades = table.Column<string>(type: "text", nullable: true),
                    TipoContrato = table.Column<int>(type: "int", nullable: false),
                    Remuneracao = table.Column<decimal>(type: "decimal(18, 2)", precision: 18, scale: 2, nullable: false),
                    TipoRemuneracao = table.Column<int>(type: "int", nullable: false),
                    TipoJornada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vagas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExperienciaProfissional",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    CurriculoId = table.Column<string>(type: "char(36)", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(100)", nullable: false),
                    Empresa = table.Column<string>(type: "varchar(200)", nullable: false),
                    Localidade = table.Column<string>(type: "varchar(100)", nullable: false),
                    Resumo = table.Column<string>(type: "text", nullable: true),
                    Inicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    Fim = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienciaProfissional", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienciaProfissional_Curriculo_CurriculoId",
                        column: x => x.CurriculoId,
                        principalTable: "Curriculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormacaoAcademica",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(36)", nullable: false),
                    CurriculoId = table.Column<string>(type: "char(36)", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(100)", nullable: false),
                    Instituicao = table.Column<string>(type: "varchar(200)", nullable: false),
                    Inicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    Fim = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormacaoAcademica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormacaoAcademica_Curriculo_CurriculoId",
                        column: x => x.CurriculoId,
                        principalTable: "Curriculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExperienciaProfissional_CurriculoId",
                table: "ExperienciaProfissional",
                column: "CurriculoId");

            migrationBuilder.CreateIndex(
                name: "IX_FormacaoAcademica_CurriculoId",
                table: "FormacaoAcademica",
                column: "CurriculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidaturas");

            migrationBuilder.DropTable(
                name: "ExperienciaProfissional");

            migrationBuilder.DropTable(
                name: "FormacaoAcademica");

            migrationBuilder.DropTable(
                name: "Vagas");

            migrationBuilder.DropTable(
                name: "Curriculos");
        }
    }
}
