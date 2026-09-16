using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Пользователськое влоежние
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Идентефикатор вложения
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя, загрузившего файл
        /// </summary>
        public required Guid UserId { get; set; }

        /// <summary>
        /// Пользователь, загрузивший файл
        /// </summary>
        public required User User { get; set; }

        /// <summary>
        /// Тип вложения
        /// </summary>
        public AttachmentType Type { get; set; }

        /// <summary>
        /// Ключ объекта
        /// </summary>
        public required string StorageKey { get; set; } 

        /// <summary>
        /// Оригинальное название файла
        /// </summary>
        public required string OriginalFileName { get; set; }

        /// <summary>
        /// MIME-тип файла
        /// </summary>
        public required string ContentType { get; set; }

        /// <summary>
        /// Размер файла в байтах
        /// </summary>
        public required long Size { get; set; }

        /// <summary>
        /// Дата загрузки файла
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
