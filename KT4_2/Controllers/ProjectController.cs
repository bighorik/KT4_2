using Application.Behaviour.Project;
using Application.Utils;
using Domain.Entities;
using Domain.Fto;
using Domain.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Fto.Project;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/project")]
    public class ProjectController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ResponseContract<ProjectToUserListingFto>> CreateProject()
        {
            CreateProjectCommand command = new()
            {
                OwnerId = User.GetId()
            };

            ResponseContract<ProjectEntity> result = await mediator.Send(command);

            return result.ToDto<ProjectToUserListingFto>();
        }

        [HttpGet("{id}")]
        public async Task<ResponseContract<ProjectToListingFto>> GetProjectById([FromRoute] Guid id)
        {
            GetProjectQuery command = new GetProjectQuery() { ProjectId = id };

            ResponseContract<ProjectEntity> result = await mediator.Send(command);

            return result.ToDto<ProjectToListingFto>();
        }

        [HttpGet("{id}/details")]
        public async Task<ResponseContract<UserProjectFto>> GetDetailedUserProjectById([FromRoute] Guid id)
        {
            GetDetailedProjectQuery command = new GetDetailedProjectQuery() { ProjectId = id };

            ResponseContract<UserProjectModel> result = await mediator.Send(command);

            return result.ToDto<UserProjectFto>();
        }

        [HttpGet("count")]
        public async Task<ResponseContract<int>> GetUserProjectsCount()
        {
            GetUserProjectsCountQuery command = new GetUserProjectsCountQuery() { UserId = User.GetId() };

            ResponseContract<int> result = await mediator.Send(command);

            return result.ToDto<int>();
        }

        [HttpGet]
        public async Task<PaginationContract<ProjectToListingFto>> GetProjectsListing([FromQuery] GetProjectListQueryFto dto)
        {
            GetProjectListQuery command = dto.Adapt<GetProjectListQuery>();

            PaginationContract<ProjectEntity> result = await mediator.Send(command);

            return result.ToPagination<ProjectToListingFto>();
        }

        [HttpGet("my")]
        public async Task<PaginationContract<UserProjectFto>> GetUserProjects()
        {
            GetUserProjectsListQuery command = new GetUserProjectsListQuery()
            {
                UserId = User.GetId()
            };

            PaginationContract<UserProjectModel> result = await mediator.Send(command);

            return result.ToPagination<UserProjectFto>();
        }

        [HttpDelete("{id}")]
        public Task<ResponseContract> DeleteProject([FromRoute] Guid id)
        {
            DeleteProjectCommand command = new()
            {
                ProjectId = id,
            };

            return mediator.Send(command);

        }

        [HttpPatch("{id}/recruitment-allow")]
        public async Task<ResponseContract<ProjectShortFto>> UpdateProjectRecruitmentAllow([FromRoute] Guid id, [FromBody] ProjectToUpdateAdditionalRecruitmentAllowFto dto)
        {
            UpdateProjectAdditionalRecruitmentAllowCommand command = new()
            {
                ProjectId = id,
                IsAdditionalRecruitmentAllowed = dto.IsAdditionalRecruitmentAllowed,
            };

            ResponseContract<ProjectEntity> result = await mediator.Send(command);

            return result.ToDto<ProjectShortFto>();
        }

        [HttpPatch("{id}/strict-selection")]
        public async Task<ResponseContract<ProjectShortFto>> UpdateProjectStrictSelection([FromRoute] Guid id, [FromBody] ProjectToUpdateStrictCandidatesSelectionFto dto)
        {
            UpdateProjectStrictCandidatesSelectionCommand command = new()
            {
                ProjectId = id,
                IsStrictCandidatesSelection = dto.IsStrictCandidatesSelection,
            };

            ResponseContract<ProjectEntity> result = await mediator.Send(command);

            return result.ToDto<ProjectShortFto>();
        }

        [HttpPatch("{id}/description")]
        public async Task<ResponseContract<ProjectShortFto>> UpdateProjectDescription([FromRoute] Guid id, [FromBody] ProjectToUpdateDescriptionFto dto)
        {
            UpdateProjectDescriptionCommand command = new()
            {
                ProjectId = id,
                Description = dto.Description,
            };

            ResponseContract<ProjectEntity> result = await mediator.Send(command);

            return result.ToDto<ProjectShortFto>();
        }

        [HttpPatch("{id}/name")]
        public async Task<ResponseContract<ProjectShortFto>> UpdateProjectName([FromRoute] Guid id, [FromBody] ProjectToUpdateNameFto dto)
        {
            UpdateProjectNameCommand command = new()
            {
                ProjectId = id,
                Name = dto.Name,
            };

            ResponseContract<ProjectEntity> result = await mediator.Send(command);

            return result.ToDto<ProjectShortFto>();
        }

        [HttpPatch("{id}/status")]
        public async Task<ResponseContract<ProjectShortFto>> UpdateProjectStatus([FromRoute] Guid id, [FromBody] ProjectToUpdateStatusFto dto)
        {
            UpdateProjectStatusCommand command = new()
            {
                ProjectId = id,
                ProjectStatus = dto.Status,
            };

            ResponseContract<ProjectEntity> result = await mediator.Send(command);

            return result.ToDto<ProjectShortFto>();
        }

    }
}


