using Application.Utils;

namespace WebApi.Fto.Project
{
    public class GetProjectListQueryFto : PaginationQuery
    {
        public string? Name { get; set; }
        public bool IncludeRequitingCompletedProjects { get; set; } = false;
        public List<Guid> SkillIds { get; set; } = [];
        // public bool? FilterByUserSkills { get; set; }
        // public bool? RequireSkillLevelMatch { get; set; }
        public bool? OnlyOwnersProjects { get; set; }
    }
}
