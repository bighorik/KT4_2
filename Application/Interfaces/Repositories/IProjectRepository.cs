using Application.Utils;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task<RepositoryResult<ProjectEntity>> AddProjectAsync(ProjectEntity project);
        Task<RepositoryResult<ProjectEntity>> AddAttachmentToProjectAsync(ProjectEntity project, Guid AttachmentId);
    }
}
