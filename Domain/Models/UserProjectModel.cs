using Domain.Entities;
using Domain.Enums;

namespace Domain.Models
{
    public class UserProjectModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<UserProjectPositionModel> RequiredPositions { get; set; } = [];
        public List<UserProjectInternetResourceLinkModel> InternetResourceLinks { get; set; } = [];
        public required ProjectStatus Status { get; set; }
        public required bool IsAdditionalRecruitmentAllowed { get; set; } = false;
        public required bool IsStrictCandidatesSelection { get; set; } = true;
        public int MembershipApplicationsCount { get; set; }
    }

    public class UserProjectMemberModel
    {
        public required Guid Id { get; set; }
        public required string Nickname { get; set; }
        public required string Email { get; set; }
        public string? AvatarUrl { get; set; }
    }

    public class UserProjectInternetResourceLinkModel
    {
        public required Guid Id { get; set; }
        public required InternetResourceLinkType Type { get; set; }
        public required string Url { get; set; }
    }

    public class UserProjectPositionModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; } = "";
        public List<UserProjectPositionSkillModel> PositionSkills { get; set; } = [];
        public List<UserProjectMembershipApplicationModel> MembershipApplications { get; set; } = [];
        public UserProjectMemberModel? ProjectMember { get; set; }
    }

    public class UserProjectMembershipApplicationModel
    {
        public required Guid Id { get; set; }
        public UserProjectMemberModel User { get; set; }
        public string Message { get; set; } = "";
        public required MembershipApplicationStatus Status { get; set; }
    }

    public class UserProjectPositionSkillModel
    {
        public required Guid Id { get; set; }
        public Guid SkillId { get; set; }
        public SkillLevel SkillLevel { get; set; }
        public string Name { get; set; }
    }
}
