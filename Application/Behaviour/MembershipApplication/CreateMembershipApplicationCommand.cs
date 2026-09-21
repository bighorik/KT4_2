using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.MembershipApplication
{
    public class CreateMembershipApplicationCommand : IRequest<ResponseContract<MembershipApplicationEntity>>
    {
        public required Guid UserId { get; set; }
        public required Guid ProjectId { get; set; }
        public string Message { get; set; } = "";
    }

    public class CreateMembershipApplicationCommandHandler(
        IProjectRepository projectRepository,
        IUserRepository userRepository,
        IMembershipApplicationRepository membershipRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<CreateMembershipApplicationCommand, ResponseContract<MembershipApplicationEntity>>
    {
        public async Task<ResponseContract<MembershipApplicationEntity>> Handle(CreateMembershipApplicationCommand request, CancellationToken cancellationToken)
        {
            if (!await projectRepository.ExistsAsync(request.ProjectId))
                return new ResponseContract<MembershipApplicationEntity>(ErrorCodes.ProjectNotFound);

            if (!await userRepository.ExistsAsync(request.UserId))
                return new ResponseContract<MembershipApplicationEntity>(ErrorCodes.UserNotFound);

            MembershipApplicationEntity application = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Message = request.Message,
                ProjectId = request.ProjectId,
                Status = MembershipApplicationStatus.Pending,
            };

            await membershipRepository.CreateAsync(application);
            await unitOfWork.SaveChangesAsync();

            return new ResponseContract<MembershipApplicationEntity>(application);

        }
    }

    public class CreateMembershipApplicationCommandValidator : AbstractValidator<CreateMembershipApplicationCommand>
    {
        public CreateMembershipApplicationCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.ProjectId).NotEmpty();
            RuleFor(command => command.Message).NotNull();
        }
    }
}
