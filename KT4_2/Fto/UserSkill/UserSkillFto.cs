using Domain.Enums;

namespace WebApi.Fto.UserSkill
{
    public class UserSkillFto
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
        /// Название скилла
        /// </summary>
        public required string SkillName { get; set; }

        /// <summary>
        /// Степень владения навыком
        /// </summary>
        public SkillLevel SkillLevel { get; set; }

        /// <summary>
        /// Идентификатор пользователя, загрузившего файл
        /// </summary>
    }
}
