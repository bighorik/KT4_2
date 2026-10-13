using Application.Interfaces.Repositories.BaseRepositories;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IProjectPositionRepository :
        IExistsRepository,
        IGetByIdRepository<ProjectPositionEntity>,
        ICreateRepository<ProjectPositionEntity>,
        IDeleteByIdRepository
    {
        Task<ProjectPositionEntity> UpdateNameAsync(Guid positionId, string name);
        Task<ProjectPositionEntity> UpdateDescriptionAsync(Guid positionId, string description);
    }
}
