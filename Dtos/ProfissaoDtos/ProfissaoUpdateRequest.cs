using System.ComponentModel.DataAnnotations;

namespace TrilhaBackendLeds.Dtos.ProfissaoDtos
{
    public class ProfissaoUpdateRequest
    {
        [StringLength(255, ErrorMessage = "O nome da profissão deve conter no máximo 255 caracteres.")]
        public string? Nome { get; set; } = string.Empty;

        public ProfissaoUpdateRequest() { }

        public ProfissaoUpdateRequest(string nome)
        {
            Nome = nome;
        }
    }
}
