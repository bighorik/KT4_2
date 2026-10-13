using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UserSkillRepository(
        ApplicationContext context) : IUserSkillRepository
    {
        public Task<bool> HasSkillAsync(Guid userId, Guid skillId)
        {
            return context.UserSkills.AnyAsync(skill => skill.UserId == userId && skill.SkillId == skillId);
        }

        public async Task<UserSkillEntity> CreateAsync(UserSkillEntity entity)
        {
            await context.UserSkills.AddAsync(entity);
            await context.Entry(entity).Reference(x => x.Skill).LoadAsync();
            return entity;
        }

        public Task<PositionSkillEntity> CreateAsync(PositionSkillEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            return context.UserSkills.Where(skill => skill.Id == id).ExecuteDeleteAsync();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return context.UserSkills.AnyAsync(skill => skill.Id == id);
        }

        public async Task<UserSkillEntity> UpdateSkillAsync(Guid userSkillId, Guid skillId)
        {
            await context.UserSkills
                 .Where(skill => skill.Id == userSkillId)
                 .ExecuteUpdateAsync(set => set.SetProperty(skill => skill.SkillId, skillId));

            return await context.UserSkills.FirstAsync(skill => skill.Id == userSkillId);
        }

        public async Task<UserSkillEntity> UpdateSkillLevelAsync(Guid userSkillId, SkillLevel level)
        {

            await context.UserSkills
             .Where(skill => skill.Id == userSkillId)
             .ExecuteUpdateAsync(set => set.SetProperty(skill => skill.SkillLevel, level));

            return await context.UserSkills.FirstAsync(skill => skill.Id == userSkillId);
        }
    }
}
