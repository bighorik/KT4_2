using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behaviour.Project
{
    public record CreateProjectCommand : IRequest
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required Guid OwnerId { get; set; }
        public List<CreateProjectPosition> RequiredPositions = [];
        public List<CreateInternetResourceLink> InternetResourceLinks = [];
        public List<Guid> AttachmentIds = [];
        public required bool IsAdditionalRecruitmentAllowed = false;
        public required bool IsStrictCandidatesSelection = false;

        public record CreateInternetResourceLink
        {
            public required InternetResourceLinkType Type { get; set; }
            public required string Url { get; set; }
        }

        public record CreateProjectPosition
        {
            public required string Name { get; set; }
            public List<Guid> RequiredSkillIds { get; set; } = [];
        }
    }
}
