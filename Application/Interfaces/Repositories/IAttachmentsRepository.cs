using Application.Interfaces.Repositories.BaseRepositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Repositories
{
    public interface IAttachmentsRepository :
        IGetByIdRepository<AttachmentEntity>,
        IExistsRepository
    {
        Task<AttachmentEntity> CreateAsync(IFormFile file, Guid UserId, AttachmentType type);
    }
}
