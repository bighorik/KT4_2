using Domain.Entities;

namespace Application.Interfaces.Repositories.BaseRepositories
{
    public interface IRefreshTokenRepository :
        ICreateRepository<RefreshTokenEntity>
    {
        Task<RefreshTokenEntity?> GetActiveAsync(string refreshHash, string userAgentHash);
        Task MarkAsUsedAsync(string refreshHash);
    }
}
