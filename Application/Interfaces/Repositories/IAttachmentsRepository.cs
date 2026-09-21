using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Repositories
{
    public interface IAttachmentsRepository
    {
        Task<AttachmentEntity> CreateAsync(IFormFile file, Guid UserId, AttachmentType type);
        Task<AttachmentEntity> GetByIdAsync(Guid Id);

        /// <summary>
        /// Проверяет существование вложения в базе по ID и в случае успеха возвращает заглушку.
        /// </summary>
        /// <returns>
        /// <see cref="Task{RepositoryResult{AttachmentEntity}}"/>.
        /// Если вложение не найдено, возвращает статус код 
        /// <see cref="RepositoryResult.NotFound"/>.
        /// </returns>
        Task<RepositoryResult<AttachmentEntity>> GetStubById(Guid Id);
        Task<bool> ExistsAsync(Guid Id);
    }
}
