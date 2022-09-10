using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutenticaoApiKey.Api.Controllers
{
    [ApiController]
    [Route("api/consultas")]
    public class ConsultasController : ControllerBase
    {

        private readonly ILogger<ConsultasController> _logger;

        public ConsultasController(ILogger<ConsultasController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult GetConsultasPublicas()
        {
            return Ok(new
            {
                Retorno = "consulta pública - ok!"
            });
        }

        [HttpGet("privadas")]
        [Authorize]
        public IActionResult GetConsultasPrivadas()
        {
            return Ok(new
            {
                Retorno = $"{User.Identity.Name}. Consulta privada - ok"
            });
        }
    }
}