// Models/Profissao.cs
using System.ComponentModel.DataAnnotations.Schema;

namespace VenhaParaOLEDS.Models
{
    public class Profissao
    {
        public int Id { get; set; }
        public required string Nome { get; set; }

        // Chave estrangeira para candidato
        public int CandidatoId { get; set; }

        // Navegação para Candidato
        [ForeignKey("CandidatoId")]
        public Candidato Candidato { get; set; }
    }
}