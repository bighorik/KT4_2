using Domain.Enums;

namespace WebApi.Fto.PositionSkill
{
    public class PositionSkillFto
    {
        public Guid Id { get; set; }
        public required string SkillName { get; set; }
        public SkillLevel SkillLevel { get; set; }
    }
}
