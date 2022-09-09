using ExemploTokenBased.DTOs;
using ExemploTokenBased.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace ExemploTokenBased.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public const string ClaimSetorUsuario = "audaces-setor";

        public JWTResult GerarJwt(Usuario usuario)
        {
            // Geração do token JWT
            var identidadeUsuario = ObterIdentidadePorUsuario(usuario);
            var geradoEm = DateTime.UtcNow;
            var expiraEm = geradoEm.AddHours(1);

            var handler = new JwtSecurityTokenHandler();

            var propriedadesToken = new SecurityTokenDescriptor()
            {
                Issuer = "DevInAudaces",
                IssuedAt = geradoEm,
                Expires = expiraEm,
                Subject = identidadeUsuario,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT_SECRET"))
                    ),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var accessToken = handler.WriteToken(
                handler.CreateToken(propriedadesToken)
            );

            return new JWTResult
            {
                AccessToken = accessToken,
                ExpiraEm = expiraEm
            };
        }

        private ClaimsIdentity ObterIdentidadePorUsuario(
            Usuario usuario
        )
        {
            var claimsUsuario = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.NomeUsuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, usuario.Papel.ToString()),
                new Claim(ClaimSetorUsuario, usuario.Setor)
            };

            return new ClaimsIdentity(
                new GenericIdentity(usuario.NomeUsuario),
                claimsUsuario
            );
        }
    }
}
