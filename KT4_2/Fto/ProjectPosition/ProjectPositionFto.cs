using WebApi.Fto.PositionSkill;

namespace WebApi.Fto.ProjectPosition
{
    public class ProjectPositionFto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; } = "";
        public List<PositionSkillFto> PositionSkills { get; set; } = [];
    }
}
