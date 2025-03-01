using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Dtos.EditaisDtos
{
    public class EditalUpdateRequest
    {

        [StringLength(9)]
        public string? NumeroDoEdital { get; set; }
        public int? OrgaoId { get; set; }
        public int? ConcursoId { get; set; }

        public EditalUpdateRequest() { }

        public EditalUpdateRequest(string? numeroDoEdital, int? orgaoId, int? concursoId)
        {
            NumeroDoEdital = numeroDoEdital;
            OrgaoId = orgaoId;
            ConcursoId = concursoId;
        }
    }
}
