namespace Domain.Entities
{
    /// <summary>
    /// Участник проекта
    /// </summary>
    public class ProjectMember
    {
        /// <summary>
        /// Идентефикатор участника проекта
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
    }
}
