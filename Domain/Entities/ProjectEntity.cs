using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Проект
    /// </summary>
    public class ProjectEntity
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
        public UserEntity Owner { get; set; }

        /// <summary>
        /// Список участников проекта
        /// </summary>
        public List<ProjectMemberEntity> ProjectMembers { get; set; } = [];

        /// <summary>
        /// Требуемые вакансии
        /// </summary>
        public List<ProjectPositionEntity> RequiredPositions { get; set; } = [];

        /// <summary>
        /// Ссылки на внешние ресурсы проекта
        /// </summary>
        public List<InternetResourceLinkEntity> InternetResourceLinks { get; set; } = [];

        public List<MembershipApplicationEntity> MembershipApplications { get; set; } = [];

        /// <summary>
        /// Вложения проекта
        /// </summary>
        public List<AttachmentEntity> Attachments { get; set; } = [];

        /// <summary>
        /// Текущий статус проекта
        /// </summary>
        public required ProjectStatus Status { get; set; }

        /// <summary>
        /// Можно ли оставить заявку в проект даже тогда, когда набор закрыт?
        /// </summary>
        public required bool IsAdditionalRecruitmentAllowed { get; set; } = false;

        /// <summary>
        /// Можно ли оставлять заявку в проект людям, у которых не полное совпадение по наывкам
        /// </summary>
        public required bool IsStrictCandidatesSelection { get; set; } = true;
    }
}
