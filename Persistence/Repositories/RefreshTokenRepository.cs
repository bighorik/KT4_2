using Application.Interfaces.Repositories.BaseRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Persistence.Repositories
{
    public class RefreshTokenRepository(ApplicationContext context) : IRefreshTokenRepository
    {
        public async Task<RefreshTokenEntity> CreateAsync(RefreshTokenEntity entity)
        {
            await context.RefreshTokens.AddAsync(entity);
            return entity;
        }

        public Task<RefreshTokenEntity?> GetActiveAsync(string refreshHash, string userAgentHash)
        {
            return context.RefreshTokens
                          .Include(token => token.User)
                          .FirstOrDefaultAsync(token => token.TokenHash == refreshHash
                                                                   && token.UserAgentHash == userAgentHash
                                                                   && token.RevokedAt == null
                                                                   && token.ExpiresAt > DateTime.UtcNow);
        }

        public Task MarkAsUsedAsync(string refreshHash)
        {
            return context.RefreshTokens
                          .Where(token => token.TokenHash == refreshHash && token.RevokedAt == null)
                          .ExecuteUpdateAsync(set => set.SetProperty(token => token.RevokedAt, DateTime.UtcNow));
        }
    }
}
