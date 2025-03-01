using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrilhaBackendLeds.Migrations
{
    /// <inheritdoc />
    public partial class PopulateOrgaos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into orgaos(Nome) Values('SEDU')");
            mb.Sql("Insert into orgaos(Nome) Values('SEJUS')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from orgaos where Nome = 'SEDU'");
            mb.Sql("Delete from orgaos where Nome = 'SEJUS'");
        }
    }
}
