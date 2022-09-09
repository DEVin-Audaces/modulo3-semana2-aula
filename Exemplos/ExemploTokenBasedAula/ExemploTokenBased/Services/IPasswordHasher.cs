namespace ExemploTokenBased.Services
{
    public interface IPasswordHasher
    {
        string CriarHash(string senha);
        bool VerificarHash(string hash, string senha);
    }
}
