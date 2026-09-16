namespace Domain.Entities
{
    /// <summary>
    /// Справочный номер группы студента
    /// </summary>
    public class StudentGroupNumber
    {
        /// <summary>
        /// Идентификатор номера группы
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Название номера группы
        /// </summary>
        public required string Name { get; set; }
    }
}
