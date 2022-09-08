using ExemploTokenBased.DTOs;
using ExemploTokenBased.Model;

namespace ExemploTokenBased.Services
{
    public interface ITokenService
    {
        JWTResult GerarJwt(Usuario usuario);
    }
}
