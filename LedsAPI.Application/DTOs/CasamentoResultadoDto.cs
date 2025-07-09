/* Este DTO (Objeto de Transfer�ncia de Dados) � usado para representar o resultado
 * do "casamento" entre candidatos e concursos.
 * Ele agrupa um candidato com a lista de concursos compat�veis para ele.
 */
namespace LedsAPI.Application.DTOs
{
    public class CasamentoResultadoDto
    {
        // Detalhes do candidato.
        public CandidatoDto Candidato { get; set; } = null!; // 'null!' indica que a propriedade ser� inicializada e n�o ser� nula.
        // Lista de concursos que o candidato pode se candidatar.
        public List<ConcursoDto> ConcursosCompativeis { get; set; } = new List<ConcursoDto>(); // Inicializa a lista.
    }
}