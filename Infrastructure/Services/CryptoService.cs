using Application.Interfaces.Services;
using Domain.Settings;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using Argon2id.PasswordHasher;

namespace Infrastructure.Services
{
    public class CryptoService(IConfiguration configuration) : ICryptoService
    {
        private const string Alphabet =
            @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public string GenerateConfirmationCode()
        {
            return RandomNumberGenerator.GetString(Alphabet, configuration.GetCryptoSettings().EmailConfirmationCodeLenght);
        }

        public string GetPasswordHash(string password)
        {
            Argon2idPasswordHasher hasher = new();
            string hash = hasher.HashPassword(password);

            return hash;
        }

        public bool VerifyPassword(string password, string storedPasswordHash)
        {
            Argon2idPasswordHasher hasher = new();
            bool result = hasher.VerifyPassword(password, storedPasswordHash);
            return result;

        }

        public string GetRefreshTokenHash(string refreshToken)
        {
            using HMACSHA256 hmac = new(Encoding.UTF8.GetBytes(configuration.GetCryptoSettings().Pepper));
            return Convert.ToHexString(hmac.ComputeHash(Encoding.UTF8.GetBytes(refreshToken)));
        }

        public string GetUserAgentHash(string userAgent)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(userAgent);
            byte[] hash = SHA256.HashData(bytes);
            return Convert.ToHexString(hash);
        }

        public bool VerifyConfirmationCode(string password)
        {
            Argon2idPasswordHasher hasher = new();
            return hasher.VerifyPassword("correct horse battery staple", password);
        }
    }
}
