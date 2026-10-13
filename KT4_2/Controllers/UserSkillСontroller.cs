
using Application.Behaviour.UserSkill;
using Application.Utils;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Fto.UserSkill;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/user/skill")]
    public class UserSkillController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ResponseContract<UserSkillFto>> CreateUserSkill()
        {
            CreateUserSkillCommand command = new()
            {
                UserId = User.GetId(),
            };

            ResponseContract<UserSkillEntity> result = await mediator.Send(command);

            return result.ToDto<UserSkillFto>();
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

        [HttpPatch("{id}/level")]
        public async Task<ResponseContract<UserSkillFto>> UpdateUserSkillLevel([FromRoute] Guid id, [FromBody] UserSkillToUpdateLevelFto dto)
        {
            UpdateUserSkillLevelCommand command = new()
            {
                UserSkillId = id,
                SkillLevel = dto.SkillLevel,
            };

            ResponseContract<UserSkillEntity> result = await mediator.Send(command);

            return result.ToDto<UserSkillFto>();
        }

        [HttpPatch("{id}/skill")]
        public async Task<ResponseContract<UserSkillFto>> UpdateUserSkill([FromRoute] Guid id, [FromBody] UserSkillToUpdateSkillFto dto)
        {
            UpdateUserSkillIdCommand command = new()
            {
                UserId = User.GetId(),
                UserSkillId = id,
                SkillId = dto.SkillId,
            };

            ResponseContract<UserSkillEntity> result = await mediator.Send(command);

            return result.ToDto<UserSkillFto>();
        }
    }
}


