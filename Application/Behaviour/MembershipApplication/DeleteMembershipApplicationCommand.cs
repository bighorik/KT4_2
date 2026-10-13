using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.MembershipApplication
{
    public class DeleteMembershipApplicationCommand : IRequest<ResponseContract>
    {
        public Guid ApplicationId { get; set; }
    }

    public class DeleteMembershipApplicationCommandHandler(
        IMembershipApplicationRepository applicationRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<DeleteMembershipApplicationCommand, ResponseContract>
    {
        public async Task<ResponseContract> Handle(DeleteMembershipApplicationCommand request, CancellationToken cancellationToken)
        {
            if (!await applicationRepository.ExistsAsync(request.ApplicationId))
                return new ResponseContract(ErrorCodes.MembershipApplicationNotFound);

            await applicationRepository.UpdateMembershipStatusAsync(request.ApplicationId, MembershipApplicationStatus.Archive);
            await unitOfWork.SaveChangesAsync();

            return new ResponseContract();
        }
    }

    public class DeleteMembershipApplicationCommandValidator : AbstractValidator<DeleteMembershipApplicationCommand>
    {
        public DeleteMembershipApplicationCommandValidator()
        {
            RuleFor(command => command.ApplicationId).NotEmpty();
        }
    }
}
