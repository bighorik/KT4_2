using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Skill
{
    public class UpdateSkillNameCommand : IRequest<ResponseContract<SkillEntity>>
    {
        public required Guid SkillId { get; set; }
        public required string Name { get; set; }
    }

    public class UpdateSkillNameCommandHandler(
        ISkillRepository skillRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateSkillNameCommand, ResponseContract<SkillEntity>>
    {
        public async Task<ResponseContract<SkillEntity>> Handle(UpdateSkillNameCommand request, CancellationToken cancellationToken)
        {
            if (!await skillRepository.ExistsAsync(request.SkillId))
                return new ResponseContract<SkillEntity>(ErrorCodes.SkillNotFound);

            SkillEntity skill = await skillRepository.UpdateSkillNameAsync(request.SkillId, request.Name);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract<SkillEntity>(skill);
        }
    }

    public class UpdateSkillNameCommandValidator : AbstractValidator<UpdateSkillNameCommand>
    {
        public UpdateSkillNameCommandValidator()
        {
            RuleFor(command => command.SkillId).NotEmpty();
            RuleFor(command => command.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
        }
    }
}
