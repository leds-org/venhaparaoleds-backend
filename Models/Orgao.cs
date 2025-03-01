using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace TrilhaBackendLeds.Models
{
    public class Orgao
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Nome { get; set; } = string.Empty;
        public ICollection<Edital> Editais { get; set; } = new List<Edital>();

        public Orgao()
        {
        }
        public Orgao(string nome)
        {
            Nome = nome;
        }

        public Orgao(int id, string nome, ICollection<Edital> editais)
        {
            Id = id;
            Nome = nome;
            Editais = editais ?? new List<Edital>();
        }

        public override bool Equals(object? obj)
        {
            return obj is Orgao orgao &&
                   Id == orgao.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
