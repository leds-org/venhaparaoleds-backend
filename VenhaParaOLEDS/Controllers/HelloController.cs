// Controllers/HelloController.cs

using Microsoft.AspNetCore.Mvc;

namespace VenhaParaOLEDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Olá, API funcionando!");
        }
    }
}