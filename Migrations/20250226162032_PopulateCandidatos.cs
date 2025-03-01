using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrilhaBackendLeds.Migrations
{
    /// <inheritdoc />
    public partial class PopulateCandidatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into candidatos(Nome, DataDeNascimento, Cpf) Values('Lindsey Craft','1976-05-19','18284508434')");
            mb.Sql("Insert into candidatos(Nome, DataDeNascimento, Cpf) Values('Jackie Dawson','1970-08-14','31166797347')");
            mb.Sql("Insert into candidatos(Nome, DataDeNascimento, Cpf) Values('Cory Mendoza','1957-02-11','56551235392')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from candidatos where Cpf = '18284508434'");
            mb.Sql("Delete from candidatos where Cpf = '31166797347'");
            mb.Sql("Delete from candidatos where Cpf = '56551235392'");
        }
    }
}
