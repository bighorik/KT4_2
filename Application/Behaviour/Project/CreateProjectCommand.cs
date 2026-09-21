using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Project
{
    public record CreateProjectCommand : IRequest<ResponseContract<ProjectEntity>>
    {
        public required string Name { get; set; }
        public required Guid OwnerId { get; set; }
    }

    public class CreateProjectCommandHandler(
        IProjectRepository projectRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateProjectCommand, ResponseContract<ProjectEntity>>
    {
        public async Task<ResponseContract<ProjectEntity>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            // Проверяем существование пользователя 
            if (!await userRepository.ExistsAsync(request.OwnerId))
                return new ResponseContract<ProjectEntity>(ErrorCodes.UserNotFound, request.OwnerId);

            ProjectEntity project = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = "",
                OwnerId = request.OwnerId,
                Attachments = [],
                InternetResourceLinks = [],
                RequiredPositions = [],
                IsAdditionalRecruitmentAllowed = false,
                IsStrictCandidatesSelection = false,
                Status = ProjectStatus.Draft,
            };
            await projectRepository.AddProjectAsync(project);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract<ProjectEntity>(project);
        }
    }

    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(command => command.OwnerId).NotEmpty();
            RuleFor(command => command.Name).NotEmpty().MaximumLength(150);
        }
    }
}
