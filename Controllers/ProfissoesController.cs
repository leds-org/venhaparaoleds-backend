using Microsoft.AspNetCore.Mvc;
using TrilhaBackendLeds.Dtos.ProfissaoDtos;
using TrilhaBackendLeds.Services.Interfaces;

namespace TrilhaBackendLeds.Controllers
{
    /// <summary>
    /// Controller para as operações relacionadas a profissões.
    /// </summary>
    [Route("api/profissoes")]
    [ApiController]
    [Produces("application/json")]
    public class ProfissoesController : ControllerBase
    {
        private readonly IProfissaoService _service;

        public ProfissoesController(IProfissaoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna uma lista de ProfissaoResponse
        /// </summary>
        /// <returns>Lista de ProfissaoResponse</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult FindAll()
        {
            IEnumerable<ProfissaoResponse> response = _service.FindAll();
            return Ok(response);
        }

        /// <summary>
        /// Retorna um ProfissaoResponse com base no ID da profissão
        /// </summary>
        /// <param name="profissaoId">ID da profissão</param>
        /// <returns>ProfissaoResponse</returns>
        [HttpGet("{profissaoId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult FindById(int profissaoId)
        {
            ProfissaoResponse response = _service.FindById(profissaoId);
            return Ok(response);
        }

        /// <summary>
        /// Cria uma profissão com base no DTO ProfissaoCreateRequest
        /// </summary>
        /// <param name="request">DTO ProfissaoCreateRequest</param>
        /// <remarks>
        /// Regras de validação:
        /// - O nome da profissão deve ser fornecido, não pode ser nulo ou vazio
        /// - O nome da profissão deve estar em letras minúsculas
        /// - O nome da profissão não deve existir previamente no DB
        /// </remarks>
        /// <returns>ProfissaoResponse</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Create(ProfissaoCreateRequest request)
        {
            ProfissaoResponse response = _service.Create(request);
            return Created($"api/profissoes/{response.Id}", response);
        }

        /// <summary>
        /// Atualiza uma profissão com base no ID da profissão e no DTO ProfissãoUpdateRequest
        /// </summary>
        /// <param name="profissaoId">ID da profissão</param>
        /// <param name="request">DTO ProfissaoUpdateRequest</param>
        /// <remarks>
        /// Regras de validação:
        /// - Pelo menos uma alteração deve ser fornecida
        /// - Se fornecido o nome da profissão não pode ser nulo ou vazio
        /// - Se fornecido o nome da profissão deve estar em letras minúsculas
        /// - Se fornecido o nome da profissão não deve existir previamente no DB
        /// </remarks>
        /// <returns>ProfissaoResponse</returns>
        [HttpPatch("{profissaoId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(int profissaoId, ProfissaoUpdateRequest request)
        {
            ProfissaoResponse response = _service.Update(profissaoId, request);
            return Ok(response);
        }

        /// <summary>
        /// Deleta uma profissão com base no ID da profissão
        /// </summary>
        /// <param name="profissaoId">ID da profissão</param>
        /// <returns></returns>
        [HttpDelete("{profissaoId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteById(int profissaoId)
        {
            _service.DeleteById(profissaoId);
            return NoContent();
        }
    }
}
