using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Project
{
    public record ChangeProjectStatusCommand : IRequest<ResponseContract<ProjectEntity>>
    {
        public required Guid ProjectId { get; set; }
        public required Guid OwnerId { get; set; }
        public required ProjectStatus ProjectStatus { get; set; }
    }

    public class ChangeProjectStatusCommandHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<ChangeProjectStatusCommand, ResponseContract<ProjectEntity>>
    {
        public async Task<ResponseContract<ProjectEntity>> Handle(ChangeProjectStatusCommand request, CancellationToken cancellationToken)
        {
            ProjectEntity? project = await projectRepository.GetByIdAsync(request.ProjectId);

            // Проверяем наличие проекта в базе
            if (project == null)
                return new ResponseContract<ProjectEntity>(ErrorCodes.ProjectNotFound);

            // Проверяем, что статус действительно требует изменений
            if (project.Status == request.ProjectStatus)
                return new ResponseContract<ProjectEntity>();

            // Проверяем, что пользователь является владельцем проекта
            if (!await projectRepository.IsProjectOwnerAsync(request.ProjectId, request.OwnerId))
                return new ResponseContract<ProjectEntity>(ErrorCodes.UserIsNotProjectOwner);

            // Пытаемся изменить статус 
            project.Status = request.ProjectStatus;

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract<ProjectEntity>(project);
        }
    }

    public class ChangeProjectStatusCommandValidator : AbstractValidator<ChangeProjectStatusCommand>
    {
        public ChangeProjectStatusCommandValidator()
        {
            RuleFor(command => command.ProjectId).NotEmpty();
            RuleFor(command => command.OwnerId).NotEmpty();
        }
    }
}
