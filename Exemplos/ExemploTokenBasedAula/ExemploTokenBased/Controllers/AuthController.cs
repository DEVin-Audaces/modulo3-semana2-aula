using ExemploTokenBased.DTOs;
using ExemploTokenBased.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExemploTokenBased.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly ITokenService _tokenService;

    public AuthController(
        IUsuarioService usuarioService, 
        ITokenService tokenService)
    {
        _usuarioService = usuarioService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public ActionResult<JWTResult> Login(
        [FromBody] LoginDTO body
    )
    {
        var usuario = _usuarioService
            .AutenticarUsuario(body.NomeUsuario, body.Senha);

        if (usuario == null)
        {
            return BadRequest("Dados de login inválidos");
        }

        var objetoRetorno = _tokenService.GerarJwt(usuario);

        return Ok(objetoRetorno);
    }
}