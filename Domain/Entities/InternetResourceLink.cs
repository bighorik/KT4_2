using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Ссылка на страницу проекта
    /// </summary>
    public class InternetResourceLink
    {
        /// <summary>
        /// Идентефикатор ссылки
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Тип ресурса
        /// </summary>
        public required InternetResourceLinkType Type { get; set; }

        /// <summary>
        /// Ссылка на ресурс
        /// </summary>
        public required string Url { get; set; }

        /// <summary>
        /// Идентификатор проекта
        /// </summary>
        public required Guid ProjectId { get; set; }

        /// <summary>
        /// Проект 
        /// </summary>
        public required Project Project { get; set; }
    }
}
