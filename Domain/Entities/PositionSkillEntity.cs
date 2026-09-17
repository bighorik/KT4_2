using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Навык пользователя
    /// </summary>
    public class PositionSkillEntity
    {
        /// <summary>
        /// Идентификатор навыка пользователя
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Идентификатор справочного навыка
        /// </summary>
        public Guid SkillId { get; set; }

        /// <summary>
        /// Навык справочного
        /// </summary>
        public SkillEntity Skill { get; set; }

        /// <summary>
        /// Степень владения навыком
        /// </summary>
        public SkillLevel SkillLevel { get; set; }

        /// <summary>
        /// Идентификатор вакансии
        /// </summary>
        public Guid ProjectPositionId { get; set; }

        /// <summary>
        /// Вакансия
        /// </summary>
        public ProjectPositionEntity ProjectPosition { get; set; }
    }
}
