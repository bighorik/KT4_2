using Application.Utils;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IProjectPositionRepository
    {
        Task<RepositoryResult<ProjectPositionEntity>> AddPositionAsync(ProjectPositionEntity entity);

    }
}
