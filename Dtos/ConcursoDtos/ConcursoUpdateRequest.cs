using System.ComponentModel.DataAnnotations;
using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Dtos.ConcursoDtos
{
    public class ConcursoUpdateRequest
    {

        public string? CodigoDoConcurso { get; set; }

        public ConcursoUpdateRequest()
        {
        }

        public ConcursoUpdateRequest(int id, string codigoDoConcurso)
        {
            CodigoDoConcurso = codigoDoConcurso;
        }
    }
}
