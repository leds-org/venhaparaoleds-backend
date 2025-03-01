using System.ComponentModel.DataAnnotations;
using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Dtos.CandidatoDtos
{
    public class CandidatoUpdateRequest
    {
        [StringLength(255, ErrorMessage = "O nome deve conter no máximo 255 caracteres.")]
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public DateOnly DataDeNascimento { get; set; }

        public CandidatoUpdateRequest()
        {
        }

        public CandidatoUpdateRequest(string nome, string cpf, DateOnly dataDeNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataDeNascimento = dataDeNascimento;
        }
    }
}
