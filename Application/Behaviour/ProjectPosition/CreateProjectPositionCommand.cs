using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;
using static Application.Behaviour.ProjectPosition.CreateProjectPositionCommand;

namespace Application.Behaviour.ProjectPosition
{
    public class CreateProjectPositionCommand : IRequest<ResponseContract<ProjectPositionEntity>>
    {
        public required string Name { get; set; }
        public string? Description { get; set; } = "";
        public List<CreatePositionSkill> PositionSkills { get; set; } = [];
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }

        public class CreatePositionSkill
        {
            public Guid SkillId { get; set; }
            public SkillLevel SkillLevel { get; set; }
        }
    }

    public class CreateProjectPositionCommandHandler(
            IProjectRepository projectRepository,
            IProjectPositionRepository positionRepository,
            ISkillRepository skillRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<CreateProjectPositionCommand, ResponseContract<ProjectPositionEntity>>
    {
        public async Task<ResponseContract<ProjectPositionEntity>> Handle(CreateProjectPositionCommand request, CancellationToken cancellationToken)
        {
            if (!await projectRepository.ExistsAsync(request.ProjectId))
                return new ResponseContract<ProjectPositionEntity>(ErrorCodes.ProjectNotFound);

            List<PositionSkillEntity> positionSkills = [];
            foreach (CreatePositionSkill positionSkill in request.PositionSkills)
            {
                if (!await skillRepository.ExistsAsync(positionSkill.SkillId))
                    return new ResponseContract<ProjectPositionEntity>(ErrorCodes.SkillNotFound);

                positionSkills.Add(new()
                {
                    Id = Guid.NewGuid(),
                    SkillId = positionSkill.SkillId,
                    SkillLevel = positionSkill.SkillLevel,
                });
            }

            ProjectPositionEntity position = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                ProjectId = request.ProjectId,
                PositionSkills = positionSkills,
            };

            await positionRepository.AddPositionAsync(position);

            await unitOfWork.SaveChangesAsync();

            return new ResponseContract<ProjectPositionEntity>(position);
        }
    }

    public class CreateProjectPositionCommandValidator : AbstractValidator<CreateProjectPositionCommand>
    {
        public CreateProjectPositionCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(command => command.ProjectId).NotEmpty();
            RuleFor(command => command.UserId).NotEmpty();
            RuleForEach(command => command.PositionSkills).ChildRules(rule =>
            {
                rule.RuleFor(position => position.SkillId).NotEmpty();
                rule.RuleFor(position => position.SkillLevel).NotEmpty();
            });
        }
    }
}
