using Application.Behaviour.Skill;
using Application.Behaviour.UserSkill;
using Application.Utils;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Fto.Skill;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/skill")]
    public class SkillController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ResponseContract<SkillFto>> CreateSkill([FromBody] SkillToCreateOrUpdateNameFto dto)
        {
            CreateSkillCommand command = new()
            {
                Name = dto.Name,
            };

            ResponseContract<SkillEntity> result = await mediator.Send(command);

            return result.ToDto<SkillFto>();
        }

        [HttpGet]
        public async Task<PaginationContract<SkillFto>> CreateSkill([FromQuery] SkillQueryFto query)
        {
            PaginationContract<SkillEntity> result = await mediator.Send(query.Adapt<GetSkillsQuery>());

            return result.ToPagination<SkillFto>();
        }

        [HttpDelete("{id}")]
        public Task<ResponseContract> DeleteUserSkill([FromRoute] Guid id)
        {
            DeleteUserSkillCommand command = new()
            {
                UserSkillId = id,
            };

            return mediator.Send(command);
        }

        [HttpPatch("{id}/name")]
        public async Task<ResponseContract<SkillFto>> UpdateSkillName([FromRoute] Guid id, [FromBody] SkillToCreateOrUpdateNameFto dto)
        {
            UpdateSkillNameCommand command = new()
            {
                SkillId = id,
                Name = dto.Name
            };

            ResponseContract<SkillEntity> result = await mediator.Send(command);

            return result.ToDto<SkillFto>();
        }
    }
}


