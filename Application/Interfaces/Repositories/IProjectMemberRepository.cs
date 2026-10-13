using Application.Interfaces.Repositories.BaseRepositories;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IProjectMemberRepository :
        ICreateRepository<ProjectMemberEntity>,
        IGetFullIncludedByIdRepository<ProjectMemberEntity>,
        IDeleteByIdRepository,
        IExistsRepository
    {
    }
}