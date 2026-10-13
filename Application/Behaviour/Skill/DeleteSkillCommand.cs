using Application.Interfaces.Repositories;
using Application.Utils;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.ProjectPosition
{
    public class DeleteSkillCommand : IRequest<ResponseContract>
    {
        public required Guid SkillId { get; set; }
    }

    public class DeleteSkillCommandHandler(
            ISkillRepository skillRepository,
            IUnitOfWork unitOfWork
        ) : IRequestHandler<DeleteSkillCommand, ResponseContract>
    {
        public async Task<ResponseContract> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
        {
            await skillRepository.DeleteByIdAsync(request.SkillId);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract();
        }
    }

    public class DeleteSkillCommandValidator : AbstractValidator<DeleteSkillCommand>
    {
        public DeleteSkillCommandValidator()
        {
            RuleFor(command => command.SkillId).NotEmpty();
        }
    }
}
