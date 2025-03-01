using Microsoft.AspNetCore.Mvc;
using TrilhaBackendLeds.Dtos.VagasDtos;
using TrilhaBackendLeds.Services.Interfaces;


namespace TrilhaBackendLeds.Controllers
{
    /// <summary>
    /// Controller para as operações relacionadas a vagas.
    /// </summary>
    [Route("api/vagas")]
    [ApiController]
    [Produces("application/json")]
    public class VagasController : ControllerBase
    {
        private readonly IVagasService _service;

        public VagasController(IVagasService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna uma lista de VagaResponse
        /// </summary>
        /// <returns>Lista de VagaResponse</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult FindAll()
        {
            IEnumerable<VagaResponse> response = _service.FindAll();
            return Ok(response);
        }

        /// <summary>
        /// Retorna uma VagaResponse com base no ID da vaga
        /// </summary>
        /// <param name="vagaId">ID da vaga</param>
        /// <returns></returns>
        [HttpGet("{vagaId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult FindById(int vagaId)
        {
            VagaResponse response = _service.FindById(vagaId);
            return Ok(response);
        }

        /// <summary>
        /// Cria uma base com base em uma VagaCreateRequest, esse exige um ID da profissão e um ID do edital
        /// </summary>
        /// <param name="request">DTO VagaCreateRequest</param>
        /// <remarks>
        /// Regras de validação:
        /// - A vaga não pode existir previamente;
        /// - A profissão deve existir;
        /// - O edital deve existir;
        /// </remarks>
        /// <returns>VagaResponse</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Create(VagaCreateRequest request)
        {
            VagaResponse response = _service.Create(request);
            return Created($"api/vagas/{response.Id}", response);
        }

        /// <summary>
        /// Atualiza uma vaga com base no ID da vaga e no DTO VagaUpdateRequest
        /// </summary>
        /// <param name="vagaId">ID da vaga</param>
        /// <param name="request">DTO VagaUpdateRequest</param>
        /// <remarks>
        /// Regras de validação:
        /// - Pelo menos uma alteração deve ser fornecida;
        /// - A vaga após alterada não pode ter o par(profissaoId e editalId) igual ao de uma vaga já existente;
        /// - A profissão deve existir;
        /// - O edital deve existir;
        /// </remarks>
        /// <returns>VagaResponse</returns>
        [HttpPatch("{vagaId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(int vagaId, VagaUpdateRequest request)
        {
            VagaResponse response = _service.Update(vagaId, request);
            return Ok(response);
        }

        /// <summary>
        /// Deleta uma vaga com base no ID da vaga
        /// </summary>
        /// <param name="vagaId">ID da vaga</param>
        /// <returns></returns>
        [HttpDelete("{vagaId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteById(int vagaId)
        {
            _service.DeleteById(vagaId);
            return NoContent();
        }

    }
}
