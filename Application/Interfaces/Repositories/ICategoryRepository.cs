using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryEntity>> GetListAsync();
        Task AddAsync(CategoryEntity category);
        Task DeleteAsync(Guid id);
    }
}
