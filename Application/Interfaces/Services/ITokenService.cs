using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface ITokenService
    {
        (string AccessToken, string RefreshToken) GenerateTokens(UserEntity user);
    }
}
