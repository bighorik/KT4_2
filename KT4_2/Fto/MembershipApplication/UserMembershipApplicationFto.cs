using Domain.Enums;
using WebApi.Fto.Project;
using WebApi.Fto.ProjectPosition;

namespace WebApi.Fto.MembershipApplication
{
    public class UserMembershipApplicationFto
    {
        public required Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required Guid ProjectId { get; set; }
        public required ProjectShortFto Project { get; set; }
        public required Guid PositionId { get; set; }
        public required ProjectPositionFto Position { get; set; }
        public string Message { get; set; } = "";
        public required MembershipApplicationStatus Status { get; set; }
    }
}
