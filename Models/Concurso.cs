using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrilhaBackendLeds.Models
{
    public class Concurso
    {
        public int Id { get; set; }

        [StringLength(11)]
        public string CodigoDoConcurso { get; set; } = string.Empty;

        public ICollection<Edital> Editais { get; set; } = new List<Edital>();

        public Concurso()
        {
        }

        public Concurso(string codigoDoConcurso)
        {
            CodigoDoConcurso = codigoDoConcurso;
        }

        public Concurso(int id, string codigoDoConcurso, ICollection<Edital> editais)
        {
            Id = id;
            CodigoDoConcurso = codigoDoConcurso;
            Editais = editais ?? new List<Edital>();
        }

        public override bool Equals(object? obj)
        {
            return obj is Concurso concurso &&
                   Id == concurso.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
