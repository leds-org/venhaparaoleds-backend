using System.ComponentModel.DataAnnotations;

namespace LedsAPI.Domain.Entities
{
    // Representa a entidade Profissão.
    public class Profissao
    {
        // Construtor padrão exigido pelo Entity Framework Core.
        public Profissao() { }
        // Construtor para criar uma nova instância de Profissão.
        public Profissao(string nomeProf)
        {
            this.Id = new Guid(); // Gera um novo ID único para a profissão.
            this.NomeProf = nomeProf;
        }

        // Identificador único da profissão (chave primária).
        [Key]
        [Required]
        public Guid Id { get; private set; }
        // Nome da profissão (ex: "Desenvolvedor", "Arquiteto").
        [Required]
        public string NomeProf { get; private set; }
    }
}