using Application.Behaviour.Project;
using Application.Interfaces.Repositories.BaseRepositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Interfaces.Repositories
{
    public interface IProjectRepository : IExistsRepository, IGetByIdRepository<ProjectEntity>
    {
        /// <summary>
        /// Возвращает проект по ID.
        /// </summary>
        /// <returns>
        /// <see cref="Task{RepositoryResult{ProjectEntity}}"/>.
        /// Если проект не найден, возвращает статус код 
        /// <see cref="RepositoryResult.NotFound"/>.
        /// </returns>
        Task<PaginationContract<ProjectEntity>> GetListAsync(GetProjectListQuery query);
        Task<RepositoryResult<ProjectEntity>> AddProjectAsync(ProjectEntity project);
        Task<RepositoryResult<ProjectEntity>> AddAttachmentToProjectAsync(ProjectEntity project, Guid AttachmentId);
        Task<RepositoryResult<Unit>> ChangeProjectStatus(Guid projectId, ProjectStatus status);
        Task<bool> IsProjectOwnerAsync(Guid projectId, Guid ownerId);
    }
}
