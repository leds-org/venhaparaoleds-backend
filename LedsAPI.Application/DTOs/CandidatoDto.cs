namespace LedsAPI.Application.DTOs
{
    public class CandidatoDto
    {
        // CPF do candidato, usado como identificador.
        public string CPF { get; set; } = string.Empty; // indica que a propriedade ser� inicializada e n�o ser� nula.
        // Nome completo do candidato.
        public string Nome { get; set; } = string.Empty;
        // Data de nascimento do candidato.
        public DateTime Nascimento { get; set; }
        // Lista de profiss�es que o candidato possui.
        public List<ProfissaoDto> Profissoes { get; set; } = new List<ProfissaoDto>(); // Inicializa a lista.
    }
}