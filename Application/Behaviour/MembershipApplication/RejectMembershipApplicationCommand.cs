using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.MembershipApplication
{
    public class RejectMembershipApplicationCommand : IRequest<ResponseContract<MembershipApplicationEntity>>
    {
        public Guid UserId { get; set; }
        public Guid ApplicationId { get; set; }
    }

    public class RejectMembershipApplicationCommandHandler(
        IMembershipApplicationRepository applicationRepository,
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<RejectMembershipApplicationCommand, ResponseContract<MembershipApplicationEntity>>
    {
        public async Task<ResponseContract<MembershipApplicationEntity>> Handle(RejectMembershipApplicationCommand request, CancellationToken cancellationToken)
        {
            MembershipApplicationEntity application = await applicationRepository.GetByIdAsync(request.ApplicationId);

            if (application == null)
                return new ResponseContract<MembershipApplicationEntity>(ErrorCodes.MembershipApplicationNotFound);

            ProjectEntity project = await projectRepository.GetByIdAsync(application.ProjectId);

            if (project == null)
                return new ResponseContract<MembershipApplicationEntity>(ErrorCodes.ProjectNotFound);

            if (project.OwnerId != request.UserId)
                return new ResponseContract<MembershipApplicationEntity>(ErrorCodes.UserIsNotProjectOwner);


            application.Status = MembershipApplicationStatus.Rejected;
            await unitOfWork.SaveChangesAsync();

            return new ResponseContract<MembershipApplicationEntity>();
        }
    }

    public class RejectMembershipApplicationCommandValidator : AbstractValidator<RejectMembershipApplicationCommand>
    {
        public RejectMembershipApplicationCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.ApplicationId).NotEmpty();
        }
    }
}
