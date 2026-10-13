using Application.Interfaces.Repositories.BaseRepositories;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository :
        ICreateRepository<UserEntity>,
        IGetByIdRepository<UserEntity>,
        IExistsRepository
    {
        Task<UserEntity?> GetByEmailAsync(string email);
        Task<UserEntity> UpdateUserNicknameAsync(Guid id, string nickname);
        Task<UserEntity> UpdateUserDescriptionAsync(Guid id, string description);
        Task MarkEmailAsConfirmedAsync(Guid id);
        Task<bool> EmailIsAvailableAsync(string email);
        public Task DeleteNotConfirmedByEmail(string email);
    }
}
