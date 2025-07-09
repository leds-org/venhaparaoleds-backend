/* Este DTO representa os dados de um concurso.
 * � usado para enviar ou receber informa��es sobre um concurso.
 */
namespace LedsAPI.Application.DTOs
{
    public class ConcursoDto
    {
        // C�digo �nico do concurso.
        public long CdConcurso { get; set; }
        // Nome ou sigla do �rg�o que promove o concurso.
        public string Orgao { get; set; } = null!; // 'null!' indica que a propriedade ser� inicializada e n�o ser� nula.
        // N�mero do edital do concurso (ex: "15/2017").
        public string Edital { get; set; } = null!;
        // Lista de vagas dispon�veis neste concurso.
        public List<VagaDto> Vagas { get; set; } = new List<VagaDto>(); // Inicializa a lista.
    }
}