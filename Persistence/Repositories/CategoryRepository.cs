using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class CategoryRepository(ApplicationContext context) : ICategoryRepository
    {
        public async Task AddAsync(CategoryEntity category)
        {

            if (!category.ParentCategoryId.HasValue || await context.Categories.AnyAsync(dbCategory => dbCategory.Id == category.ParentCategoryId.Value))
            {
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception();
            }

        }

        public async Task DeleteAsync(Guid id)
        {
            await context.Categories.Where(category => category.Id == id).ExecuteDeleteAsync();
            await context.SaveChangesAsync();
        }

        public Task<List<CategoryEntity>> GetListAsync()
        {
            return context.Categories.ToListAsync();
        }
    }
}
