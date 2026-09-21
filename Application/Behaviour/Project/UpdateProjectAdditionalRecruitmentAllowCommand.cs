using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Project
{
    public record UpdateProjectAdditionalRecruitmentAllowCommand : IRequest<ResponseContract<ProjectEntity>>
    {
        public required Guid OwnerId { get; set; }
        public required Guid ProjectId { get; set; }
        public required bool IsAdditionalRecruitmentAllowed { get; set; }
    }

    public class UpdateProjectAdditionalRecruitmentAllowCommandHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateProjectAdditionalRecruitmentAllowCommand, ResponseContract<ProjectEntity>>
    {
        public async Task<ResponseContract<ProjectEntity>> Handle(UpdateProjectAdditionalRecruitmentAllowCommand request, CancellationToken cancellationToken)
        {
            ProjectEntity? project = await projectRepository.GetByIdAsync(request.ProjectId);

            // Проверяем существование проекта 
            if (project == null)
                return new ResponseContract<ProjectEntity>(ErrorCodes.ProjectNotFound, request.ProjectId);

            // Проверяем принадлежность проекта
            if (project.OwnerId != request.OwnerId)
                return new ResponseContract<ProjectEntity>(ErrorCodes.UserIsNotProjectOwner, request.OwnerId);

            project.IsAdditionalRecruitmentAllowed = request.IsAdditionalRecruitmentAllowed;

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract<ProjectEntity>(project);
        }
    }

    public class UpdateProjectAdditionalRecruitmentAllowCommandValidator : AbstractValidator<UpdateProjectAdditionalRecruitmentAllowCommand>
    {
        public UpdateProjectAdditionalRecruitmentAllowCommandValidator()
        {
            RuleFor(command => command.ProjectId).NotEmpty();
            RuleFor(command => command.OwnerId).NotEmpty();
            RuleFor(command => command.IsAdditionalRecruitmentAllowed).NotEmpty();
        }
    }
}
