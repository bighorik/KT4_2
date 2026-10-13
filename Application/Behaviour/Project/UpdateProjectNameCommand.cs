using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Project
{
    public record UpdateProjectNameCommand : IRequest<ResponseContract<ProjectEntity>>
    {
        public required Guid ProjectId { get; set; }
        public required string Name { get; set; }
    }

    public class UpdateProjectNameCommandHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateProjectNameCommand, ResponseContract<ProjectEntity>>
    {
        public async Task<ResponseContract<ProjectEntity>> Handle(UpdateProjectNameCommand request, CancellationToken cancellationToken)
        {
            if (!await projectRepository.ExistsAsync(request.ProjectId))
                return new ResponseContract<ProjectEntity>(ErrorCodes.ProjectNotFound, request.ProjectId);

            ProjectEntity project = await projectRepository.UpdateProjectNameAsync(request.ProjectId, request.Name);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract<ProjectEntity>(project);
        }
    }

    public class UpdateProjectNameCommandValidator : AbstractValidator<UpdateProjectNameCommand>
    {
        public UpdateProjectNameCommandValidator()
        {
            RuleFor(command => command.ProjectId).NotEmpty();
            RuleFor(command => command.Name).NotEmpty().MinimumLength(3).MaximumLength(150);
        }
    }
}
