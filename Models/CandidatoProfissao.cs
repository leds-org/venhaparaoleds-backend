using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TrilhaBackendLeds.Models
{
    [Table("candidato_profissao")]
    public class CandidatoProfissao
    {
        public int CandidatoId { get; set; }
        public Candidato Candidato { get; set; }

        public int ProfissaoId { get; set; }
        public Profissao Profissao { get; set; }


    }
}
