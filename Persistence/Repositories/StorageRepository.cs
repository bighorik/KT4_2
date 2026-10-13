using Application.Interfaces.Repositories;
using Domain.Enums;
using Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;

namespace Persistence.Repositories
{
    public class StorageRepository(
        IMinioClient client,
        IConfiguration configuration) : IStorageRepository
    {
        public string GetFileKey(Guid objectId, string fileName, AttachmentType type)
        => $"{type}/{objectId}{Path.GetExtension(fileName)}";
        public async Task<Guid> PutObjectAsync(IFormFile file, AttachmentType type)
        {
            if (file is null || file.Length == 0)
                throw new ArgumentException("Файл не передан", nameof(file));

            Guid objectId = Guid.NewGuid();

            await using var stream = file.OpenReadStream();

            PutObjectArgs args = new PutObjectArgs()
                .WithBucket(configuration.GetStorageSettings().Bucket)
                .WithObject(GetFileKey(objectId, file.Name, type))
                .WithObjectSize(file.Length)
                .WithStreamData(file.OpenReadStream())
                .WithContentType(file.ContentType);

            await client.PutObjectAsync(args);

            return objectId;
        }
    }
}
