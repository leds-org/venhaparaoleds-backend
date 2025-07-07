// Controller/StatusController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

namespace VenhaParaOLEDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : Controller
    {
        // GET /status/health
        [HttpGet("health")]
        public IActionResult Health()
        {
            // Retorna apenas 200 OK para indicar que a API está de pé
            return Ok("Healthy");
        }

        // GET /status
        [HttpGet]
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