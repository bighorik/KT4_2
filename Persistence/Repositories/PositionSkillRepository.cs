using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class PositionSkillRepository(
        ApplicationContext context) : IPositionSkillRepository
    {
        public Task<bool> AlreadyAddedAsync(Guid positionId, Guid skillId)
        {
            return context.PositionSkills.AnyAsync(skill => skill.SkillId == skillId && skill.ProjectPositionId == positionId);
        }

        public async Task<PositionSkillEntity> CreateAsync(PositionSkillEntity entity)
        {
            await context.PositionSkills.AddAsync(entity);

            return entity;
        }

        public Task DeleteByIdAsync(Guid id)
        {
            return context.PositionSkills.Where(skill => skill.Id == id).ExecuteDeleteAsync();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return context.PositionSkills.AnyAsync(skill => skill.Id == id);
        }

        public async Task<PositionSkillEntity> UpdateSkillLevelAsync(Guid positionSkillId, SkillLevel level)
        {

            await context.PositionSkills
             .Where(skill => skill.Id == positionSkillId)
             .ExecuteUpdateAsync(set => set.SetProperty(skill => skill.SkillLevel, skill => level));

            return await context.PositionSkills.FirstAsync(skill => skill.Id == positionSkillId);
        }
    }
}
