using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Dtos.ConcursoDtos
{
    public class ConcursoResponse
    {
        public int Id { get; set; }
        public string CodigoDoConcurso { get; set; } = string.Empty;


        public ConcursoResponse()
        {
        }

        public ConcursoResponse(int id, string codigoDoConcurso)
        {
            Id = id;
            CodigoDoConcurso = codigoDoConcurso;
        }
    }
}
