using Application.Utils;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> CreateAsync(UserEntity user);
        Task<UserEntity?> GetByIdAsync(Guid Id);
        Task<UserEntity?> GetByEmailAsync(string email);
        Task MarkEmailAsConfirmedAsync(Guid Id);
        Task<bool> ExistsAsync(Guid Id);
        Task<bool> EmailIsAvailableAsync(string email);
    }
}
