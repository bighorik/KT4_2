using Application.Behaviour.Skill;
using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class StudentGroupNumberRepository(
        ApplicationContext context) : IStudentGroupNumberRepository
    {
        public async Task<StudentGroupNumberEntity> CreateAsync(StudentGroupNumberEntity entity)
        {
            await context.StudentGroupNumbers.AddAsync(entity);

            return entity;
        }

        public Task DeleteByIdAsync(Guid id)
        {
            return context.StudentGroupNumbers.Where(skill => skill.Id == id).ExecuteDeleteAsync();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return context.StudentGroupNumbers.AnyAsync(skill => skill.Id == id);
        }

        public async Task<PaginationContract<StudentGroupNumberEntity>> GetPaginatedListAsync(GetStudentGroupNumbersQuery query)
        {
            IQueryable<StudentGroupNumberEntity> queryable
                 = context.StudentGroupNumbers
                          .Where(skill => query.Name != null && skill.Name.Contains(query.Name));

            List<StudentGroupNumberEntity> groups =
                   await queryable.Skip(query.Offset)
                                  .Take(query.Limit)
                                  .ToListAsync();

            int count = await queryable.CountAsync();

            PaginationContract<StudentGroupNumberEntity> result = new()
            {
                Limit = query.Limit,
                Total = count,
                Items = groups,
            };

            return result;
        }

        public async Task<StudentGroupNumberEntity> UpdateGroupNumberAsync(Guid skillId, string name)
        {
            await context.StudentGroupNumbers
             .Where(skill => skill.Id == skillId)
             .ExecuteUpdateAsync(set => set.SetProperty(skill => skill.Name, name));

            return await context.StudentGroupNumbers.FirstAsync(skill => skill.Id == skillId);
        }
    }
}
