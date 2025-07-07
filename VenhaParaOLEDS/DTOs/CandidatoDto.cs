// DTOs/CandidatoDto.cs
namespace VenhaParaOLEDS.DTOs
{
    public class CandidatoDto
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; } = string.Empty; 
    }
}