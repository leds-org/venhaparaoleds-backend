using System.ComponentModel.DataAnnotations;
using TrilhaBackendLeds.Models;

namespace TrilhaBackendLeds.Dtos.OrgaosDtos
{
    public class OrgaoResponse
    {
        
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public OrgaoResponse()
        {
        }

        public OrgaoResponse(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
