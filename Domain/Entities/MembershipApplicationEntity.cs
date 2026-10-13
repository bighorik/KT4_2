using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Заявка на вступление в проект
    /// </summary>
    public class MembershipApplicationEntity
    {
        /// <summary>
        /// Идентефикатор заявки на вступление
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public required Guid UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public UserEntity User { get; set; }

        /// <summary>
        /// Идентификатор проекта
        /// </summary>
        public required Guid PositionId { get; set; }

        /// <summary>
        /// Проект 
        /// </summary>
        public ProjectPositionEntity Position { get; set; }

        /// <summary>
        /// Сопроводительное письмо заявки
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// Статус заявки
        /// </summary>
        public required MembershipApplicationStatus Status { get; set; }
    }
}
