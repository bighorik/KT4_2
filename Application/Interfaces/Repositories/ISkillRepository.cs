using Application.Behaviour.Skill;
using Application.Interfaces.Repositories.BaseRepositories;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ISkillRepository :
        IExistsRepository,
        ICreateRepository<SkillEntity>,
        IDeleteByIdRepository,
        IGetPaginatedList<SkillEntity, GetSkillsQuery>
    {
        Task<SkillEntity> UpdateSkillNameAsync(Guid skillId, string name);
        Task<SkillEntity?> GetNotAddedAsync(Guid userId);
    }
}
