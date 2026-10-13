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
        public UserEntity User { get; set; }

        /// <summary>
        /// Идентификатор проекта
        /// </summary>
        public required Guid ProjectId { get; set; }

        /// <summary>
        /// Проект 
        /// </summary>
        public ProjectEntity Project { get; set; }

        public required Guid PositionId { get; set; }

        public ProjectPositionEntity Position { get; set; }
    }
}
