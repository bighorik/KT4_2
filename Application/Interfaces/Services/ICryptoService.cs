namespace Application.Interfaces.Services
{
    public interface ICryptoService
    {
        string GetPasswordHash(string password);
        string GetRefreshTokenHash(string refreshToken);
        string GetUserAgentHash(string userAgent);
        string GenerateConfirmationCode();
        bool VerifyConfirmationCode(string password);
        bool VerifyPassword(string password, string storedPasswordHash);
    }
}
