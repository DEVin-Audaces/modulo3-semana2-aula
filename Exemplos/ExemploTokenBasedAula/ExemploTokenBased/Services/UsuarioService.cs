using ExemploTokenBased.Data;
using ExemploTokenBased.Model;

namespace ExemploTokenBased.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly TokenBasedContext _context;

        public UsuarioService(TokenBasedContext context)
        {
            _context = context;
        }

        public Usuario AutenticarUsuario(string nomeUsuario, string senha)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.NomeUsuario == nomeUsuario);

            if (usuario == null)
            {
                return null;
            }

            if(usuario.HashSenha == senha)
            {
                return usuario;
            }
            else
            {
                return null;
            }
        }
    }
}
