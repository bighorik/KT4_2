using Application.Interfaces.Repositories.BaseRepositories;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface IInternetResourceLinksRepository :
        ICreateRepository<InternetResourceLinkEntity>,
        IDeleteByIdRepository,
        IExistsRepository
    {
        Task<InternetResourceLinkEntity> UpdateUrlAsync(Guid linkId, string url);
        Task<InternetResourceLinkEntity> UpdateTypeAsync(Guid linkId, InternetResourceLinkType type);
    }
}
