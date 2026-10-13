using Domain.Enums;

namespace WebApi.Fto.UserSkill
{
    public class UserSkillFto
    {
        public required Guid Id { get; set; }
        public required string SkillName { get; set; }
        public SkillLevel SkillLevel { get; set; }
    }
}
