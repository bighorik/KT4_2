using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Repositories
{
    public interface IStorageRepository
    {
        public Task PutObjectAsync(IFormFile file, AttachmentType type);
    }
}
