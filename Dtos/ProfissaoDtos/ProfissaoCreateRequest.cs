using System.ComponentModel.DataAnnotations;

namespace TrilhaBackendLeds.Dtos.ProfissaoDtos
{
    public class ProfissaoCreateRequest
    {
        [Required]
        [StringLength(255, ErrorMessage = "O nome da profissão deve conter no máximo 255 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        public ProfissaoCreateRequest() { }

        public ProfissaoCreateRequest(string nome)
        {
            Nome = nome;
        }
    }
}
