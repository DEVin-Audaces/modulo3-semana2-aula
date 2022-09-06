using ExemploApiKeys.Model;

namespace ExemploApiKeys.Services;

public interface IClienteService
{
    Cliente VerificaApiKey(string apiKey);
}