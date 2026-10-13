using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.MembershipApplication
{
    public class ApplyMembershipApplicationCommand : IRequest<ResponseContract>
    {
        public Guid ApplicationId { get; set; }
    }

    public class ApplyMembershipApplicationCommandHandler(
        IMembershipApplicationRepository applicationRepository,
        IProjectMemberRepository memberRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<ApplyMembershipApplicationCommand, ResponseContract>
    {
        public async Task<ResponseContract> Handle(ApplyMembershipApplicationCommand request, CancellationToken cancellationToken)
        {
            MembershipApplicationEntity? app = await applicationRepository.GetFullIncludedByIdAsync(request.ApplicationId);
            if (app == null)
                return new ResponseContract(ErrorCodes.MembershipApplicationNotFound);

            ProjectMemberEntity member = new()
            {
                Id = Guid.NewGuid(),
                ProjectId = app.Position.ProjectId,
                UserId = app.UserId,
                PositionId = app.PositionId,
            };

            await memberRepository.CreateAsync(member);
            await applicationRepository.UpdateMembershipStatusAsync(request.ApplicationId, MembershipApplicationStatus.Approved);
            await unitOfWork.SaveChangesAsync();

            return new ResponseContract();
        }
    }

    public class ApplyMembershipApplicationCommandValidator : AbstractValidator<ApplyMembershipApplicationCommand>
    {
        public ApplyMembershipApplicationCommandValidator()
        {
            RuleFor(command => command.ApplicationId).NotEmpty();
        }
    }
}
