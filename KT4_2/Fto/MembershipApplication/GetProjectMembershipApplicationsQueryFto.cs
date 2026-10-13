using Application.Utils;
using Domain.Enums;

namespace WebApi.Fto.MembershipApplication
{
    public class GetProjectMembershipApplicationsQueryFto : PaginationQuery
    {
        public required Guid ProjectId { get; set; }
        public MembershipApplicationStatus? Status { get; set; }
    }
}
