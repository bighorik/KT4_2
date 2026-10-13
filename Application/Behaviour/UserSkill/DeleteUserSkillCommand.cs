using Application.Interfaces.Repositories;
using Application.Utils;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.UserSkill
{
    public class DeleteUserSkillCommand : IRequest<ResponseContract>
    {
        public Guid UserSkillId { get; set; }
    }

    public class DeleteUserSkillCommandHandler(
            IUserSkillRepository userSkillRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<DeleteUserSkillCommand, ResponseContract>
    {
        public async Task<ResponseContract> Handle(DeleteUserSkillCommand request, CancellationToken cancellationToken)
        {
            await userSkillRepository.DeleteByIdAsync(request.UserSkillId);
            await unitOfWork.SaveChangesAsync();

            return new ResponseContract();
        }
    }

    public class DeleteUserSkillCommandValidator : AbstractValidator<DeleteUserSkillCommand>
    {
        public DeleteUserSkillCommandValidator()
        {
            RuleFor(command => command.UserSkillId).NotEmpty();
        }
    }
}
