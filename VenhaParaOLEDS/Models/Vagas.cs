// Models/Vagas.cs
using System.ComponentModel.DataAnnotations.Schema;

namespace VenhaParaOLEDS.Models
{
    public class Vaga
    {
        public int Id { get; set; }
        public required string Nome { get; set; }

        // Chave estrangeira para Concurso
        public int ConcursoId { get; set; }

        // Nvegação para Concurso
        [ForeignKey("ConcursoId")]
        public Concurso Concurso { get; set; }
    }
}