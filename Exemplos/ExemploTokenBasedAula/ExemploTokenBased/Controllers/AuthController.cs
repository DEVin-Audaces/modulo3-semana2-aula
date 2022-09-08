using ExemploTokenBased.DTOs;
using ExemploTokenBased.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExemploTokenBased.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public AuthController(
        IUsuarioService usuarioService
    )
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("login")]
    public ActionResult Login(
        [FromBody] LoginDTO body
    )
    {
        var usuario = _usuarioService
            .AutenticarUsuario(body.NomeUsuario, body.Senha);

        if (usuario == null)
        {
            return BadRequest("Dados de login inválidos");
        }
    }
}