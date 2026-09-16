namespace Domain.Entities
{
    /// <summary>
    /// Заявка на вступление в проект
    /// </summary>
    public class MembershipApplication
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
        public required User User { get; set; }

        /// <summary>
        /// Идентификатор проекта
        /// </summary>
        public required Guid ProjectId { get; set; }

        /// <summary>
        /// Проект 
        /// </summary>
        public required Project Project { get; set; }

        /// <summary>
        /// Сопроводительное письмо заявки
        /// </summary>
        public string Message { get; set; } = "";
    }
}
