using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrilhaBackendLeds.Migrations
{
    /// <inheritdoc />
    public partial class PopulateProfissoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into profissoes(Nome) Values('carpinteiro')");
            mb.Sql("Insert into profissoes(Nome) Values('marceneiro')");
            mb.Sql("Insert into profissoes(Nome) Values('assistente administrativo')");
            mb.Sql("Insert into profissoes(Nome) Values('analista de sistemas')");
            mb.Sql("Insert into profissoes(Nome) Values('professor de matemática')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from profissoes where Nome in ('carpinteiro', 'marceneiro', 'assistente administrativo', 'analista de sistemas', 'professor de matemática')");
        }
    }
}
