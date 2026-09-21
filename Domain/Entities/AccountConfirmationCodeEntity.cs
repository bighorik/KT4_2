namespace Domain.Entities
{
    /// <summary>
    /// Код подтверждения почты
    /// </summary>
    public class AccountConfirmationCodeEntity
    {
        /// <summary>
        /// Идентефикатор кода
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Id пользователя, чью почту подтверждают
        /// </summary>
        public required Guid UserId { get; set; }

        /// <summary>
        /// Код подтверждения
        /// </summary>
        public required string Code { get; set; }

        /// <summary>
        /// Дата генерации кода
        /// </summary>
        public required DateTime CreatedAt { get; set; }

        /// <summary>
        /// Время истечения кода
        /// </summary>
        public required DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Когда код был использован
        /// </summary>
        public DateTime? ConsumedAt { get; set; }
    }
}
