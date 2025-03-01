using System.ComponentModel.DataAnnotations;
using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Dtos.ConcursoDtos
{
    public class ConcursoCreateRequest
    {
        [Required(ErrorMessage = "O código do concurso é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O código do concurso deve ter 11 caracteres")]
        public string CodigoDoConcurso { get; set; } = string.Empty;

        public ConcursoCreateRequest()
        {
        }

        public ConcursoCreateRequest(string codigoDoConcurso)
        {
            CodigoDoConcurso = codigoDoConcurso;
        }
    }
}
