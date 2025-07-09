using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LedsAPI.Domain.Entities
{
    // Define o nome da tabela no banco de dados para esta entidade.
    [Table("Candidatos")]
    // Representa a entidade Candidato no domínio da aplicação.
    public class Candidato
    {
        // Construtor padrão exigido pelo Entity Framework Core.
        public Candidato() { }
        // Construtor para criar uma nova instância de Candidato.
        public Candidato(string cpf, string nome, DateTime nascimento, List<Profissao> profissoes)
        {
            this.CPF = cpf;
            this.Nome = nome;
            this.Nascimento = nascimento;
            this.Profissoes = profissoes;
        }
        // Método para atualizar as informações de um candidato existente.
        public void Atualizar(string nome, DateTime nascimento, List<Profissao> profissoes)
        {
            this.Nome = nome;
            this.Nascimento = nascimento;
            this.Profissoes = profissoes;
        }

        // CPF do candidato, chave primária da tabela.
        [Key]
        [Required]
        public string CPF { get; private set; }
        // Nome completo do candidato.
        [Required]
        public string Nome { get; private set; }
        // Data de nascimento do candidato.
        [Required]
        public DateTime Nascimento { get; private set; }
        // Lista de profissões associadas a este candidato.
        [Required]
        public List<Profissao> Profissoes { get; private set; } = new List<Profissao>();
    }
}