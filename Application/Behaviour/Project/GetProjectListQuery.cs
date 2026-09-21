using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behaviour.Project
{
    public class GetProjectListQuery : PaginationQuery, IRequest<PaginationContract<ProjectEntity>>
    {
        public ProjectStatus? Status { get; set; }
        public bool? IncludeReqrutingCompletedProjects { get; set; } = false;
        public List<Guid> SkillIds { get; set; } = [];
        public Guid? UserId { get; set; }
        public bool? FilterByIserSkills { get; set; }
        public bool? RequireSkillLevelMatch { get; set; }
        public bool? OnlyOwnersProjects { get; set; }
    }

    public class GetProjectListQueryHandler(
        IProjectRepository projectRepository) : IRequestHandler<GetProjectListQuery, PaginationContract<ProjectEntity>>
    {
        Task<PaginationContract<ProjectEntity>> IRequestHandler<GetProjectListQuery, PaginationContract<ProjectEntity>>.Handle(GetProjectListQuery request, CancellationToken cancellationToken)
        {
            return projectRepository.GetListAsync(request);
        }
    }

    public class GetProjectListQueryValidator : BasePaginationValidator<GetProjectListQuery>
    {

    }
}
