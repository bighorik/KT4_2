using Application.Behaviour.Project;
using Application.Interfaces.Repositories.BaseRepositories;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;

namespace Application.Interfaces.Repositories
{
    public interface IProjectRepository :
        IExistsRepository,
        IGetByIdRepository<ProjectEntity>,
        IGetFullIncludedByIdRepository<ProjectEntity>,
        ICreateRepository<ProjectEntity>,
        IGetPaginatedList<ProjectEntity, GetProjectListQuery>,
        IGetPaginatedList<UserProjectModel, GetUserProjectsListQuery>
    {
        Task<int> GetUserProjectsCountAsync(Guid userId);
        Task<ProjectEntity> AddAttachmentToProjectAsync(Guid id, Guid attachmentId);
        Task<ProjectEntity> DeleteAttachmentFromProjectAsync(Guid id, Guid AattachmentId);
        Task<ProjectEntity> UpdateProjectStatusAsync(Guid id, ProjectStatus status);
        Task<ProjectEntity> UpdateProjectDescriptionAsync(Guid id, string description);
        Task<ProjectEntity> UpdateProjectNameAsync(Guid id, string name);
        Task<ProjectEntity> UpdateProjectAdditionalRecruitmentAllowAsync(Guid id, bool isAdditionalRecruitmentAllowed);
        Task<ProjectEntity> UpdateProjectStrictCandidatesSelectionAsync(Guid id, bool isStrictCandidatesSelection);
        Task<UserProjectModel?> GetDetailedById(Guid id);
    }
}
