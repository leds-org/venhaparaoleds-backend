using Microsoft.AspNetCore.Mvc;
using TrilhaBackendLeds.Dtos.EditaisDtos;
using TrilhaBackendLeds.Services.Interfaces;

namespace TrilhaBackendLeds.Controllers
{
    /// <summary>
    /// Controller para as operações relacionadas a editais.
    /// </summary>
    [Route("api/editais")]
    [ApiController]
    [Produces("application/json")]
    public class EditaisController : ControllerBase
    {
        private readonly IEditalService _service;

        public EditaisController(IEditalService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna uma lista de EditalResponse
        /// </summary>
        /// <returns>Lista de EditalResponse</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult FindAll()
        {
            IEnumerable<EditalResponse> response = _service.FindAll();
            return Ok(response);
        }

        /// <summary>
        /// Retorna um EditalResponse com base no ID do edital
        /// </summary>
        /// <param name="editalId">ID do edital</param>
        /// <returns>EditalResponse</returns>
        [HttpGet("{editalId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult FindById(int editalId)
        {
            EditalResponse response = _service.FindById(editalId);
            return Ok(response);
        }

        /// <summary>
        /// Cria um Edital com base em um DTO EditalCreateRequest
        /// </summary>
        /// <param name="request">DTO EditalCreateRequest</param>
        /// <remarks> 
        /// Regras de validação:
        /// - O número do edital deve ser fornecido, não pode ser nulo ou vazio
        /// - O número do edital deve estar no formato N/AAAA, onde N pode ter até 4 dígitos e AAAA é o ano do edital
        /// - Deve ser fornecido um orgaoId existente e o orgão não deve conter esse número do edital
        /// - Deve ser fornecido um concursoId e o concurso deve existir
        /// </remarks>
        /// <returns>EditalResponse</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Create(EditalCreateRequest request)
        {
            EditalResponse response = _service.Create(request);
            return Created($"api/editais/{response.Id}", response);
        }

        /// <summary>
        /// Atualiza um Edital com base no ID do edital e um DTO EditalUpdateRequest
        /// </summary>
        /// <param name="editalId">ID do Edital</param>
        /// <param name="request">DTO EditalUpdateRequest</param>
        /// <remarks>
        /// Regras de validação:
        /// - Pelo menos uma alteração deve ser fornecida
        /// - O número do edital do edital pode ser nulo(não resultará em alteração), mas não pode ser vazio ou conter espaços em branco
        /// - O número do edital se fornecido deve estar no formato N/AAAA, onde N pode ter até 4 dígitos e AAAA é o ano do edital
        /// - Se fornecido um orgaoId existente, o orgão não deve conter esse número do edital
        /// - Se fornecido um concursoId, o concurso deve existir
        /// </remarks> 
        /// <returns>EditalResponse</returns>
        [HttpPatch("{editalId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(int editalId, EditalUpdateRequest request)
        {
            EditalResponse response = _service.Update(editalId, request);
            return Ok(response);
        }

        /// <summary>
        /// Deleta um Edital com base no ID do edital.
        /// </summary>
        /// <param name="editalId">ID do edital</param>
        /// <returns></returns>
        [HttpDelete("{editalId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteById(int editalId)
        {
            _service.DeleteById(editalId);
            return NoContent();
        }
    }
}
