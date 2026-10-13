using Application.Utils;
using Domain.Enums;

namespace WebApi.Fto.MembershipApplication
{
    public class GetUserMembershipApplicationsQueryFto : PaginationQuery
    {
        public MembershipApplicationStatus? Status { get; set; }
    }
}
