using Domain.Enums;

namespace WebApi.Fto.Project
{
    public class ProjectToUpdateStatusFto
    {
        public required ProjectStatus Status { get; set; }
    }
}
