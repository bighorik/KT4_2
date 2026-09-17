namespace Domain.Entities
{
    /// <summary>
    /// Участник проекта
    /// </summary>
    public class ProjectMemberEntity
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
        public required UserEntity User { get; set; }

        /// <summary>
        /// Идентификатор проекта
        /// </summary>
        public required Guid ProjectId { get; set; }

        /// <summary>
        /// Проект 
        /// </summary>
        public required ProjectEntity Project { get; set; }
    }
}
