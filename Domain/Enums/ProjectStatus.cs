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
        /// Набор завершен
        /// </summary>
        Completed,

        /// <summary>
        /// Архив
        /// </summary>
        Archived,
    }
}
