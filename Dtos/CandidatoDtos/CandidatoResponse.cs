
using TrilhaBackendLeds.Dtos.ProfissaoDtos;
using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Dtos.CandidatoDtos
{
    public class CandidatoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public DateOnly DataDeNascimento { get; set; }
        public ICollection<ProfissaoResponse> Profissoes { get; set; } = new List<ProfissaoResponse>();

        public CandidatoResponse()
        {
        }
        public CandidatoResponse(int id, string nome, string cpf, DateOnly dataDeNascimento)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            DataDeNascimento = dataDeNascimento;
        }

        public CandidatoResponse(int id, string nome, string cpf, DateOnly dataDeNascimento, ICollection<ProfissaoResponse> profissoes)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            DataDeNascimento = dataDeNascimento;
            Profissoes = profissoes;
        }
    }
}

