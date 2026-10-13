using Domain.Enums;

namespace Domain.Fto
{
    public class UserProjectFto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<UserProjectPositionFto> RequiredPositions { get; set; } = [];
        public List<UserProjectInternetResourceLinkFto> InternetResourceLinks { get; set; } = [];
        public required ProjectStatus Status { get; set; }
        public required bool IsAdditionalRecruitmentAllowed { get; set; } = false;
        public required bool IsStrictCandidatesSelection { get; set; } = true;
        public int MembershipApplicationsCount { get; set; }
    }

    public class UserProjectMemberFto
    {
        public required Guid Id { get; set; }
        public required string Nickname { get; set; }
        public required string Email { get; set; }
        public string? AvatarUrl { get; set; }
    }

    public class UserProjectInternetResourceLinkFto
    {
        public required Guid Id { get; set; }
        public required InternetResourceLinkType Type { get; set; }
        public required string Url { get; set; }
    }

    public class UserProjectPositionFto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; } = "";
        public List<UserProjectPositionSkillFto> PositionSkills { get; set; } = [];
        public List<UserProjectMembershipApplicationFto> MembershipApplications { get; set; } = [];
        public UserProjectMemberFto? ProjectMember { get; set; }
    }

    public class UserProjectMembershipApplicationFto
    {
        public required Guid Id { get; set; }
        public UserProjectMemberFto User { get; set; }
        public string Message { get; set; } = "";
        public required MembershipApplicationStatus Status { get; set; }
    }

    public class UserProjectPositionSkillFto
    {
        public required Guid Id { get; set; }
        public Guid SkillId { get; set; }
        public SkillLevel SkillLevel { get; set; }
        public string Name { get; set; }
    }
}


