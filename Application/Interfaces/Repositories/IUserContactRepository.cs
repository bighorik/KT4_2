using Application.Interfaces.Repositories.BaseRepositories;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface IUserContactRepository :
        ICreateRepository<UserContactEntity>,
        IDeleteByIdRepository,
        IExistsRepository
    {
        Task<UserContactEntity> UpdateValueAsync(Guid contactId, string value);
        Task<UserContactEntity> UpdateTypeAsync(Guid contactId, UserContactType type);
    }
}
