using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.ProjectPosition
{
    public class CreateProjectPositionCommand : IRequest<ResponseContract<ProjectPositionEntity>>
    {
        public Guid ProjectId { get; set; }
    }

    public class CreateProjectPositionCommandHandler(
            IProjectRepository projectRepository,
            IProjectPositionRepository positionRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<CreateProjectPositionCommand, ResponseContract<ProjectPositionEntity>>
    {
        public async Task<ResponseContract<ProjectPositionEntity>> Handle(CreateProjectPositionCommand request, CancellationToken cancellationToken)
        {
            if (!await projectRepository.ExistsAsync(request.ProjectId))
                return new ResponseContract<ProjectPositionEntity>(ErrorCodes.ProjectNotFound);

            ProjectPositionEntity position = new()
            {
                Id = Guid.NewGuid(),
                Name = "Название вакансии",
                Description = "",
                ProjectId = request.ProjectId,
                PositionSkills = [],
            };

            await positionRepository.CreateAsync(position);

            await unitOfWork.SaveChangesAsync();

            return new ResponseContract<ProjectPositionEntity>(position);
        }
    }

    public class CreateProjectPositionCommandValidator : AbstractValidator<CreateProjectPositionCommand>
    {
        public CreateProjectPositionCommandValidator()
        {
            RuleFor(command => command.ProjectId).NotEmpty();
        }
    }
}
