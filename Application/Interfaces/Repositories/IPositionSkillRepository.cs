using Application.Interfaces.Repositories.BaseRepositories;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface IPositionSkillRepository :
        IExistsRepository,
        ICreateRepository<PositionSkillEntity>,
        IDeleteByIdRepository
    {
        Task<PositionSkillEntity> UpdateSkillLevelAsync(Guid positionSkillId, SkillLevel level);
        Task<bool> AlreadyAddedAsync(Guid positionId, Guid skillId);
    }
}
