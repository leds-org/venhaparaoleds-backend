using System.ComponentModel.DataAnnotations;
using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Dtos.VagasDtos
{
    public class VagaCreateRequest
    {
        [Required]
        public int ProfissaoId { get; set; }
        [Required]
        public int EditalId { get; set; }

        public VagaCreateRequest()
        {
        }

        public VagaCreateRequest(int profissaoId, int editalId)
        {
            ProfissaoId = profissaoId;
            EditalId = editalId;
        }
    }
}
