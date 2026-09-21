using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.MembershipApplication
{
    public class ApplyMembershipApplicationCommand : IRequest<ResponseContract<MembershipApplicationEntity>>
    {
        public Guid UserId { get; set; }
        public Guid ApplicationId { get; set; }
    }

    public class ApplyMembershipApplicationCommandHandler(
        IMembershipApplicationRepository applicationRepository,
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<ApplyMembershipApplicationCommand, ResponseContract<MembershipApplicationEntity>>
    {
        public async Task<ResponseContract<MembershipApplicationEntity>> Handle(ApplyMembershipApplicationCommand request, CancellationToken cancellationToken)
        {
            MembershipApplicationEntity application = await applicationRepository.GetByIdAsync(request.ApplicationId);

            if (application == null)
                return new ResponseContract<MembershipApplicationEntity>(ErrorCodes.MembershipApplicationNotFound);

            ProjectEntity project = await projectRepository.GetByIdAsync(application.ProjectId);

            if (project == null)
                return new ResponseContract<MembershipApplicationEntity>(ErrorCodes.ProjectNotFound);

            if (project.OwnerId != request.UserId)
                return new ResponseContract<MembershipApplicationEntity>(ErrorCodes.UserIsNotProjectOwner);


            application.Status = MembershipApplicationStatus.Archive;
            await unitOfWork.SaveChangesAsync();

            return new ResponseContract<MembershipApplicationEntity>();
        }
    }

    public class ApplyMembershipApplicationCommandValidator : AbstractValidator<ApplyMembershipApplicationCommand>
    {
        public ApplyMembershipApplicationCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.ApplicationId).NotEmpty();
        }
    }
}
