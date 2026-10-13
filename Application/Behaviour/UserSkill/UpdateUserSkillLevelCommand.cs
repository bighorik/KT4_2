using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.UserSkill
{
    public class UpdateUserSkillLevelCommand : IRequest<ResponseContract<UserSkillEntity>>
    {
        public Guid UserSkillId { get; set; }
        public SkillLevel SkillLevel { get; set; }
    }

    public class UpdateUserSkillLevelCommandHandler(
            IUserSkillRepository userSkillRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<UpdateUserSkillLevelCommand, ResponseContract<UserSkillEntity>>
    {
        public async Task<ResponseContract<UserSkillEntity>> Handle(UpdateUserSkillLevelCommand request, CancellationToken cancellationToken)
        {
            if (!await userSkillRepository.ExistsAsync(request.UserSkillId))
                return new ResponseContract<UserSkillEntity>(ErrorCodes.ProjectPositionSkillNotFound);

            UserSkillEntity userSkill = await userSkillRepository.UpdateSkillLevelAsync(request.UserSkillId, request.SkillLevel);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract<UserSkillEntity>(userSkill);
        }
    }

    public class UpdateUserSkillLevelCommanddValidator : AbstractValidator<UpdateUserSkillLevelCommand>
    {
        public UpdateUserSkillLevelCommanddValidator()
        {
            RuleFor(command => command.UserSkillId).NotEmpty();
            RuleFor(command => command.SkillLevel).NotEmpty();
        }
    }
}
