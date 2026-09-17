using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ISkillRepository
    {
        Task<bool> ExistsAsync(Guid Id);

        SkillEntity GetStub(Guid Id);
        List<SkillEntity> GetStubList(IEnumerable<Guid> Id);
    }
}
