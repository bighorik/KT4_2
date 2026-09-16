using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Навык пользователя
    /// </summary>
    public class UserSkill
    {
        /// <summary>
        /// Идентификатор навыка пользователя
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Идентификатор справочного навыка
        /// </summary>
        public required Guid SkillId { get; set; }

        /// <summary>
        /// Навык справочного
        /// </summary>
        public required Skill Skill { get; set; }

        /// <summary>
        /// Степень владения навыком
        /// </summary>
        public SkillLevel SkillLevel { get; set; }
    }
}
