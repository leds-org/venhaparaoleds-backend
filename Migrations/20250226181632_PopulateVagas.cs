using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrilhaBackendLeds.Migrations
{
    /// <inheritdoc />
    public partial class PopulateVagas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into vagas(ProfissaoId, EditalId) Values(4,1)");
            mb.Sql("Insert into vagas(ProfissaoId, EditalId) Values(2,1)");
            mb.Sql("Insert into vagas(ProfissaoId, EditalId) Values(1,2)");
            mb.Sql("Insert into vagas(ProfissaoId, EditalId) Values(5,2)");
            mb.Sql("Insert into vagas(ProfissaoId, EditalId) Values(3,2)");
            mb.Sql("Insert into vagas(ProfissaoId, EditalId) Values(5,3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from vagas where ProfissaoId = 4 AND EditalId = 1");
            mb.Sql("Delete from vagas where ProfissaoId = 2 AND EditalId = 1");
            mb.Sql("Delete from vagas where ProfissaoId = 1 AND EditalId = 2");
            mb.Sql("Delete from vagas where ProfissaoId = 5 AND EditalId = 2");
            mb.Sql("Delete from vagas where ProfissaoId = 3 AND EditalId = 2");
            mb.Sql("Delete from vagas where ProfissaoId = 5 AND EditalId = 3");
        }
    }
}
