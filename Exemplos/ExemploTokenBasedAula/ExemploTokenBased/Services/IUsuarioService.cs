using ExemploTokenBased.Model;

namespace ExemploTokenBased.Services
{
    public interface IUsuarioService
    {
        Usuario AutenticarUsuario(string nomeUsuario, string senha);
    }
}
