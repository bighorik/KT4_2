using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.MembershipApplication
{
    public class GetProjectMembershipApplicationsQuery : PaginationQuery, IRequest<PaginationContract<MembershipApplicationEntity>>
    {
        public required Guid UserId { get; set; }
        public required Guid ProjectId { get; set; }
        public MembershipApplicationStatus? Status { get; set; }
    }

    public class GetProjectMembershipApplicationsQueryHandler(
            IMembershipApplicationRepository applicationRepository
        ) : IRequestHandler<GetProjectMembershipApplicationsQuery, PaginationContract<MembershipApplicationEntity>>
    {
        public async Task<PaginationContract<MembershipApplicationEntity>> Handle(GetProjectMembershipApplicationsQuery request, CancellationToken cancellationToken)
        {
            return await applicationRepository.GetApplicationsWithUserByProjectAsync(request);
        }
    }

    public class GetProjectMembershipApplicationsQueryValidator : BasePaginationValidator<GetProjectMembershipApplicationsQuery>
    {
        public GetProjectMembershipApplicationsQueryValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.ProjectId).NotEmpty();
        }
    }
}
