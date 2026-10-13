using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.MembershipApplication
{
    public class RejectMembershipApplicationCommand : IRequest<ResponseContract>
    {
        public Guid ApplicationId { get; set; }
    }

    public class RejectMembershipApplicationCommandHandler(
        IMembershipApplicationRepository applicationRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<RejectMembershipApplicationCommand, ResponseContract>
    {
        public async Task<ResponseContract> Handle(RejectMembershipApplicationCommand request, CancellationToken cancellationToken)
        {
            if (!await applicationRepository.ExistsAsync(request.ApplicationId))
                return new ResponseContract(ErrorCodes.MembershipApplicationNotFound);

            await applicationRepository.UpdateMembershipStatusAsync(request.ApplicationId, MembershipApplicationStatus.Rejected);
            await unitOfWork.SaveChangesAsync();

            return new ResponseContract();
        }
    }

    public class RejectMembershipApplicationCommandValidator : AbstractValidator<RejectMembershipApplicationCommand>
    {
        public RejectMembershipApplicationCommandValidator()
        {
            RuleFor(command => command.ApplicationId).NotEmpty();
        }
    }
}
