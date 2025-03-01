using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TrilhaBackendLeds.Models;
using TrilhaBackendLeds.Dtos.OrgaosDtos;
using TrilhaBackendLeds.Dtos.ConcursoDtos;

namespace TrilhaBackendLeds.Dtos.EditaisDtos
{
    public class EditalResponse
    {
        public int Id { get; set; }
        public string NumeroDoEdital { get; set; } = string.Empty;
        public OrgaoResponse Orgao { get; set; }
        public ConcursoResponse Concurso { get; set; }

        public EditalResponse()
        {
        }

        public EditalResponse(int id, string numeroDoEdital)
        {
            Id = id;
            NumeroDoEdital = numeroDoEdital;
        }

        public EditalResponse(int id, string numeroDoEdital, OrgaoResponse orgao, ConcursoResponse concurso)
        {
            Id = id;
            NumeroDoEdital = numeroDoEdital;
            Orgao = orgao;
            Concurso = concurso;
        }
    }
}
