using Microsoft.AspNetCore.Mvc;
using TrilhaBackendLeds.Dtos.CandidatoDtos;
using TrilhaBackendLeds.Services;
using TrilhaBackendLeds.Services.Interfaces;

namespace TrilhaBackendLeds.Controllers
{
    /// <summary>
    /// Controller para as operações relacionadas a candidatos.
    /// </summary>
    [Route("api/candidatos")]
    [ApiController]
    [Produces("application/json")]
    public class CandidatosController : ControllerBase
    {
        private readonly ICandidatoService _candidatoService;
        private readonly MatchService _matchService;
        private readonly ICandidatoProfissaoService _candidatoProfissaoService;

        public CandidatosController(ICandidatoService candidatoService, MatchService matchService, ICandidatoProfissaoService candidatoProfissaoService)
        {
            _candidatoService = candidatoService;
            _matchService = matchService;
            _candidatoProfissaoService = candidatoProfissaoService;
        }

        /// <summary>
        /// Obtém uma lista de candidatos e suas repesctivas profissões.
        /// </summary>
        /// <returns>Uma lista de CandidatoResponse</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult FindAll()
        {
            IEnumerable<CandidatoResponse> response = _candidatoService.FindAll();
            return Ok(response);
        }

        /// <summary>
        /// Obtém um candidato e suas respectivas profissões com base no ID do candidato.
        /// </summary>
        /// <param name="candidatoId">ID do candidato.</param>
        /// <returns>CandidatoResponse</returns>
        [HttpGet("{candidatoId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult FindById(int candidatoId)
        {
            CandidatoResponse response = _candidatoService.FindById(candidatoId);
            return Ok(response);
        }

        /// <summary>
        /// Obtém um candidato e suas respectivas profissões com base no CPF do candidato.
        /// </summary>
        /// <param name="cpf">CPF do candidato.</param>
        /// <remarks>
        /// Regras de validação:
        /// - Forneça um CPF que possua somente 11 dígitos
        /// - Os dígitos não podem ser todos iguais
        /// </remarks>
        /// <returns></returns>
        [HttpGet("cpf")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult FindByCpf(string cpf)
        {
            CandidatoResponse response = _candidatoService.FindByCpf(cpf);
            return Ok(response);
        }
        /// <summary>
        /// Obtém uma lista de OpportunitiesResponse. Esse é o endpoint que encontra os concursos que um candidato pode participar. Esse endpoint responde o primeiro desafio.
        /// </summary>
        /// <param name="cpf">CPF do candidato.</param>
        /// <remarks>
        /// Regras de validação:
        /// - Forneça um CPF que possua somente 11 dígitos
        /// - Os dígitos não podem ser todos iguais
        /// </remarks>
        /// <returns>Uma lista de OpportunitiesResponse</returns>
        [HttpGet("match/cpf")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult FindOpportunitiesByCpf(string cpf)
        {
            var response = _matchService.FindOpportunitiesByCpf(cpf).ToList();
            return Ok(response);
        }

        /// <summary>
        /// Obtém uma lista de CandidatoConcursoMatchResponse. Esse é o endpoint que encontra os candidatos que podem participar de um concurso. Esse endpoint responde o segundo desafio.
        /// </summary>
        /// <param name="codigoDoConcurso">Código do concurso.</param>
        /// <remarks>
        /// Regras de validação:
        /// - Forneça um código do concurso que possua somente 11 dígitos
        /// </remarks>
        /// <returns>Lista de CandidatoConcursoMatchResponse</returns>
        [HttpGet("match/codigo_do_concurso")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult FindCandidatosByCodigoDoConcurso(string codigoDoConcurso)
        {
            var response = _matchService.FindCandidatosByCodigoDoConcurso(codigoDoConcurso).ToList();
            return Ok(response);
        }

        /// <summary>
        /// Esse endpoint é o responsável por adicionar uma profissão a um candidato.
        /// </summary>
        /// <param name="candidatoId">ID do candidato.</param>
        /// <param name="profissaoId">ID da profissão.</param>
        /// <remarks>
        /// Regras de validação:
        /// - candidatoId e profissaoId devem existir
        /// - O candidato não deve possuir a profissão antes da adição
        /// </remarks>
        /// <returns>CandidatoResponse</returns>
        [HttpPost("add/profissao/{candidatoId}/{profissaoId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult AddProfissaoToCandidato(int candidatoId, int profissaoId)
        {
            var response = _candidatoProfissaoService.AddProfissaoToCandidato(profissaoId, candidatoId);
            return CreatedAtAction(nameof(AddProfissaoToCandidato), new { candidatoId, profissaoId }, response);
        }

        /// <summary>
        /// Esse endpoint é o responsável por remover uma profissão de um candidato.
        /// </summary>
        /// <param name="candidatoId">ID do candidato.</param>
        /// <param name="profissaoId">ID da profissão.</param>
        /// <remarks>
        /// Regras de validação:
        /// - candidatoId e profissaoId devem existir
        /// - O candidato deve possuir a profissão antes da remoção
        /// </remarks> 
        /// <returns></returns>
        [HttpDelete("remove/profissao/{candidatoId}/{profissaoId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult RemoveProfissaoFromCandidato(int candidatoId, int profissaoId)
        {
            _candidatoProfissaoService.RemoveProfissaoFromCandidato(profissaoId, candidatoId);
            return NoContent();
        }

        /// <summary>
        /// Endpoint responsável pela criação de um candidato.
        /// </summary>
        /// <param name="request">DTO CandidatoCreateRequest.</param>
        /// <remarks>
        /// Regras de validação:
        /// - O nome não pode ser nulo ou vazio
        /// - O nome não pode conter números
        /// - O CPF não deve estar cadastrado antes da criação
        /// - O CPF não pode ser nulo ou vazio
        /// - O CPF deve possuir somente 11 números e não podem ser todos iguais
        /// - A data de nascimento deve estar no passado
        /// </remarks>
        /// <returns>CandidatoResponse</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Create(CandidatoCreateRequest request)
        {
            CandidatoResponse response = _candidatoService.Create(request);
            return Created($"api/candidatos/{response.Id}", response);
        }

        /// <summary>
        /// Endpoint responsável por atualizar um candidato, a atualização pode ser feita desde que 1 alteração seja fornecida.
        /// </summary>
        /// <param name="candidatoId">ID do candidato.</param>
        /// <param name="request">DTO CandidatoUpdateRequest.</param>
        /// <remarks>
        /// Regras de validação:
        /// - Pelo menos uma alteração deve ser fornecida
        /// - O nome não pode ser nulo ou vazio
        /// - O nome não pode conter números
        /// - O CPF não deve estar cadastrado antes da criação
        /// - O CPF não pode ser nulo ou vazio
        /// - O CPF deve possuir somente 11 números e não podem ser todos iguais
        /// - A data de nascimento deve estar no passado
        /// </remarks>
        /// <returns>CandidatoResponse</returns>
        [HttpPatch("{candidatoId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(int candidatoId, CandidatoUpdateRequest request)
        {
            CandidatoResponse response = _candidatoService.Update(candidatoId, request);
            return Ok(response);
        }

        /// <summary>
        /// Endpoint responsável por deletar um candidato com base em seu ID.
        /// </summary>
        /// <param name="candidatoId">ID do candidato.</param>
        /// <returns></returns>
        [HttpDelete("{candidatoId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteById(int candidatoId)
        {
            _candidatoService.DeleteById(candidatoId);
            return NoContent();
        }

    }
}
