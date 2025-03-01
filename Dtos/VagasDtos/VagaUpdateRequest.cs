using System.ComponentModel.DataAnnotations;
using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Dtos.VagasDtos
{
    public class VagaUpdateRequest
    {
        public int? ProfissaoId { get; set; }
        public int? EditalId { get; set; }

        public VagaUpdateRequest()
        {
        }
        public VagaUpdateRequest(int? profissaoId, int? editalId)
        {
            this.ProfissaoId = profissaoId;
            this.EditalId = editalId;
        }
    }
}
