using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface ICryptoService
    {
        string GetPasswordHash(string password);

        string GenerateConfirmationCode();
    }
}
