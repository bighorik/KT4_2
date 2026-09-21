using Domain.Entities;
using WebApi.Fto.Attachments;
using WebApi.Fto.UserSkill;

namespace WebApi.Fto.User
{
    /// <summary>
    /// Dto пользователя
    /// </summary>
    public class UserFto
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
        /// Группа, к которой пренадлежит студент
        /// </summary>
        public required StudentGroupNumberEntity StudentGroupNumber { get; set; }

        /// <summary>
        /// Ссылка на аватарку
        /// </summary>
        public AttachmentFto? Avatar { get; set; }

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
        public List<UserSkillFto> UserSkills { get; set; } = [];
    }
}
