using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LedsAPI.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidatos",
                columns: table => new
                {
                    CPF = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Nascimento = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatos", x => x.CPF);
                });

            migrationBuilder.CreateTable(
                name: "Concursos",
                columns: table => new
                {
                    CdConcurso = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Orgao = table.Column<string>(type: "TEXT", nullable: false),
                    Edital = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concursos", x => x.CdConcurso);
                });

            migrationBuilder.CreateTable(
                name: "Profissoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeProf = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vagas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeVag = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vagas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidatoProfissao",
                columns: table => new
                {
                    CandidatoCPF = table.Column<string>(type: "TEXT", nullable: false),
                    ProfissoesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidatoProfissao", x => new { x.CandidatoCPF, x.ProfissoesId });
                    table.ForeignKey(
                        name: "FK_CandidatoProfissao_Candidatos_CandidatoCPF",
                        column: x => x.CandidatoCPF,
                        principalTable: "Candidatos",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidatoProfissao_Profissoes_ProfissoesId",
                        column: x => x.ProfissoesId,
                        principalTable: "Profissoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConcursoEmpregoVaga",
                columns: table => new
                {
                    ConcursosCdConcurso = table.Column<long>(type: "INTEGER", nullable: false),
                    VagasId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcursoEmpregoVaga", x => new { x.ConcursosCdConcurso, x.VagasId });
                    table.ForeignKey(
                        name: "FK_ConcursoEmpregoVaga_Concursos_ConcursosCdConcurso",
                        column: x => x.ConcursosCdConcurso,
                        principalTable: "Concursos",
                        principalColumn: "CdConcurso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConcursoEmpregoVaga_Vagas_VagasId",
                        column: x => x.VagasId,
                        principalTable: "Vagas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VagaProfissao",
                columns: table => new
                {
                    EmpregoVagaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProfissoesNecessariasId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VagaProfissao", x => new { x.EmpregoVagaId, x.ProfissoesNecessariasId });
                    table.ForeignKey(
                        name: "FK_VagaProfissao_Profissoes_ProfissoesNecessariasId",
                        column: x => x.ProfissoesNecessariasId,
                        principalTable: "Profissoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VagaProfissao_Vagas_EmpregoVagaId",
                        column: x => x.EmpregoVagaId,
                        principalTable: "Vagas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidatoProfissao_ProfissoesId",
                table: "CandidatoProfissao",
                column: "ProfissoesId");

            migrationBuilder.CreateIndex(
                name: "IX_ConcursoEmpregoVaga_VagasId",
                table: "ConcursoEmpregoVaga",
                column: "VagasId");

            migrationBuilder.CreateIndex(
                name: "IX_Profissoes_NomeProf",
                table: "Profissoes",
                column: "NomeProf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VagaProfissao_ProfissoesNecessariasId",
                table: "VagaProfissao",
                column: "ProfissoesNecessariasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidatoProfissao");

            migrationBuilder.DropTable(
                name: "ConcursoEmpregoVaga");

            migrationBuilder.DropTable(
                name: "VagaProfissao");

            migrationBuilder.DropTable(
                name: "Candidatos");

            migrationBuilder.DropTable(
                name: "Concursos");

            migrationBuilder.DropTable(
                name: "Profissoes");

            migrationBuilder.DropTable(
                name: "Vagas");
        }
    }
}
