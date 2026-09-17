using Application.Utils;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IAttachmentsRepository
    {
        Task<RepositoryResult<AttachmentEntity>> AddAttachmentAsync(IFormFile file);
        Task<RepositoryResult<AttachmentEntity>> GetByIdAsync(Guid Id);
        Task<bool> ExistsAsync(Guid Id);
    }
}
