using System.ComponentModel.DataAnnotations;

namespace LedsAPI.Domain.Entities
{
    // Representa a entidade Concurso, detalhando as informações de um concurso público.
    public class Concurso
    {
        // Construtor padrão.
        public Concurso() { }
        // Construtor para criar uma nova instância de Concurso.
        public Concurso(long cdConcurso, string orgao, string edital, List<EmpregoVaga> vagas)
        {
            this.CdConcurso = cdConcurso;
            this.Orgao = orgao;
            this.Edital = edital;
            this.Vagas = vagas;
        }
        // Método para atualizar as informações de um concurso existente.
        public void Atualizar(string orgao, string edital, List<EmpregoVaga> vagas)
        {
            this.Orgao = orgao;
            this.Edital = edital;
            this.Vagas = vagas;
        }

        // Código do concurso, utilizado como chave primária.
        [Key]
        [Required]
        public long CdConcurso { get; private set; }
        // Nome do órgão responsável pelo concurso.
        [Required]
        public string Orgao { get; private set; }
        // Número ou identificação do edital do concurso.
        [Required]
        public string Edital { get; private set; }
        // Lista de vagas de emprego associadas a este concurso.
        [Required]
        public List<EmpregoVaga> Vagas { get; private set; } = new List<EmpregoVaga>();
    }
}