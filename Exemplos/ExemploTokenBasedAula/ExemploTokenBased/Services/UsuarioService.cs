using ExemploTokenBased.Data;
using ExemploTokenBased.Model;

namespace ExemploTokenBased.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly TokenBasedContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UsuarioService(
            TokenBasedContext context, 
            IPasswordHasher passwordHasher
        )
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public Usuario AutenticarUsuario(string nomeUsuario, string senha)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.NomeUsuario == nomeUsuario);

            if (usuario == null)
            {
                return null;
            }

            var verificacaoSenha = _passwordHasher.VerificarHash(
                usuario.HashSenha,
                senha
            );

            if(verificacaoSenha)
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
