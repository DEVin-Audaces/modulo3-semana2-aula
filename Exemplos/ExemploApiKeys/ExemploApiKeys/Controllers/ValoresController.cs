using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExemploApiKeys.Controllers;

[ApiController]
[Route("valores")]
public class ValoresController : ControllerBase
{
    [HttpGet("allow-anonymous")]
    [AllowAnonymous]
    public IActionResult GetSemAutenticação()
        => Ok(new
        {
            resultado = "Dados que podem ser acessados sem autenticação."
        });
    
    [HttpGet("logado")]
    [Authorize]
    public IActionResult GetLogado()
        => Ok(new
        {
            resultado = "Dados que apenas usuários logados podem ver",
            usuarioLogado = User.FindFirstValue(ClaimTypes.Name)
        });
}