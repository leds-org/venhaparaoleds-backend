namespace TrilhaBackendLeds.Dtos.MatchDtos
{
    public class CandidatoConcursoMatchResponse
    {
        public string Nome { get; set; } = string.Empty;
        public DateOnly DataDeNascimento { get; set; }
        public string Cpf { get; set; } = string.Empty;

        public CandidatoConcursoMatchResponse(string nome, DateOnly dataDeNascimento, string cpf)
        {
            Nome = nome;
            DataDeNascimento = dataDeNascimento;
            Cpf = cpf;
        }
    }
}
