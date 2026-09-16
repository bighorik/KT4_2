using Domain.Entities;
using MediatR;

namespace Application.Behaviour.Project
{
    public class CreateProjectCommand : IRequest
    {
        /// <summary>
        /// Название проекта
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Описание проекта
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Идентефикатор владельца проекта
        /// </summary>
        public required Guid OwnerId { get; set; }

        /// <summary>
        /// Требуемые вакансии
        /// </summary>
        public List<ProjectPosition> RequiredPositions = [];

        /// <summary>
        /// Ссылки на внешние ресурсы проекта
        /// </summary>
        public List<Guid> InternetResourceLinkIds = [];

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
