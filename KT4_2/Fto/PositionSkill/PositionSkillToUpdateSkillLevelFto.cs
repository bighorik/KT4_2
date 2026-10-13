using Domain.Enums;

namespace WebApi.Fto.PositionSkill
{
    public class PositionSkillToUpdateSkillLevelFto
    {
        public required Guid PositionSkillId { get; set; }
        public required SkillLevel SkillLevel { get; set; }
    }
}
