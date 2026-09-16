namespace Domain.Entities
{
    /// <summary>
    /// Возможный навык - справочное значение
    /// </summary>
    public class Skill
    {
        /// <summary>
        /// Идентификатор навыка
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Название навыка
        /// </summary>
        public required string Name { get; set; }
    }
}
