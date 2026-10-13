using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.UserSkill
{
    public class CreateUserSkillCommand : IRequest<ResponseContract<UserSkillEntity>>
    {
        public Guid UserId { get; set; }
    }

    public class CreateUserSkillCommandHandler(
            IUserSkillRepository userSkillRepository,
            ISkillRepository skillRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<CreateUserSkillCommand, ResponseContract<UserSkillEntity>>
    {
        public async Task<ResponseContract<UserSkillEntity>> Handle(CreateUserSkillCommand request, CancellationToken cancellationToken)
        {
            SkillEntity? availableSkill = await skillRepository.GetNotAddedAsync(request.UserId);

            if (availableSkill == null)
                return new ResponseContract<UserSkillEntity>(ErrorCodes.NoModeAvailableSkills);

            UserSkillEntity skill = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                SkillId = availableSkill.Id,
                Skill = availableSkill,
                SkillLevel = SkillLevel.None,
            };

            await userSkillRepository.CreateAsync(skill);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract<UserSkillEntity>(skill);
        }
    }

    public class CreateUserSkillCommandValidator : AbstractValidator<CreateUserSkillCommand>
    {
        public CreateUserSkillCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
        }
    }
}
