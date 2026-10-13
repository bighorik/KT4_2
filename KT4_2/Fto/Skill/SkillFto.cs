namespace WebApi.Fto.Skill
{
    public class SkillFto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required bool IsArchived { get; set; }
    }
}
