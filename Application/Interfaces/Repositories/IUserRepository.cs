using Application.Utils;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<RepositoryResult<UserEntity>> GetByIdAsync(Guid Id);
        Task<bool> ExistsAsync(Guid Id);
    }
}
