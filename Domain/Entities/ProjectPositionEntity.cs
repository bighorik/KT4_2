namespace Domain.Entities
{
    /// <summary>
    /// Вакансия в проекте
    /// </summary>
    public class ProjectPositionEntity
    {
        /// <summary>
        /// Идентификатор вакансии
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Имя вакансии
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Описание позиции
        /// </summary>
        public string? Description { get; set; } = "";

        /// <summary>
        /// Перечень требуемых навыков кандидата
        /// </summary>
        public List<PositionSkillEntity> PositionSkills { get; set; } = [];

        /// <summary>
        /// Идентификатор проекта вакансии
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Проект вакансии
        /// </summary>
        public ProjectEntity Project { get; set; }

        public List<MembershipApplicationEntity> MembershipApplications { get; set; } = [];

        public ProjectMemberEntity? ProjectMember { get; set; }
    }
}
