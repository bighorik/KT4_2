using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UserRepository(
        ApplicationContext context) : IUserRepository
    {
        public async Task<UserEntity> CreateAsync(UserEntity entity)
        {
            await context.Users.AddAsync(entity);

            return entity;
        }

        public async Task DeleteNotConfirmedByEmail(string email)
        {
            var query = context.Users
                .Where(user => user.Email == email && user.EmailConfirmed);

            Console.WriteLine(query.ToQueryString());

            var provider = context.Database.ProviderName;
            await context.Users
                         .Where(user => user.Email == email && user.EmailConfirmed)
                         .ExecuteDeleteAsync();
        }

        public async Task<bool> EmailIsAvailableAsync(string email)
        {
            return !(await context.Users.AnyAsync(context => context.Email == email));
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return context.Users.AnyAsync(user => user.Id == id);
        }

        public Task<UserEntity?> GetByEmailAsync(string email)
        {
            return context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public Task<UserEntity?> GetByIdAsync(Guid id)
        {
            return context.Users
                          .Include(user => user.Skills)
                          .ThenInclude(skill => skill.Skill)
                          .Include(user => user.Contacts)
                          .Include(user => user.StudentGroupNumber)
                          .FirstOrDefaultAsync(user => user.Id == id);
        }

        public Task MarkEmailAsConfirmedAsync(Guid id)
        {
            return context.Users
                          .Where(user => user.Id == id)
                          .ExecuteUpdateAsync(set => set.SetProperty(user => user.EmailConfirmed, true));
        }

        public async Task<UserEntity> UpdateUserDescriptionAsync(Guid id, string description)
        {
            await context.Users.Where(user => user.Id == id)
                .ExecuteUpdateAsync(set => set.SetProperty(user => user.Description, description));

            return await context.Users.FirstAsync(user => user.Id == id);
        }

        public async Task<UserEntity> UpdateUserNicknameAsync(Guid id, string nickname)
        {
            await context.Users.Where(user => user.Id == id)
                .ExecuteUpdateAsync(set => set.SetProperty(user => user.Nickname, nickname));

            return await context.Users.FirstAsync(user => user.Id == id);
        }
    }
}
