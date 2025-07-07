// Controller/StatusController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

namespace VenhaParaOLEDS.Controllers
{
    /// <summary>
    /// Fornece endpoints para verificação da saúde e status da API.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class StatusController : Controller
    {
        /// <summary>
        /// Verifica se a API está online
        /// </summary>
        /// <returns>Status "Healthy"</returns>
        /// <response code="200">API está saudável.</response>
        // GET /status/health
        [HttpGet("health")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]

        public IActionResult Health()
        {
            
            return Ok("Healthy");
        }

        /// <summary>
        /// Retorna informações de status da aplicação.
        /// </summary>
        /// <returns>Informações de versãi, ambiente e data/hora.</returns>
        /// <response code="200">Status retornado com sucesso.</response>
        // GET /status
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public IActionResult GetStatus()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var statusInfo = new
            {
                App = "VenhaParaOLEDS API",
                Version = assembly.GetName().Version?.ToString(),
                Timestamp = DateTime.UtcNow.ToString("o"),
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknow",
                Database = "Ok"
            };

            return Ok(statusInfo);
        }
    }
}