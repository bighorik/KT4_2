using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Project
{
    public record UpdateProjectStatusCommand : IRequest<ResponseContract<ProjectEntity>>
    {
        public required Guid ProjectId { get; set; }
        public required ProjectStatus ProjectStatus { get; set; }
    }

    public class ChangeProjectStatusCommandHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateProjectStatusCommand, ResponseContract<ProjectEntity>>
    {
        public async Task<ResponseContract<ProjectEntity>> Handle(UpdateProjectStatusCommand request, CancellationToken cancellationToken)
        {
            if (!await projectRepository.ExistsAsync(request.ProjectId))
                return new ResponseContract<ProjectEntity>(ErrorCodes.ProjectNotFound);

            ProjectEntity project = await projectRepository.UpdateProjectStatusAsync(request.ProjectId, request.ProjectStatus);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract<ProjectEntity>(project);
        }
    }

    public class ChangeProjectStatusCommandValidator : AbstractValidator<UpdateProjectStatusCommand>
    {
        public ChangeProjectStatusCommandValidator()
        {
            RuleFor(command => command.ProjectId).NotEmpty();
            RuleFor(command => command.ProjectStatus).NotEmpty();
        }
    }
}
