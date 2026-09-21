using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IInternetResourceLingRepository
    {
        Task<InternetResourceLinkEntity> CreateAsync(InternetResourceLinkEntity link);
    }
}
