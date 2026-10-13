using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Project
{
    public record GetDetailedProjectQuery : IRequest<ResponseContract<UserProjectModel>>
    {
        public required Guid ProjectId { get; set; }
    }

    public class GetDetailedProjectQueryHandler(
        IProjectRepository projectRepository) : IRequestHandler<GetDetailedProjectQuery, ResponseContract<UserProjectModel>>
    {
        public async Task<ResponseContract<UserProjectModel>> Handle(GetDetailedProjectQuery request, CancellationToken cancellationToken)
        {
            UserProjectModel? project = await projectRepository.GetDetailedById(request.ProjectId);

            if (project == null)
                return new ResponseContract<UserProjectModel>(ErrorCodes.ProjectNotFound);

            return new ResponseContract<UserProjectModel>(project);
        }
    }

    public class GetDetailedProjectQueryValidator : AbstractValidator<GetDetailedProjectQuery>
    {
        public GetDetailedProjectQueryValidator()
        {
            RuleFor(command => command.ProjectId).NotEmpty();
        }
    }
}
