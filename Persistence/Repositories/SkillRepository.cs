using Application.Behaviour.Skill;
using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class SkillRepository(
        ApplicationContext context) : ISkillRepository
    {
        public async Task<SkillEntity> CreateAsync(SkillEntity entity)
        {
            await context.Skills.AddAsync(entity);

            return entity;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await context.Skills
                         .Where(skill => skill.Id == id)
                         .ExecuteUpdateAsync(set => set.SetProperty(skill => skill.IsArchived, true));
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return context.Skills.AnyAsync(skill => skill.Id == id && skill.IsArchived == false);
        }

        public Task<SkillEntity?> GetNotAddedAsync(Guid userId)
        {
            return context.Skills.FirstOrDefaultAsync(skill => !context.UserSkills.Any(userSkill => userSkill.UserId == userId && userSkill.SkillId == skill.Id));
        }

        public async Task<PaginationContract<SkillEntity>> GetPaginatedListAsync(GetSkillsQuery query)
        {
            IQueryable<SkillEntity> queryable
                 = context.Skills
                          .Where(skill => query.Name == null || skill.Name.Contains(query.Name));

            List<SkillEntity> skills =
                   await queryable.Skip(query.Offset)
                                  .Take(query.Limit)
                                  .ToListAsync();

            int count = await queryable.CountAsync();

            PaginationContract<SkillEntity> result = new()
            {
                Limit = query.Limit,
                Total = count,
                Items = skills,
            };

            return result;
        }

        public async Task<SkillEntity> UpdateSkillNameAsync(Guid skillId, string name)
        {
            await context.Skills
                         .Where(skill => skill.Id == skillId)
                         .ExecuteUpdateAsync(set => set.SetProperty(skill => skill.Name, name));

            return await context.Skills.FirstAsync(skill => skill.Id == skillId);
        }
    }
}
