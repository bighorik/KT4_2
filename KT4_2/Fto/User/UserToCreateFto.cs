namespace WebApi.Fto.User
{
    /// <summary>
    /// Dto для создания пользователя
    /// </summary>
    public class UserToCreateFto
    {
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
        public required string Password { get; set; }

        /// <summary>
        /// Группа, к которой пренадлежит студент
        /// </summary>
        public required Guid StudentGroupNumberId { get; set; }
    }
}
