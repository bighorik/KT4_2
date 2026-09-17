using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Навык пользователя
    /// </summary>
    public class UserSkillEntity
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
        public required SkillEntity Skill { get; set; }

        /// <summary>
        /// Степень владения навыком
        /// </summary>
        public SkillLevel SkillLevel { get; set; }

        /// <summary>
        /// Идентификатор пользователя, загрузившего файл
        /// </summary>
        public required Guid UserId { get; set; }

        /// <summary>
        /// Пользователь, загрузивший файл
        /// </summary>
        public required UserEntity User { get; set; }
    }
}
