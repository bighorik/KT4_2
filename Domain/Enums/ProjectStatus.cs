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
        Recruiting,

        /// <summary>
        /// В процессе набора команды
        /// </summary>
        RecruitingCompleted,

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
