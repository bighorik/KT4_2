using Domain.Enums;

namespace WebApi.Fto.Attachments
{
    public class AttachmentFto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public AttachmentType Type { get; set; }
        public required string StorageKey { get; set; }
        public required string OriginalFileName { get; set; }
        public required string ContentType { get; set; }
        public required long Size { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
