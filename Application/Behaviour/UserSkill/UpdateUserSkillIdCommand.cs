using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.UserSkill
{
    public class UpdateUserSkillIdCommand : IRequest<ResponseContract<UserSkillEntity>>
    {
        public required Guid UserId { get; set; }
        public required Guid UserSkillId { get; set; }
        public required Guid SkillId { get; set; }
    }

    public class UpdateUserSkillIdCommandHandler(
            IUserSkillRepository userSkillRepository,
            ISkillRepository skillRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<UpdateUserSkillIdCommand, ResponseContract<UserSkillEntity>>
    {
        public async Task<ResponseContract<UserSkillEntity>> Handle(UpdateUserSkillIdCommand request, CancellationToken cancellationToken)
        {
            if (!await userSkillRepository.ExistsAsync(request.UserSkillId))
                return new ResponseContract<UserSkillEntity>(ErrorCodes.UserSkillNotFound);

            if (!await skillRepository.ExistsAsync(request.SkillId))
                return new ResponseContract<UserSkillEntity>(ErrorCodes.SkillNotFound);

            if (await userSkillRepository.HasSkillAsync(request.UserId, request.SkillId))
                return new ResponseContract<UserSkillEntity>(ErrorCodes.SkillAlreadyAdded);

            UserSkillEntity userSkill = await userSkillRepository.UpdateSkillAsync(request.UserSkillId, request.SkillId);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract<UserSkillEntity>(userSkill);
        }
    }

    public class UpdateUserSkillIdCommanddValidator : AbstractValidator<UpdateUserSkillIdCommand>
    {
        public UpdateUserSkillIdCommanddValidator()
        {
            RuleFor(command => command.UserSkillId).NotEmpty();
            RuleFor(command => command.SkillId).NotEmpty();
            RuleFor(command => command.UserId).NotEmpty();
        }
    }
}
