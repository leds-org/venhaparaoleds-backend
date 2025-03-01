using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace TrilhaBackendLeds.Models
{
    public class Candidato
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string Nome { get; set; } = string.Empty;

        public DateOnly DataDeNascimento { get; set; }

        [StringLength(11)]
        public string Cpf { get; set; } = string.Empty;

        public ICollection<CandidatoProfissao> CandidatoProfissoes { get; set; } = new List<CandidatoProfissao>();

        public Candidato()
        {
            
        }

        public Candidato(string nome, DateOnly dataDeNascimento, string cpf)
        {
            Nome = nome;
            DataDeNascimento = dataDeNascimento;
            Cpf = cpf;
        }

        public Candidato(int id, string nome, DateOnly dataDeNascimento, string cpf, ICollection<CandidatoProfissao> candidatoProfissoes)
        {
            Id = id;
            Nome = nome;
            DataDeNascimento = dataDeNascimento;
            Cpf = cpf;
            CandidatoProfissoes = candidatoProfissoes;
        }

        public override bool Equals(object? obj)
        {
            return obj is Candidato candidato &&
                   Id == candidato.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
