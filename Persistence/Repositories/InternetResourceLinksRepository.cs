using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class InternetResourceLinksRepository(
        ApplicationContext context) : IInternetResourceLinksRepository
    {
        public async Task<InternetResourceLinkEntity> CreateAsync(InternetResourceLinkEntity entity)
        {
            await context.InternetResourceLinks.AddAsync(entity);

            return entity;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await context.InternetResourceLinks.Where(link => link.Id == id).ExecuteDeleteAsync();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return context.InternetResourceLinks.AnyAsync(link => link.Id == id);
        }

        public async Task<InternetResourceLinkEntity> UpdateTypeAsync(Guid linkId, InternetResourceLinkType type)
        {
            await context.InternetResourceLinks
                         .Where(link => link.Id == linkId)
                         .ExecuteUpdateAsync(set => set.SetProperty(link => link.Type, type));

            return await context.InternetResourceLinks.FirstAsync(link => link.Id == linkId);
        }

        public async Task<InternetResourceLinkEntity> UpdateUrlAsync(Guid linkId, string url)
        {
            await context.InternetResourceLinks
                         .Where(link => link.Id == linkId)
                         .ExecuteUpdateAsync(set => set.SetProperty(link => link.Url, url));

            return await context.InternetResourceLinks.FirstAsync(link => link.Id == linkId);
        }
    }
}
