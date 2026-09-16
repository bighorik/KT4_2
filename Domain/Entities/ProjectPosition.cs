namespace Domain.Entities
{
    /// <summary>
    /// Вакансия в проекте
    /// </summary>
    public class ProjectPosition
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
        /// Перечень требуемых навыков кандидата
        /// </summary>
        public List<UserSkill> RequiredSkills { get; set; } = [];

        /// <summary>
        /// Идентификатор проекта вакансии
        /// </summary>
        public required Guid ProjectId { get; set; }

        /// <summary>
        /// Проект вакансии
        /// </summary>
        public required Project Project { get; set; }
    }
}
