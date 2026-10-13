using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Project
{
    public record UpdateProjectAdditionalRecruitmentAllowCommand : IRequest<ResponseContract<ProjectEntity>>
    {
        public required Guid ProjectId { get; set; }
        public required bool IsAdditionalRecruitmentAllowed { get; set; }
    }

    public class UpdateProjectAdditionalRecruitmentAllowCommandHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateProjectAdditionalRecruitmentAllowCommand, ResponseContract<ProjectEntity>>
    {
        public async Task<ResponseContract<ProjectEntity>> Handle(UpdateProjectAdditionalRecruitmentAllowCommand request, CancellationToken cancellationToken)
        {
            if (!await projectRepository.ExistsAsync(request.ProjectId))
                return new ResponseContract<ProjectEntity>(ErrorCodes.ProjectNotFound, request.ProjectId);

            ProjectEntity project = await projectRepository.UpdateProjectAdditionalRecruitmentAllowAsync(request.ProjectId, request.IsAdditionalRecruitmentAllowed);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract<ProjectEntity>(project);
        }
    }

    public class UpdateProjectAdditionalRecruitmentAllowCommandValidator : AbstractValidator<UpdateProjectAdditionalRecruitmentAllowCommand>
    {
        public UpdateProjectAdditionalRecruitmentAllowCommandValidator()
        {
            RuleFor(command => command.ProjectId).NotEmpty();
            RuleFor(command => command.IsAdditionalRecruitmentAllowed).NotEmpty();
        }
    }
}
