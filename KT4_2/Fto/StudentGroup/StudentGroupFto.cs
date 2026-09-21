namespace WebApi.Fto.StudentGroup
{
    /// <summary>
    /// Справочный номер группы студента
    /// </summary>
    public class StudentGroupFto
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
