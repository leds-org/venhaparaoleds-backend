using TrilhaBackendLeds.Dtos.EditaisDtos;
using TrilhaBackendLeds.Dtos.ProfissaoDtos;
using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Dtos.VagasDtos
{
    public class VagaResponse
    {
        public int Id { get; set; }

        public string NomeDaProfissao { get; set; }

        public string NumeroDoEdital { get; set; }

        public string CodigoDoConcurso { get; set; }

        public VagaResponse()
        {
        }

        public VagaResponse(int id, string nomeDaProfissao, string numeroDoEdital, string codigoDoConcurso)
        {
            Id = id;
            NomeDaProfissao = nomeDaProfissao;
            NumeroDoEdital = numeroDoEdital;
            CodigoDoConcurso = codigoDoConcurso;
        }
    }
}
