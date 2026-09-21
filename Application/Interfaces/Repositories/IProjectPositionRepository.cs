using Application.Interfaces.Repositories.BaseRepositories;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IProjectPositionRepository :
        IExistsRepository,
        ICreateRepository<ProjectPositionEntity>,
        IDeleteByIdRepository
    {
    }
}
