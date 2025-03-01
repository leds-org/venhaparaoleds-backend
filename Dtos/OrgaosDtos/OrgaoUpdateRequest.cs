using System.ComponentModel.DataAnnotations;

namespace TrilhaBackendLeds.Dtos.OrgaosDtos
{
    public class OrgaoUpdateRequest
    {
        [StringLength(50, ErrorMessage = "O nome do órgão pode ter no máximo 50 caracteres")]
        public string? Nome { get; set; } = string.Empty;

        public OrgaoUpdateRequest()
        {
        }

        public OrgaoUpdateRequest(string nome)
        {
            Nome = nome;
        }
    }
}
