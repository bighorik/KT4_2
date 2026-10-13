using Domain.Enums;

namespace WebApi.Fto.PositionSkill
{
    public class PositionSkillToCreateFto
    {
        public required Guid SkillId { get; set; }
        public required Guid ProjectPositionId { get; set; }
    }
}
