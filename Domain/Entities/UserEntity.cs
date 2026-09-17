namespace Domain.Entities
{
    /// <summary>
    /// Пользователь сервиса
    /// </summary>
    public class UserEntity
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Никнейм пользователя
        /// </summary>
        public required string NickName { get; set; }

        /// <summary>
        /// Группа, к которой пренадлежит студент
        /// </summary>
        public required StudentGroupNumberEntity StudentGroupNumber { get; set; }

        /// <summary>
        /// Список навыков, которыми студент владеет
        /// </summary>
        public List<UserSkillEntity> SkillList { get; set; } = [];

        /// <summary>
        /// Ссылка на аватарку
        /// </summary>
        public string? AvatarUrl { get; set; }

        /// <summary>
        /// Описание пользователя
        /// </summary>
        public string Description = string.Empty;
    }
}

//Добавить описания вроде "готов уделять проекту такое-то время"