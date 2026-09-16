namespace Domain.Entities
{
    /// <summary>
    /// Проект
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Идентефикатор проекта
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Название проекта
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Описание проекта
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Идентефикатор владельца проекта
        /// </summary>
        public required Guid OwnerId { get; set; }

        /// <summary>
        /// Владелец проекта
        /// </summary>
        public required User Owner { get; set; }
        
        /// <summary>
        /// Список участников проекта
        /// </summary>
        public List<ProjectMember> ProjectMembers { get; set; } = [];

        /// <summary>
        /// Требуемые вакансии
        /// </summary>
        public List<ProjectPosition> RequiredPositions = [];

        /// <summary>
        /// Ссылки на внешние ресурсы проекта
        /// </summary>
        public List<InternetResourceLink> InternetResourceLinks = [];

        /// <summary>
        /// Вложения проекта
        /// </summary>
        public List<string> Attachments = [];

        /// <summary>
        /// Можно ли оставить заявку в проект даже тогда, когда набор закрыт?
        /// </summary>
        public required bool IsAdditionalRecruitmentAllowed = false;

        /// <summary>
        /// Можно ли оставлять заявку в проект людям, у которых не полное совпадение по наывкам
        /// </summary>
        public required bool IsStrictCandidatesSelection = false;
    }
}
