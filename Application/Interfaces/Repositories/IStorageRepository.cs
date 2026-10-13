using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Repositories
{
    public interface IStorageRepository
    {
        public Task<Guid> PutObjectAsync(IFormFile file, AttachmentType type);
        public string GetFileKey(Guid objectId, string fileName, AttachmentType type);
    }
}
