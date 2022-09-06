using ExemploApiKeys.Data;
using ExemploApiKeys.Model;

namespace ExemploApiKeys.Services;

public class UsuarioService : IClienteService
{
    private readonly ApiKeyContext _context;

    public UsuarioService(
        ApiKeyContext context
    )
    {
        _context = context;
    }

    public Cliente VerificaApiKey(string apiKey)
    {
        var cliente = _context.Usuarios.FirstOrDefault(u => u.ApiKey == apiKey);

        return cliente;
    }
}