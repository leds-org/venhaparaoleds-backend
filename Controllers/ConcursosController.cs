using Microsoft.AspNetCore.Mvc;
using TrilhaBackendLeds.Dtos.ConcursoDtos;
using TrilhaBackendLeds.Services.Interfaces;

namespace TrilhaBackendLeds.Controllers
{
    /// <summary>
    /// Controller para as operações relacionadas a concursos.
    /// </summary>
    [Route("api/concursos")]
    [ApiController]
    [Produces("application/json")]
    public class ConcursosController : ControllerBase
    {
        private readonly IConcursoService _service;

        public ConcursosController(IConcursoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna uma lista de ConcursoResponse
        /// </summary>
        /// <returns>Lista de ConcursoResponse</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult FindAll()
        {
            IEnumerable<ConcursoResponse> response = _service.FindAll();
            return Ok(response);
        }

        /// <summary>
        /// Retorna um ConcursoResponse com base no ID do concurso
        /// </summary>
        /// <param name="concursoId">ID do concurso.</param>
        /// <returns>ConcursoResponse</returns>
        [HttpGet("{concursoId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult FindById(int concursoId)
        {
            ConcursoResponse response = _service.FindById(concursoId);
            return Ok(response);
        }

        /// <summary>
        /// Retorna um ConcursoResponse com base no código do concurso
        /// </summary>
        /// <param name="codigoDoConcurso">Código do concurso.</param>
        /// <remarks>
        /// Regras de validação:
        /// - Forneça um código do concurso que possua somente 11 dígitos
        /// </remarks>
        /// <returns>ConcursoResponse</returns>
        [HttpGet("codigoDoConcurso")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult FindByCodigoDoConcurso(string codigoDoConcurso)
        {
            ConcursoResponse response = _service.FindByCodigoDoConcurso(codigoDoConcurso);
            return Ok(response);
        }

        /// <summary>
        /// Cria um concurso com base em um DTO ConcursoCreateRequest
        /// </summary>
        /// <param name="request">DTO ConcursoCreateRequest</param>
        /// <remarks>
        /// Regras de validação:
        /// - Forneça um código do concurso que possua somente 11 dígitos
        /// - O código não pode existir no DB antes da criação
        /// </remarks>
        /// <returns>ConcursoResponse</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Create(ConcursoCreateRequest request)
        {
            ConcursoResponse response = _service.Create(request);
            return Created($"api/concursos/{response.Id}", response);
        }

        /// <summary>
        /// Atualiza um concurso com base no ID do concurso e um DTO ConcursoUpdateRequest
        /// </summary>
        /// <param name="concursoId">ID do concurso</param>
        /// <param name="request">DTO ConcursoUpdateRequest</param>
        /// <remarks>
        /// Regras de validação:
        /// - Pelo menos uma alteração deve ser fornecida
        /// - Forneça um código do concurso que possua somente 11 dígitos
        /// - O novo código não pode existir no DB antes da atualização
        /// </remarks>
        /// <returns>ConcursoResponse</returns>
        [HttpPatch("{concursoId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(int concursoId, ConcursoUpdateRequest request)
        {
            ConcursoResponse response = _service.Update(concursoId, request);
            return Ok(response);
        }

        /// <summary>
        /// Deleta um concurso com base no ID do concurso
        /// </summary>
        /// <param name="concursoId">ID do concurso</param>
        /// <returns></returns>
        [HttpDelete("{concursoId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteById(int concursoId)
        {
            _service.DeleteById(concursoId);
            return NoContent();
        }
    }
}
