using Domain.Entities;
using WebApi.Fto.Attachments;
using WebApi.Fto.UserContact;
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
        public required string Nickname { get; set; }

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
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Почта подтверждена?
        /// </summary>
        public bool EmailConfirmed { get; set; } = false;

        /// <summary>
        /// Скиллы пользователя
        /// </summary>
        public List<UserSkillFto> Skills { get; set; } = [];

        /// <summary>
        /// Контакты пользователя
        /// </summary>
        public List<UserContactFto> Contacts { get; set; } = [];
    }
}
