/* Este DTO representa os dados de uma profiss�o.
 * � usado para enviar ou receber informa��es sobre uma profiss�o.
 */
namespace LedsAPI.Application.DTOs
{
    public class ProfissaoDto
    {
        // Nome da profiss�o (ex: "Desenvolvedor", "M�dico").
        public string NomeProf { get; set; } = null!; // 'null!' indica que a propriedade ser� inicializada e n�o ser� nula.
    }
}