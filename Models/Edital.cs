using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrilhaBackendLeds.Models
{
    public class Edital
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string NumeroDoEdital { get; set; } = string.Empty;

        [ForeignKey("Orgao")]
        public int OrgaoId { get; set; }
        public Orgao? Orgao { get; set; }

        [ForeignKey("Concurso")]
        public int ConcursoId { get; set; }
        public Concurso? Concurso { get; set; }

        public ICollection<Vaga> Vagas { get; set; } = new List<Vaga>();

        public Edital()
        {
        }

        public Edital(string numeroDoEdital, int orgaoId, int concursoId)
        {
            NumeroDoEdital = numeroDoEdital;
            OrgaoId = orgaoId;
            ConcursoId = concursoId;
        }

        public Edital(string numeroDoEdital, Orgao? orgao, Concurso? concurso)
        {
            NumeroDoEdital = numeroDoEdital;
            Orgao = orgao;
            Concurso = concurso;
        }

        public Edital(int id, string numeroDoEdital, Orgao? orgao, Concurso? concurso, ICollection<Vaga> vagas)
        {
            Id = id;
            NumeroDoEdital = numeroDoEdital;
            Orgao = orgao;
            Concurso = concurso;
            Vagas = vagas;
        }

        public override bool Equals(object? obj)
        {
            return obj is Edital edital &&
                   Id == edital.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
