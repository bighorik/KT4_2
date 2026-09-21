using Application.Behaviour.Users;
using Application.Utils;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Fto.User;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ResponseContract<UserFto>> CreateUser([FromBody] UserToCreateFto userDto)
        {
            ResponseContract<UserEntity> result = await mediator.Send(userDto.Adapt<CreateUserCommand>());

            return result.ToDto<UserFto>();
        }
    }
}


