// Controllers/ConcursosController.cs
using Microsoft.AspNetCore.Mvc;
using VenhaParaOLEDS.Data;
using VenhaParaOLEDS.DTOs;
using Microsoft.EntityFrameworkCore;

namespace VenhaParaOLEDS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConcursosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ConcursosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{codigo}/candidatos-compativeis")]
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