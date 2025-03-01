
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TrilhaBackendLeds.Models
{
    public class Profissao
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string Nome { get; set; } = string.Empty;

        public ICollection<CandidatoProfissao> CandidatoProfissoes { get; set; } = new List<CandidatoProfissao>();
        public ICollection<Vaga> Vagas { get; set; } = new List<Vaga>();

        public Profissao()
        {
        }

        public Profissao(string nome)
        {
            Nome = nome;
        }

        public Profissao(int id, string nome, ICollection<Candidato> candidatos, ICollection<Vaga> vagas)
        {
            Id = id;
            Nome = nome;
            Vagas = vagas;
        }

        public override bool Equals(object? obj)
        {
            return obj is Profissao profissao &&
                   Id == profissao.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
