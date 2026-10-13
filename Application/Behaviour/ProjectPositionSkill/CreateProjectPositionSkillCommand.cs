using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.ProjectPositionSkill
{
    public class CreateProjectPositionSkillCommand : IRequest<ResponseContract<PositionSkillEntity>>
    {
        public Guid SkillId { get; set; }
        public Guid ProjectPositionId { get; set; }
    }

    public class CreateProjectPositionSkillCommandHandler(
            IProjectPositionRepository positionRepository,
            ISkillRepository skillRepository,
            IPositionSkillRepository positionSkillRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<CreateProjectPositionSkillCommand, ResponseContract<PositionSkillEntity>>
    {
        public async Task<ResponseContract<PositionSkillEntity>> Handle(CreateProjectPositionSkillCommand request, CancellationToken cancellationToken)
        {
            if (!await positionRepository.ExistsAsync(request.ProjectPositionId))
                return new ResponseContract<PositionSkillEntity>(ErrorCodes.ProjectPositionNotFound);

            if (!await skillRepository.ExistsAsync(request.SkillId))
                return new ResponseContract<PositionSkillEntity>(ErrorCodes.SkillNotFound);

            if (!await positionSkillRepository.AlreadyAddedAsync(request.ProjectPositionId, request.SkillId))
                return new ResponseContract<PositionSkillEntity>(ErrorCodes.SkillAlreadyAdded);

            PositionSkillEntity skill = new()
            {
                Id = Guid.NewGuid(),
                ProjectPositionId = request.ProjectPositionId,
                SkillId = request.SkillId,
                SkillLevel = SkillLevel.None,
            };

            await positionSkillRepository.CreateAsync(skill);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract<PositionSkillEntity>(skill);
        }
    }

    public class CreateProjectPositionSkillCommandValidator : AbstractValidator<CreateProjectPositionSkillCommand>
    {
        public CreateProjectPositionSkillCommandValidator()
        {
            RuleFor(command => command.ProjectPositionId).NotEmpty();
            RuleFor(position => position.SkillId).NotEmpty();
        }
    }
}
