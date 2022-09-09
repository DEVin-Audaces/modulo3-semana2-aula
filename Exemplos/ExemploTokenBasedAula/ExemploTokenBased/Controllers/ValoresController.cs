using ExemploTokenBased.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExemploTokenBased.Controllers;

[ApiController]
[Route("api/valores")]
[Authorize]
public class ValoresController : ControllerBase
{
    [HttpGet("allow-anonymous")]
    [AllowAnonymous] //Permite o acesso sem autenticação
    public IActionResult GetSemAutenticação()
        => Ok(new
        {
            resultado = "Dados que podem ser acessados sem autenticação."
        });

    [HttpGet("logado")]
    [Authorize()]
    public IActionResult GetLogado()
        => Ok(new
        {
            resultado = "Dados que apenas usuários logados podem ver"
        });

    [HttpGet("consulta-apenas-comuns")]
    [Authorize(Roles = nameof(UsuarioPapel.Comum))]
    public IActionResult GetConsultaRoleComum()
    {
        return Ok(new
        {
            resultado = "Dados que apenas usuários com a role Comum podem acessar"
        });
    }

    [HttpGet("consulta-setor-vendas")]
    [Authorize(Policy = "SetorVendas")]
    public IActionResult GetConsultaPolicyVendas()
    {
        return Ok(new
        {
            resultado = "Dados que apenas usuários do setor Vendas podem acessar"
        });
    }
}