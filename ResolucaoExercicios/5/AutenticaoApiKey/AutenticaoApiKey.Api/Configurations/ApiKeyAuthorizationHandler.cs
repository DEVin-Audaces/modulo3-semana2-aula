using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace AutenticaoApiKey.Api.Configurations
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private static List<string> ApiKeysValidas = new()
        {
            "1125fffb-0a9e-4dcf-9211-3396589c1cbe",
            "85f806a6-9740-4cba-957c-3cf480aee497",
            "eb636bd0-0f20-46ce-a2db-26c98f7a07f7"
        };

        public const string HeaderAuthentication = "X-API-KEY";
        public const string SchemeName = "ApiKey";

        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory factory,
            UrlEncoder encoder,
            ISystemClock systemClock
        ) : base(options, factory, encoder, systemClock)
        {

        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var valorHeader = Request.Headers[HeaderAuthentication].ToString();

            if (string.IsNullOrEmpty(valorHeader))
            {
                return Task.FromResult(
                    AuthenticateResult.Fail("Header não adicionado na request")
                );
            }

            var apiKeyValida = ApiKeysValidas.Contains(valorHeader);

            if (!apiKeyValida)
            {
                return Task.FromResult(
                    AuthenticateResult.Fail("ApiKey é inválida")
                );
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Cliente padrão")
            };

            var identidade = new ClaimsIdentity(
                claims,
                Scheme.Name // ApiKey
            );

            var principal = new ClaimsPrincipal(identidade);

            var ticket = new AuthenticationTicket(
                principal,
                Scheme.Name
            );

            return Task.FromResult(
                AuthenticateResult.Success(ticket)
            );
        }
    }
}
