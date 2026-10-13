using Application.Interfaces.Repositories;
using Application.Utils;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.ProjectPositionSkill
{
    public class DeleteProjectPositionSkillCommand : IRequest<ResponseContract>
    {
        public Guid PositionSkillId { get; set; }
    }

    public class DeleteProjectPositionSkillCommandHandler(
            IPositionSkillRepository positionSkillRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<DeleteProjectPositionSkillCommand, ResponseContract>
    {
        public async Task<ResponseContract> Handle(DeleteProjectPositionSkillCommand request, CancellationToken cancellationToken)
        {
            await positionSkillRepository.DeleteByIdAsync(request.PositionSkillId);
            await unitOfWork.SaveChangesAsync();

            return new ResponseContract();
        }
    }

    public class DeleteProjectPositionSkillCommandValidator : AbstractValidator<DeleteProjectPositionSkillCommand>
    {
        public DeleteProjectPositionSkillCommandValidator()
        {
            RuleFor(command => command.PositionSkillId).NotEmpty();
        }
    }
}
