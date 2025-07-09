using System.ComponentModel.DataAnnotations;

namespace LedsAPI.Domain.Entities
{
    // Representa uma Vaga de Emprego, incluindo detalhes e profissões necessárias.
    public class EmpregoVaga
    {
        // Construtor padrão exigido pelo Entity Framework Core.
        public EmpregoVaga() { }
        // Construtor para criar uma nova instância de EmpregoVaga.
        public EmpregoVaga(string nomeVag, List<Concurso> concursos, List<Profissao> ProfissoesNecessarias)
        {
            this.Id = new Guid(); // Gera um novo ID único para a vaga.
            this.NomeVag = nomeVag;
            this.Concursos = concursos;
            this.ProfissoesNecessarias = ProfissoesNecessarias;
        }
        // Método para atualizar as informações de uma vaga de emprego existente.
        public void Atualizar(string nomeVag, List<Concurso> concursos, List<Profissao> ProfissoesNecessarias)
        {
            this.NomeVag = nomeVag;
            this.Concursos = concursos;
            this.ProfissoesNecessarias = ProfissoesNecessarias;
        }

        // Identificador único da vaga de emprego (chave primária).
        [Key]
        [Required]
        public Guid Id { get; private set; }
        // Nome ou descrição da vaga (ex: "Analista de Sistemas").
        [Required]
        public string NomeVag { get; private set; }
        // Lista de concursos aos quais esta vaga está associada.
        public List<Concurso> Concursos { get; private set; } = new List<Concurso>();
        // Lista de profissões exigidas para esta vaga.
        [Required]
        public List<Profissao> ProfissoesNecessarias { get; private set; } = new List<Profissao>();
    }
}