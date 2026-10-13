
using Application.Behaviour.Users;
using Application.Behaviour.UserSkill;
using Application.Utils;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Fto.User;
using WebApi.Fto.UserSkill;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("description")]
        public async Task<ResponseContract<UserFto>> UpdateUserDescription([FromBody] UserToUpdateDescriptionFto dto)
        {
            UpdateUserDescriptionCommand command = new()
            {
                UserId = User.GetId(),
                Description = dto.Description,
            };

            ResponseContract<UserEntity> result = await mediator.Send(command);

            return result.ToDto<UserFto>();
        }
        [HttpPatch("nickname")]
        public async Task<ResponseContract<UserFto>> UpdateUserNickname([FromBody] UserToUpdateNicknameFto dto)
        {
            UpdateUserNicknameCommand command = new()
            {
                UserId = User.GetId(),
                Nickname = dto.Nickname,
            };

            ResponseContract<UserEntity> result = await mediator.Send(command);

            return result.ToDto<UserFto>();
        }
    }
}


