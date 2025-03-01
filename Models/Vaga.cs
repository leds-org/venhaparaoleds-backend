using System.Collections.ObjectModel;

namespace TrilhaBackendLeds.Models
{
    public class Vaga
    {
        public int Id { get; set; }
        public int ProfissaoId { get; set; }
        public Profissao? Profissao { get; set; }
        public int EditalId { get; set; }
        public Edital? Edital { get; set; }
        public Vaga() 
        { 
        }

        public Vaga(int profissaoId, int editalId)
        {
            ProfissaoId = profissaoId;
            EditalId = editalId;
        }

        public Vaga(Profissao? profissao, Edital? edital)
        {
            this.Profissao = profissao;
            this.Edital = edital;
        }

        public Vaga(int id, Profissao? profissao, Edital? edital)
        {
            Id = id;
            this.Profissao = profissao;
            this.Edital = edital;
        }

        public override bool Equals(object? obj)
        {
            return obj is Vaga vaga &&
                   Id == vaga.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
