using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Skill
{
    public class CreateSkillCommand : IRequest<ResponseContract<SkillEntity>>
    {
        public required string Name { get; set; }
    }

    public class CreateSkillCommandHandler(
        ISkillRepository skillRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateSkillCommand, ResponseContract<SkillEntity>>
    {
        public async Task<ResponseContract<SkillEntity>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            SkillEntity skill = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                IsArchived = false
            };

            await skillRepository.CreateAsync(skill);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract<SkillEntity>(skill);
        }
    }

    public class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
    {
        public CreateSkillCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
        }
    }
}
