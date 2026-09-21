using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IStudentGroupRepository
    {
        Task<bool> ExistsAsync(Guid id);

        StudentGroupNumberEntity GetStub(Guid id);
    }
}
