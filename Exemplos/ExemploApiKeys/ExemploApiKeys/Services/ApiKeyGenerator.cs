using System.Security.Cryptography;

namespace ExemploApiKeys.Services
{
    public static class ApiKeyGenerator
    {
        public static string GenerateKey(int byteLength = 16)
        {
            Span<byte> buffer = byteLength > 4096
                    ? new byte[byteLength]
                    : stackalloc byte[byteLength];

            RandomNumberGenerator.Fill(buffer);

            return Convert.ToHexString(buffer);
        }
    }
}
