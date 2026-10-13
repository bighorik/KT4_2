using Domain.Enums;
using WebApi.Fto.Project;
using WebApi.Fto.User;

namespace WebApi.Fto.MembershipApplication
{
    public class ProjectMembershipApplicationFto
    {
        public required Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required UserFto User { get; set; }
        public required Guid ProjectId { get; set; }
        public required ProjectShortFto Project { get; set; }
        public string Message { get; set; } = "";
        public required MembershipApplicationStatus Status { get; set; }
    }
}
