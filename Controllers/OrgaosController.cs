
using Microsoft.AspNetCore.Mvc;
using TrilhaBackendLeds.Dtos.OrgaosDtos;
using TrilhaBackendLeds.Services.Interfaces;

namespace TrilhaBackendLeds.Controllers
{
    /// <summary>
    /// Controller para as operações relacionadas a órgãos.
    /// </summary>
    [Route("api/orgaos")]
    [ApiController]
    [Produces("application/json")]
    public class OrgaosController : ControllerBase
    {

        private readonly IOrgaoService _service;

        public OrgaosController(IOrgaoService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna uma lista de OrgaoResponse
        /// </summary>
        /// <returns>Lista de OrgaoResponse</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult FindAll()
        {
            IEnumerable<OrgaoResponse> response = _service.FindAll();
            return Ok(response);
        }

        /// <summary>
        /// Retorna um OrgaoResponse
        /// </summary>
        /// <param name="orgaoId">ID do órgão</param>
        /// <returns>OrgaoResponse</returns>
        [HttpGet("{orgaoId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult FindById(int orgaoId)
        {
            OrgaoResponse response = _service.FindById(orgaoId);
            return Ok(response);
        }

        /// <summary>
        /// Cria um órgão com base em um DTO OrgaoCreateRequest
        /// </summary>
        /// <param name="request">DTO OrgaoCreateRequest</param>
        /// <remarks>
        /// Regras de validação:
        /// - O nome do orgão deve ser fornecido, não pode ser nulo ou vazio
        /// - O nome do orgão deve estar em letras maiúsculas
        /// - O nome do orgão não deve existir previamente no DB
        /// </remarks>
        /// <returns>OrgaoResponse</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult Create(OrgaoCreateRequest request)
        {
            OrgaoResponse response = _service.Create(request);
            return Created($"api/orgaos/{response.Id}", response);
        }
        /// <summary>
        /// Atualiza um órgão com base em um DTO OrgaoUpdateRequest
        /// </summary>
        /// <param name="orgaoId">ID do órgão</param>
        /// <param name="request">DTO OrgaoUpdateRequest</param>
        /// <remarks>
        /// Regras de validação:
        /// - Pelo menos uma alteração deve ser fornecida
        /// - Se fornecido o  nome do orgão não pode ser vazio
        /// - Se fornecido o  nome do orgão deve estar em letras maiúsculas
        /// - O novo nome do orgão não deve existir previamente no DB
        /// </remarks>
        /// <returns>OrgaoResponse</returns>
        [HttpPatch("{orgaoId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(int orgaoId, OrgaoUpdateRequest request)
        {
            OrgaoResponse response = _service.Update(orgaoId, request);
            return Ok(response);
        }

        /// <summary>
        /// Deleta um órgão com base no ID do órgão
        /// </summary>
        /// <param name="orgaoId">ID do órgão</param>
        /// <returns></returns>
        /// 
        [HttpDelete("{orgaoId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteById(int orgaoId)
        {
            _service.DeleteById(orgaoId);
            return NoContent();
        }
    }
}
