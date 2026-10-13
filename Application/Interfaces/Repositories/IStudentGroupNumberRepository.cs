using Application.Behaviour.Skill;
using Application.Interfaces.Repositories.BaseRepositories;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IStudentGroupNumberRepository :
        IExistsRepository,
        ICreateRepository<StudentGroupNumberEntity>,
        IDeleteByIdRepository,
        IGetPaginatedList<StudentGroupNumberEntity, GetStudentGroupNumbersQuery>
    {
        Task<StudentGroupNumberEntity> UpdateGroupNumberAsync(Guid skillId, string name);
    }
}
