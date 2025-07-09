/* Esta classe é um controlador ASP.NET Core que lida com a importação de dados
 * e a consulta de informações relacionadas a concursos e candidatos.
 * Ela expõe endpoints HTTP para iniciar importações e buscar dados.
 */
using Microsoft.AspNetCore.Mvc;
using LedsAPI.Application.Interfaces;

namespace LedsAplication.Controllers
{
    // Indica que esta classe é um controlador de API.
    [ApiController]
    // Define a rota base para os endpoints deste controlador (ex: /api/Importacao).
    [Route("api/[controller]")]
    public class ImportacaoController : ControllerBase
    {
        // Campos somente leitura para os serviços que este controlador irá usar.
        private readonly IConcursoService _concursoService;
        private readonly IImportacaoService _importacaoService;

        // Construtor: O ASP.NET Core injetará as instâncias dos serviços aqui.
        public ImportacaoController(IConcursoService concursoService, IImportacaoService importacaoService)
        {
            _concursoService = concursoService;
            _importacaoService = importacaoService;
        }

        // Endpoint POST para importar dados de candidatos.
        // Responde a requisições POST para /api/Importacao/candidatos.
        [HttpPost("candidatos")]
        public async Task<IActionResult> ImportarCandidatos([FromQuery] string path)
        {
            // Chama o serviço de leitura para importar os candidatos do arquivo.
            await _importacaoService.ImportarArquivos_CandidatosAsync(path);
            // Retorna um status OK com uma mensagem de sucesso.
            return Ok("Candidatos importados com sucesso!");
        }

        // Endpoint POST para importar dados de concursos.
        // Responde a requisições POST para /api/Importacao/concursos.
        [HttpPost("concursos")]
        public async Task<IActionResult> ImportarConcursos([FromQuery] string path)
        {
            // Chama o serviço de leitura para importar os concursos do arquivo.
            await _importacaoService.ImportarArquivos_ConcursosAsync(path);
            // Retorna um status OK com uma mensagem de sucesso.
            return Ok("Concursos importados com sucesso!");
        }


        // Endpoint GET para obter a lista de concursos compatíveis para todos os candidatos.
        // Responde a requisições GET para /api/Importacao/casamentos.
        [HttpGet("casamentos")]
        public async Task<IActionResult> GetCasamentos()
        {
            // Chama o serviço de casamento para obter os concursos compatíveis.
            var resultado = await _concursoService.ObterConcursosCompativeisAsync();
            // Retorna um status OK com o resultado.
            return Ok(resultado);
        }

        // Endpoint GET para buscar concursos compatíveis por CPF.
        // Responde a requisições GET para /api/Importacao/concursos-por-cpf.
        [HttpGet("concursos-por-cpf")]
        public async Task<IActionResult> GetConcursosPorCpf([FromQuery] string cpf)
        {
            // Chama o serviço de consulta para buscar concursos por CPF.
            var resultado = await _concursoService.BuscarConcursosPorCpfAsync(cpf);
            // Se nenhum resultado for encontrado, retorna NotFound.
            if (resultado == null || !resultado.Any())
            {
                return NotFound($"Nenhum concurso encontrado para o CPF: {cpf}");
            }
            // Retorna um status OK com os resultados.
            return Ok(resultado);
        }

        // Endpoint GET para buscar candidatos compatíveis por código de concurso.
        // Responde a requisições GET para /api/Importacao/candidatos-por-concurso.
        [HttpGet("candidatos-por-concurso")]
        public async Task<IActionResult> GetCandidatosPorConcurso([FromQuery] long cdConcurso)
        {
            // Chama o serviço de consulta para buscar candidatos por código de concurso.
            var resultado = await _concursoService.BuscarCandidatosPorConcursoAsync(cdConcurso);
            // Se nenhum resultado for encontrado, retorna NotFound.
            if (resultado == null || !resultado.Any())
            {
                return NotFound($"Nenhum candidato encontrado para o concurso: {cdConcurso}");
            }
            // Retorna um status OK com os resultados.
            return Ok(resultado);
        }
    }
}