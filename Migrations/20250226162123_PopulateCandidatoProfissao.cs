using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrilhaBackendLeds.Migrations
{
    /// <inheritdoc />
    public partial class PopulateCandidatoProfissao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into candidato_profissao(CandidatoId, ProfissaoId) Values(1, 1) ");
            mb.Sql("Insert into candidato_profissao(CandidatoId, ProfissaoId) Values(2, 2) ");
            mb.Sql("Insert into candidato_profissao(CandidatoId, ProfissaoId) Values(2, 3) ");
            mb.Sql("Insert into candidato_profissao(CandidatoId, ProfissaoId) Values(3, 1) ");
            mb.Sql("Insert into candidato_profissao(CandidatoId, ProfissaoId) Values(3, 2) ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from candidato_profissao where CandidatoId = 1 and ProfissaoId = 1");
            mb.Sql("Delete from candidato_profissao where CandidatoId = 2 and ProfissaoId = 2");
            mb.Sql("Delete from candidato_profissao where CandidatoId = 2 and ProfissaoId = 3");
            mb.Sql("Delete from candidato_profissao where CandidatoId = 3 and ProfissaoId = 1");
            mb.Sql("Delete from candidato_profissao where CandidatoId = 3 and ProfissaoId = 2");
        }
    }
}
