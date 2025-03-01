using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrilhaBackendLeds.Migrations
{
    /// <inheritdoc />
    public partial class PopulateConcursos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into concursos(CodigoDoConcurso) Values('61828450843')");
            mb.Sql("Insert into concursos(CodigoDoConcurso) Values('95655123539')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from concursos where CodigoDoConcurso = '61828450843'");
            mb.Sql("Delete from concursos where CodigoDoConcurso = '95655123539'");
        }
    }
}
