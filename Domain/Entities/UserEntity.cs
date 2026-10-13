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
        public required string Nickname { get; set; }

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public required string PasswordHash { get; set; }

        /// <summary>
        /// Id группы, к которой пренадлежит студент
        /// </summary>
        public required Guid StudentGroupNumberId { get; set; }

        /// <summary>
        /// Id группы, к которой пренадлежит студент
        /// </summary>
        public StudentGroupNumberEntity StudentGroupNumber { get; set; }

        /// <summary>
        /// Ссылка на аватарку
        /// </summary>
        public string? AvatarUrl { get; set; }

        /// <summary>
        /// Описание пользователя
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Почта подтверждена?
        /// </summary>
        public bool EmailConfirmed { get; set; } = false;

        /// <summary>
        /// Скиллы пользователя
        /// </summary>
        public List<UserSkillEntity> Skills { get; set; } = [];

        /// <summary>
        /// Контакты пользователя
        /// </summary>
        public List<UserContactEntity> Contacts { get; set; } = [];
    }
}

//Добавить описания вроде "готов уделять проекту такое-то время"