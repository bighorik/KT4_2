using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.ProjectPositionSkill
{
    public class UpdateProjectPositionSkillLevelCommand : IRequest<ResponseContract<PositionSkillEntity>>
    {
        public Guid PositionSkillId { get; set; }
        public SkillLevel SkillLevel { get; set; }
    }

    public class UpdateProjectPositionSkillLevelCommandHandler(
            IPositionSkillRepository positionSkillRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<UpdateProjectPositionSkillLevelCommand, ResponseContract<PositionSkillEntity>>
    {
        public async Task<ResponseContract<PositionSkillEntity>> Handle(UpdateProjectPositionSkillLevelCommand request, CancellationToken cancellationToken)
        {
            if (!await positionSkillRepository.ExistsAsync(request.PositionSkillId))
                return new ResponseContract<PositionSkillEntity>(ErrorCodes.ProjectPositionSkillNotFound);

            PositionSkillEntity positionSkill = await positionSkillRepository.UpdateSkillLevelAsync(request.PositionSkillId, request.SkillLevel);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract<PositionSkillEntity>(positionSkill);
        }
    }

    public class UpdateProjectPositionSkillLevelCommandValidator : AbstractValidator<UpdateProjectPositionSkillLevelCommand>
    {
        public UpdateProjectPositionSkillLevelCommandValidator()
        {
            RuleFor(command => command.PositionSkillId).NotEmpty();
            RuleFor(command => command.SkillLevel).NotEmpty();
        }
    }
}
