using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProjectMemberRepository(
        ApplicationContext context) : IProjectMemberRepository
    {
        public async Task<ProjectMemberEntity> CreateAsync(ProjectMemberEntity entity)
        {
            await context.ProjectMember.AddAsync(entity);

            return entity;
        }

        public Task DeleteByIdAsync(Guid id)
        {
            return context.ProjectMember.Where(member => member.Id == id).ExecuteDeleteAsync();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return context.ProjectMember.AnyAsync(member => member.Id == id);
        }

        public Task<ProjectMemberEntity?> GetFullIncludedByIdAsync(Guid id)
        {
            return context.ProjectMember
                          .Where(member => member.Id == id)
                          .Include(member => member.Project)
                          .Include(member => member.User)
                          .FirstOrDefaultAsync();
        }
    }
}
