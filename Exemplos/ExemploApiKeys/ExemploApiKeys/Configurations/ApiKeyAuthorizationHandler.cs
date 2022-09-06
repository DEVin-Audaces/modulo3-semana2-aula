using ExemploApiKeys.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace ExemploApiKeys.Configurations
{
    public class ApiKeyAuthorizationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IClienteService _clienteService;

        public ApiKeyAuthorizationHandler(
            IClienteService userService,
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        )
            : base(options, logger, encoder, clock)
        {
            _clienteService = userService;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (string.IsNullOrEmpty(Request.Headers["XApiKey"]))
            {
                return Task.FromResult(AuthenticateResult.Fail("Autenticação falhou, header não enviado"));
            }

            var cliente = _clienteService.VerificaApiKey(Request.Headers["XApiKey"]);

            if (cliente == null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Autenticação falhou, api key inválida"));
            }

            if (!cliente.Ativo)
            {
                return Task.FromResult(AuthenticateResult.Fail("Autenticação falhou, api key inativa"));
            }

            var claims = new[] {
                new Claim(ClaimTypes.Name, cliente.Nome)
            };
            var identity = new ClaimsIdentity(
                claims,
                Scheme.Name
            );
            var principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
