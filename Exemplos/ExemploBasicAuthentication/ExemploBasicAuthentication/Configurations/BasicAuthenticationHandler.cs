using ExemploBasicAuthentication.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace ExemploBasicAuthentication.Configurations
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUsuarioService _userService;

        public BasicAuthenticationHandler(
            IUsuarioService userService,
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        )
            : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (string.IsNullOrEmpty(Request.Headers["Authorization"]))
            {
                return AuthenticateResult.Fail("Autenticação falhou, header não enviado");
            }

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

            if (string.IsNullOrEmpty(authHeader.Parameter))
            {
                return AuthenticateResult.Fail("Autenticação falhou, header não enviado");
            }
            
            var corpoBase64 = Convert.FromBase64String(authHeader.Parameter);
            var credenciais = Encoding.UTF8.GetString(corpoBase64).Split(':');

            if(credenciais.Length != 2)
            {
                return AuthenticateResult.Fail("Autenticação falhou, header inválido");
            }

            var nomeUsuario = credenciais.FirstOrDefault();
            var senha = credenciais.LastOrDefault();

            if (!_userService.AutenticarUsuario(nomeUsuario, senha)) 
                return AuthenticateResult.Fail("Autenticação falhou, credenciais inválidas");

            var claims = new[] {
                new Claim(ClaimTypes.Name, nomeUsuario)
            };
            var identity = new ClaimsIdentity(
                claims,
                Scheme.Name
            );
            var principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

    }
}
