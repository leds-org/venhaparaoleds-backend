namespace TrilhaBackendLeds.Dtos.MatchDtos
{
    public class OpportunitiesResponse
    {
        public string NumeroDoEdital { get; set; } = string.Empty;
        public string CodigoDoConcurso { get; set; } = string.Empty;
        public string NomeDoOrgao { get; set; } = string.Empty;

        public OpportunitiesResponse(string numeroDoEdital, string codigoDoConcurso, string nomeDoOrgao)
        {
            NumeroDoEdital = numeroDoEdital;
            CodigoDoConcurso = codigoDoConcurso;
            NomeDoOrgao = nomeDoOrgao;
        }
    }
}
