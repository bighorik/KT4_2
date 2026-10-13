namespace WebApi.Fto.Project
{
    public class ProjectShortFto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool IsAdditionalRecruitmentAllowed = false;
        public required bool IsStrictCandidatesSelection = true;
    }
}
