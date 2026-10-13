using Application.Utils;

namespace WebApi.Fto.Skill
{
    public class SkillQueryFto : PaginationQuery
    {
        public string? Name { get; set; }
    }
}
