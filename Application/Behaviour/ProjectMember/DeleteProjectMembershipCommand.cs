using Application.Interfaces.Repositories;
using Application.Utils;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.ProjectMember
{
    public class DeleteProjectMembershipCommand : IRequest<ResponseContract>
    {
        public required Guid MemberId { get; set; }
    }

    public class DeleteProjectMembershipCommandHandler(
        IProjectMemberRepository memberRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<DeleteProjectMembershipCommand, ResponseContract>
    {
        public async Task<ResponseContract> Handle(DeleteProjectMembershipCommand request, CancellationToken cancellationToken)
        {
            if (!await memberRepository.ExistsAsync(request.MemberId))
                return new ResponseContract(ErrorCodes.ProjectMemberNotFound);

            await memberRepository.DeleteByIdAsync(request.MemberId);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract();
        }
    }

    public class DeleteProjectMembershipCommandValidator : AbstractValidator<DeleteProjectMembershipCommand>
    {
        public DeleteProjectMembershipCommandValidator()
        {
            RuleFor(command => command.MemberId).NotEmpty();
        }
    }
}
