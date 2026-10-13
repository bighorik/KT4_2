using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Контакт пользователя для связи
    /// </summary>
    public class UserContactEntity
    {
        /// <summary>
        /// Идентефикатор контакта
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Тип контакта
        /// </summary>
        public required UserContactType Type { get; set; }

        /// <summary>
        /// Контакт
        /// </summary>
        public required string Value { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Пользователь 
        /// </summary>
        public UserEntity User { get; set; }
    }
}
