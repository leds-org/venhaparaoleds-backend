using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Dtos.EditaisDtos
{
    public class EditalCreateRequest
    {
        [Required]
        [StringLength(20, ErrorMessage = "O número do edital deve conter no máximo 20 caracteres")]
        public string NumeroDoEdital { get; set; } = string.Empty;
        [Required]
        public int OrgaoId { get; set; }
        [Required]
        public int ConcursoId { get; set; }

        public EditalCreateRequest()
        {
        }

        public EditalCreateRequest(string numeroDoEdital, int orgaoId, int concursoId)
        {
            NumeroDoEdital = numeroDoEdital;
            OrgaoId = orgaoId;
            ConcursoId = concursoId;
        }
    }
}
