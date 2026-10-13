using Application.Behaviour.ProjectPositionSkill;
using Application.Utils;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Fto.PositionSkill;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/project/{projectId}/position/{positionId}/skill")]
    public class PositionSkillController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ResponseContract<PositionSkillFto>> CreatePositionSkill([FromBody] PositionSkillToCreateFto dto)
        {
            CreateProjectPositionSkillCommand command = new()
            {
                ProjectPositionId = dto.ProjectPositionId,
                SkillId = dto.SkillId,
            };

            ResponseContract<PositionSkillEntity> result = await mediator.Send(command);

            return result.ToDto<PositionSkillFto>();
        }

        [HttpDelete("{id}")]
        public Task<ResponseContract> DeleteUserPositionSkill([FromRoute] Guid id)
        {
            DeleteProjectPositionSkillCommand command = new()
            {
                PositionSkillId = id
            };

            return mediator.Send(command);
        }

        [HttpPatch("{id}/level")]
        public async Task<ResponseContract<PositionSkillFto>> UpdatePositionSkillLevel([FromRoute] Guid id, [FromBody] PositionSkillToUpdateSkillLevelFto dto)
        {
            UpdateProjectPositionSkillLevelCommand command = new()
            {
                PositionSkillId = id,
                SkillLevel = dto.SkillLevel,
            };

            ResponseContract<PositionSkillEntity> result = await mediator.Send(command);

            return result.ToDto<PositionSkillFto>();
        }
    }
}


