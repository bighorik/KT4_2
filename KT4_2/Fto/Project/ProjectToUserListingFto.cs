using Domain.Entities;
using Domain.Enums;

namespace WebApi.Fto.Project
{
    public class ProjectToUserListingFto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<ProjectPositionEntity> RequiredPositions = [];
        public List<InternetResourceLinkEntity> InternetResourceLinks = [];
        public List<AttachmentEntity> Attachments = [];

        public required ProjectStatus Status { get; set; }
        public required bool IsAdditionalRecruitmentAllowed = false;
        public required bool IsStrictCandidatesSelection = true;
    }
}
