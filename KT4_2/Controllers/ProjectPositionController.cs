using Application.Behaviour.ProjectMember;
using Application.Behaviour.ProjectPosition;
using Application.Utils;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Fto.ProjectPosition;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/project/{projectId}/position")]
    public class ProjectPositionController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ResponseContract<ProjectPositionFto>> CreateProjectPosition([FromRoute] Guid projectId)
        {
            CreateProjectPositionCommand command = new() { ProjectId = projectId };

            ResponseContract<ProjectPositionEntity> result = await mediator.Send(command);
            return result.ToDto<ProjectPositionFto>();
        }

        [HttpPatch("{id}/name")]
        public async Task<ResponseContract<ProjectPositionFto>> UpdateProjectPositionName([FromBody] ProjectPositionToUpdateNameFto dto)
        {
            UpdateProjectPositionNameCommand command = dto.Adapt<UpdateProjectPositionNameCommand>();

            ResponseContract<ProjectPositionEntity> result = await mediator.Send(command);
            return result.ToDto<ProjectPositionFto>();
        }

        [HttpPatch("{id}/description")]
        public async Task<ResponseContract<ProjectPositionFto>> UpdateProjectPositionDescription([FromBody] ProjectPositionToUpdateDescriptionFto dto)
        {
            UpdateProjectPositionDescriptionCommand command = dto.Adapt<UpdateProjectPositionDescriptionCommand>();

            ResponseContract<ProjectPositionEntity> result = await mediator.Send(command);
            return result.ToDto<ProjectPositionFto>();
        }

        [HttpDelete("{id}")]
        public Task<ResponseContract> DeleteProjectPosition([FromRoute] Guid id)
        {
            DeleteProjectPositionCommand command = new()
            {
                ProjectPositionId = id
            };

            return mediator.Send(command);
        }
    }
}


