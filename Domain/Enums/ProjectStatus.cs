namespace Domain.Enums
{
    /// <summary>
    /// Статусы проекта
    /// </summary>
    public enum ProjectStatus
    {
        /// <summary>
        /// Черновик
        /// </summary>
        Draft,

        /// <summary>
        /// В процессе набора команды
        /// </summary>
        Recruting,

        /// <summary>
        /// В процессе набора команды
        /// </summary>
        RecrutingCompleted,

        /// <summary>
        /// Набор завершен
        /// </summary>
        ProjectCompleted,

        /// <summary>
        /// Архив
        /// </summary>
        Archived,
    }
}
