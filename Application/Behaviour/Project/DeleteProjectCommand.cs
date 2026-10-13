using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Project
{
    public record DeleteProjectCommand : IRequest<ResponseContract>
    {
        public required Guid ProjectId { get; set; }
    }

    public class DeleteProjectCommandHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteProjectCommand, ResponseContract>
    {
        public async Task<ResponseContract> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            if (!await projectRepository.ExistsAsync(request.ProjectId))
                return new ResponseContract(ErrorCodes.ProjectNotFound);

            await projectRepository.UpdateProjectStatusAsync(request.ProjectId, ProjectStatus.Archived);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract();
        }
    }

    public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
    {
        public DeleteProjectCommandValidator()
        {
            RuleFor(command => command.ProjectId).NotEmpty();
        }
    }
}
