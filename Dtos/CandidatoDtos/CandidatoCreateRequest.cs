using System.ComponentModel.DataAnnotations;
using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Dtos.CandidatoDtos
{
    public class CandidatoCreateRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(255, ErrorMessage = "O nome deve conter no máximo 255 caracteres.")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(11,MinimumLength = 11, ErrorMessage = "O CPF deve conter 11 caracteres")]
        public string Cpf { get; set; } = string.Empty;
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateOnly DataDeNascimento { get; set; }

        public CandidatoCreateRequest()
        {
        }

        public CandidatoCreateRequest(string nome, string cpf, DateOnly dataDeNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataDeNascimento = dataDeNascimento;
        }
    }
}
