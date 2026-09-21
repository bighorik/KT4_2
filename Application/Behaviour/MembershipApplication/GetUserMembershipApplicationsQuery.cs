using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;
using Minio.DataModel.ILM;

namespace Application.Behaviour.MembershipApplication
{
    public class GetUserMembershipApplicationsQuery : PaginationQuery, IRequest<PaginationContract<MembershipApplicationEntity>>
    {
        public required Guid UserId { get; set; }
        public MembershipApplicationStatus? Status { get; set; }
    }

    public class GetUserMembershipApplicationsQueryHandler(
            IMembershipApplicationRepository applicationRepository
        ) : IRequestHandler<GetUserMembershipApplicationsQuery, PaginationContract<MembershipApplicationEntity>>
    {
        public async Task<PaginationContract<MembershipApplicationEntity>> Handle(GetUserMembershipApplicationsQuery request, CancellationToken cancellationToken)
        {
            return await applicationRepository.GetApplicationsWithProjectByUserAsync(request);
        }
    }

    public class GetUserMembershipApplicationsQueryValidator : BasePaginationValidator<GetUserMembershipApplicationsQuery>
    {
        public GetUserMembershipApplicationsQueryValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
        }
    }
}
