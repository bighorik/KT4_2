using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Project
{
    public record UpdateProjectDescriptionCommand : IRequest<ResponseContract<ProjectEntity>>
    {
        public required Guid ProjectId { get; set; }
        public required string Description { get; set; }
    }

    public class UpdateProjectDescriptionCommandHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateProjectDescriptionCommand, ResponseContract<ProjectEntity>>
    {
        public async Task<ResponseContract<ProjectEntity>> Handle(UpdateProjectDescriptionCommand request, CancellationToken cancellationToken)
        {
            if (!await projectRepository.ExistsAsync(request.ProjectId))
                return new ResponseContract<ProjectEntity>(ErrorCodes.ProjectNotFound, request.ProjectId);

            ProjectEntity project = await projectRepository.UpdateProjectDescriptionAsync(request.ProjectId, request.Description);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract<ProjectEntity>(project);
        }
    }

    public class UpdateProjectDescriptionCommandValidator : AbstractValidator<UpdateProjectDescriptionCommand>
    {
        public UpdateProjectDescriptionCommandValidator()
        {
            RuleFor(command => command.ProjectId).NotEmpty();
            RuleFor(command => command.Description).NotEmpty().MaximumLength(500);
        }
    }
}
