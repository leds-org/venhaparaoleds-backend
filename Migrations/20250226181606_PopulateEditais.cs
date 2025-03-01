using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrilhaBackendLeds.Migrations
{
    /// <inheritdoc />
    public partial class PopulateEditais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into editais(NumeroDoEdital, OrgaoId, ConcursoId) Values('9/2016', 1, 1)");
            mb.Sql("Insert into editais(NumeroDoEdital, OrgaoId, ConcursoId) Values('15/2017', 2, 1)");
            mb.Sql("Insert into editais(NumeroDoEdital, OrgaoId, ConcursoId) Values('17/2017', 2, 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from editais where NumeroDoEdital = '9/2016' AND OrgaoId = 1 AND ConcursoId = 1");
            mb.Sql("Delete from editais where NumeroDoEdital = '15/2017' AND OrgaoId = 2 AND ConcursoId = 1");
            mb.Sql("Delete from editais where NumeroDoEdital = '17/2017' AND OrgaoId = 2 AND ConcursoId = 2");
        }
    }
}
