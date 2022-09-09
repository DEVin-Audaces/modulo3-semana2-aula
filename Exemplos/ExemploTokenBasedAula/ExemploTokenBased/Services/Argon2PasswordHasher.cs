using Isopoh.Cryptography.Argon2;

namespace ExemploTokenBased.Services
{
    public class Argon2PasswordHasher : IPasswordHasher
    {
        public string CriarHash(string senha)
        {
            return Argon2.Hash(senha);
        }

        public bool VerificarHash(string hash, string senha)
        {
            return Argon2.Verify(hash, senha);
        }
    }
}
