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
        /// Почта пользователя
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public required string PasswordHash { get; set; }

        /// <summary>
        /// Группа, к которой пренадлежит студент
        /// </summary>
        public required StudentGroupNumberEntity StudentGroupNumber { get; set; }

        /// <summary>
        /// Ссылка на аватарку
        /// </summary>
        public string? AvatarUrl { get; set; }

        /// <summary>
        /// Описание пользователя
        /// </summary>
        public string Description = string.Empty;

        /// <summary>
        /// Почта подтверждена?
        /// </summary>
        public bool EmailConfirmed = false;

        /// <summary>
        /// Скиллы пользователя
        /// </summary>
        public List<UserSkillEntity> UserSkills { get; set; } = [];
    }
}

//Добавить описания вроде "готов уделять проекту такое-то время"