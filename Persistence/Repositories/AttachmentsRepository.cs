using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class AttachmentsRepository(
        ApplicationContext context,
        IStorageRepository storage) : IAttachmentsRepository
    {
        public async Task<AttachmentEntity> CreateAsync(IFormFile file, Guid UserId, AttachmentType type)
        {
            Guid objectId = await storage.PutObjectAsync(file, type);

            AttachmentEntity attachment = new()
            {
                Id = objectId,
                ContentType = file.ContentType,
                CreatedAt = DateTime.UtcNow,
                OriginalFileName = file.FileName,
                Size = file.Length,
                StorageKey = storage.GetFileKey(objectId, file.FileName, type),
                Type = type,
                UserId = UserId
            };

            await context.Attachments.AddAsync(attachment);

            return attachment;
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return context.Attachments.AnyAsync(attachment => attachment.Id == id);
        }

        public Task<AttachmentEntity?> GetByIdAsync(Guid id)
        {
            return context.Attachments.FirstOrDefaultAsync(attachment => attachment.Id == id);
        }
    }
}
