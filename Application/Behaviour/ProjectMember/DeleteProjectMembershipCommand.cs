using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.ProjectMember
{
    public class DeleteProjectMembershipCommand : IRequest<ResponseContract>
    {
        public required Guid UserId { get; set; }

        public required Guid MemberId { get; set; }
    }

    public class DeleteProjectMembershipCommandHandler(
        IProjectMemberRepository memberRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<DeleteProjectMembershipCommand, ResponseContract>
    {
        public async Task<ResponseContract> Handle(DeleteProjectMembershipCommand request, CancellationToken cancellationToken)
        {
            ProjectMemberEntity? member = await memberRepository.GetFullIncludedByIdAsync(request.MemberId);

            if (member == null)
                return new ResponseContract(ErrorCodes.ProjectMemberNotFound);

            if (member.Project.OwnerId != request.UserId)
                return new ResponseContract(ErrorCodes.UserIsNotProjectOwner);

            await memberRepository.DeleteAsync(member);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract();
        }
    }

    public class DeleteProjectMembershipCommandValidator : AbstractValidator<DeleteProjectMembershipCommand>
    {
        public DeleteProjectMembershipCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.MemberId).NotEmpty();
        }
    }
}
