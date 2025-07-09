/* Este DTO representa os dados de uma vaga de emprego em um concurso.
 * � usado para enviar ou receber informa��es sobre uma vaga.
 */
namespace LedsAPI.Application.DTOs
{
    public class VagaDto
    {
        // Nome da vaga (ex: "Analista de Sistemas J�nior").
        public string NomeVag { get; set; } = null!; // 'null!' indica que a propriedade ser� inicializada e n�o ser� nula.
        // Lista de profiss�es que s�o necess�rias para esta vaga.
        public List<ProfissaoDto> ProfissoesNecessarias { get; set; } = new List<ProfissaoDto>(); // Inicializa a lista.
    }
}