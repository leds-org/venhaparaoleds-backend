// Controllers/ConcursosController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using VenhaParaOLEDS.Data;
using VenhaParaOLEDS.DTOs;
using Microsoft.EntityFrameworkCore;

namespace VenhaParaOLEDS.Controllers
{
    /// <summary>
    /// Controlador responsável pelas operações relacionadas à concursos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ConcursosController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor do controlador de concursos.
        /// </summary>
        /// <param name="context">Contexto do banco de dados.</param>
        public ConcursosController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna uma lista de candidatos compatíveis com as vagas do concurso informado.
        /// </summary>
        /// <param name="codigo">Código identificador do concurso.</param>
        /// <returns>Lista de candidatos compatíveis.</returns>
        /// <response code="200">Candidatos encontrados com sucesso.</response>
        /// <response code="404">Concurso não encontrado.</response>
        [HttpGet("{codigo}/candidatos-compativeis")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CandidatoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCandidatosCompativeis(string codigo)
        {
            //Busca o concurso e suas vagas
            var concurso = _context.Concursos
                .Include(c => c.Vagas)
                .FirstOrDefault(c => c.Codigo == codigo);

            if (concurso == null)
            {
                return NotFound("Concurso não encontrado");
            }

            var vagas = concurso.Vagas.Select(v => v.Nome).ToList();

            // Busca candidatos que tenham ao menos uma profissão compatível
            var candidatosCompativeis = _context.Candidatos
                .Include(c => c.Profissoes)
                .Where(c => c.Profissoes.Any(p => vagas.Contains(p.Nome)))
                .Select(c => new CandidatoDto
                {
                    Nome = c.Nome,
                    CPF = c.CPF,
                    DataNascimento = c.DataNascimento
                })
                .ToList();

            return Ok(candidatosCompativeis);
        }
    }
}