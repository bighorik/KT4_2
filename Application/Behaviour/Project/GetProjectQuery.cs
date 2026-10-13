using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Project
{
    public record GetProjectQuery : IRequest<ResponseContract<ProjectEntity>>
    {
        public required Guid ProjectId { get; set; }
    }

    public class GetProjectQueryHandler(
        IProjectRepository projectRepository) : IRequestHandler<GetProjectQuery, ResponseContract<ProjectEntity>>
    {
        public async Task<ResponseContract<ProjectEntity>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            ProjectEntity? project = await projectRepository.GetFullIncludedByIdAsync(request.ProjectId);

            if (project == null)
                return new ResponseContract<ProjectEntity>(ErrorCodes.ProjectNotFound);

            return new ResponseContract<ProjectEntity>(project);
        }
    }

    public class GetProjectQueryValidator : AbstractValidator<GetProjectQuery>
    {
        public GetProjectQueryValidator()
        {
            RuleFor(command => command.ProjectId).NotEmpty();
        }
    }
}
