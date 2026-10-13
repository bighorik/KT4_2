using Application.Interfaces.Repositories.BaseRepositories;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface IUserSkillRepository :
        IExistsRepository,
        ICreateRepository<UserSkillEntity>,
        IDeleteByIdRepository
    {
        Task<UserSkillEntity> UpdateSkillLevelAsync(Guid userSkillId, SkillLevel level);
        Task<UserSkillEntity> UpdateSkillAsync(Guid userSkillId, Guid skillId);
        Task<bool> HasSkillAsync(Guid userId, Guid skillId);
    }
}
