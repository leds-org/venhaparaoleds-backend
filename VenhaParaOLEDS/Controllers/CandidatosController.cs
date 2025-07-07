// Controllers/Candidato.cs
using Microsoft.AspNetCore.Mvc;
using VenhaParaOLEDS.Data;
using VenhaParaOLEDS.DTOs;
using Microsoft.EntityFrameworkCore;

namespace VenhaParaOLEDS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatosController : ControllerBase
    {
        private readonly AppDbContext _context;
        // Construtor
        public CandidatosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{cpf}/concursos-compatíveis")]
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