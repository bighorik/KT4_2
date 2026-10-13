using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UserContactRepository(
        ApplicationContext context) : IUserContactRepository
    {
        public async Task<UserContactEntity> CreateAsync(UserContactEntity entity)
        {
            await context.UserContacts.AddAsync(entity);

            return entity;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await context.UserContacts.Where(contact => contact.Id == id).ExecuteDeleteAsync();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return context.UserContacts.AnyAsync(contact => contact.Id == id);
        }

        public async Task<UserContactEntity> UpdateTypeAsync(Guid contactId, UserContactType type)
        {
            await context.UserContacts
                         .Where(contact => contact.Id == contactId)
                         .ExecuteUpdateAsync(set => set.SetProperty(contact => contact.Type, type));

            return await context.UserContacts.FirstAsync(contact => contact.Id == contactId);
        }

        public async Task<UserContactEntity> UpdateValueAsync(Guid contactId, string value)
        {
            await context.UserContacts
                         .Where(contact => contact.Id == contactId)
                         .ExecuteUpdateAsync(set => set.SetProperty(contact => contact.Value, value));

            return await context.UserContacts.FirstAsync(contact => contact.Id == contactId);
        }
    }
}
