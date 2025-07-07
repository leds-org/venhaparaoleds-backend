// Controllers/Candidato.cs
using Microsoft.AspNetCore.Mvc;
using VenhaParaOLEDS.Data;
using VenhaParaOLEDS.DTOs;
using Microsoft.EntityFrameworkCore;

namespace VenhaParaOLEDS.Controllers
{
    /// <summary>
    /// Operações relacionadas à candidato.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatosController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor para injeção de dependência do contexto.
        /// </summary>
        public CandidatosController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retorna uma lista de concursos compatíveis com as profissões do candidato informado.
        /// </summary>
        /// <param name="cpf">CPF do candidato.</param>
        /// <returns>Lista de concursos compatíveis.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        /// <response code="404">Candidato não encontrado.</response>
        [HttpGet("{cpf}/concursos-compatíveis")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ConcursoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetConcursosCompativeis(string cpf)
        {
            //Busca o candidato com suas profissões
            var candidato = _context.Candidatos
                .Include(c => c.Profissoes)
                .FirstOrDefault(candidato => candidato.CPF == cpf);

            if (candidato == null)
            {
                return NotFound("Candidato não encontrado.");
            }

            //Extrai os nomes das profissões do candidato
            var profissoesDoCandidato = candidato.Profissoes.Select(p => p.Nome).ToList();

            //Busca concursos que tenham ao menos uma vaga compatível
            var concursosCompativeis = _context.Concursos
                .Include(c => c.Vagas)
                .Where(c => c.Vagas.Any(v => profissoesDoCandidato.Contains(v.Nome)))
                // Projeta os dados para o DTO ConcursoDto, garantindo segurança e organização.
                .Select(c => new ConcursoDto
                {
                    Orgao = c.Orgao,
                    Edital = c.Edital,
                    Codigo = c.Codigo
                })
                .ToList();
            // Retorna u JSON 200 com a lista dos concursos compatíveis.
            return Ok(concursosCompativeis);
        }
    }
}