namespace ExemploBasicAuthentication.Services;

public interface IUsuarioService
{
    bool AutenticarUsuario(string nomeUsuario, string senha);
}