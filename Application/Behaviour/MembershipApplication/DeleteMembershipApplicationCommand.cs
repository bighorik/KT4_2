using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.MembershipApplication
{
    public class DeleteMembershipApplicationCommand : IRequest<ResponseContract>
    {
        public Guid UserId { get; set; }

        public Guid ApplicationId { get; set; }
    }

    public class DeleteMembershipApplicationCommandHandler(
        IMembershipApplicationRepository applicationRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<DeleteMembershipApplicationCommand, ResponseContract>
    {
        public async Task<ResponseContract> Handle(DeleteMembershipApplicationCommand request, CancellationToken cancellationToken)
        {
            MembershipApplicationEntity application = await applicationRepository.GetByIdAsync(request.ApplicationId);

            if (application == null)
                return new ResponseContract(ErrorCodes.MembershipApplicationNotFound);

            if (application.UserId != request.UserId)
                return new ResponseContract(ErrorCodes.UserIsNotOwnerMembershipApplication);

            application.Status = MembershipApplicationStatus.Archive;
            await unitOfWork.SaveChangesAsync();

            return new ResponseContract();
        }
    }

    public class DeleteMembershipApplicationCommandValidator : AbstractValidator<DeleteMembershipApplicationCommand>
    {
        public DeleteMembershipApplicationCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.ApplicationId).NotEmpty();
        }
    }
}
