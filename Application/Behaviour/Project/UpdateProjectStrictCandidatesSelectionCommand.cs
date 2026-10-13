using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Project
{
    public record UpdateProjectStrictCandidatesSelectionCommand : IRequest<ResponseContract<ProjectEntity>>
    {
        public required Guid ProjectId { get; set; }
        public required bool IsStrictCandidatesSelection { get; set; }
    }

    public class UpdateProjectStrictCandidatesSelectionCommandHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateProjectStrictCandidatesSelectionCommand, ResponseContract<ProjectEntity>>
    {
        public async Task<ResponseContract<ProjectEntity>> Handle(UpdateProjectStrictCandidatesSelectionCommand request, CancellationToken cancellationToken)
        {
            if (!await projectRepository.ExistsAsync(request.ProjectId))
                return new ResponseContract<ProjectEntity>(ErrorCodes.ProjectNotFound, request.ProjectId);

            ProjectEntity project = await projectRepository.UpdateProjectStrictCandidatesSelectionAsync(request.ProjectId, request.IsStrictCandidatesSelection);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract<ProjectEntity>(project);
        }
    }

    public class UpdateProjectStrictCandidatesSelectionCommandValidator : AbstractValidator<UpdateProjectStrictCandidatesSelectionCommand>
    {
        public UpdateProjectStrictCandidatesSelectionCommandValidator()
        {
            RuleFor(command => command.ProjectId).NotEmpty();
            RuleFor(command => command.IsStrictCandidatesSelection).NotEmpty();
        }
    }
}
