using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{

    public class ProjectPositionRepository(
        ApplicationContext context) : IProjectPositionRepository
    {
        public async Task<ProjectPositionEntity> CreateAsync(ProjectPositionEntity entity)
        {
            await context.ProjectPositions.AddAsync(entity);

            return entity;
        }

        public Task DeleteByIdAsync(Guid id)
        {
            return context.ProjectPositions.Where(position => position.Id == id).ExecuteDeleteAsync();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return context.ProjectPositions.AnyAsync(position => position.Id == id);
        }

        public Task<ProjectPositionEntity?> GetByIdAsync(Guid id)
        {
            return context.ProjectPositions.FirstOrDefaultAsync(position => position.Id == id);
        }

        public async Task<ProjectPositionEntity> UpdateDescriptionAsync(Guid positionId, string description)
        {
            await context.ProjectPositions
                         .Where(position => position.Id == positionId)
                         .ExecuteUpdateAsync(set => set.SetProperty(position => position.Description, description));

            return await context.ProjectPositions.FirstAsync(position => position.Id == positionId);
        }

        public async Task<ProjectPositionEntity> UpdateNameAsync(Guid positionId, string name)
        {
            await context.ProjectPositions
                         .Where(position => position.Id == positionId)
                         .ExecuteUpdateAsync(set => set.SetProperty(position => position.Name, name));

            return await context.ProjectPositions.FirstAsync(position => position.Id == positionId);
        }
    }
}
