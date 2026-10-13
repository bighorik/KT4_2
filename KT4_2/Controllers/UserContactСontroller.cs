using Application.Behaviour.InternetResourceLink;
using Application.Utils;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Fto.UserContact;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/user/contact")]
    public class UserContact—ontroller(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ResponseContract<UserContactFto>> CreateUserContact()
        {
            CreateUserContactCommand command = new()
            {
                UserId = User.GetId(),
            };

            ResponseContract<UserContactEntity> result = await mediator.Send(command);

            return result.ToDto<UserContactFto>();
        }


        [HttpDelete("{id}")]
        public Task<ResponseContract> DeleteUserContact([FromRoute] Guid id)
        {
            DeleteUserContactCommand command = new()
            {
                ContactId = id,
            };

            return mediator.Send(command);
        }

        [HttpPatch("{id}/value")]
        public async Task<ResponseContract<UserContactFto>> UpdateUserContactValue([FromRoute] Guid id, [FromBody] UserContactToUpdateValueFto dto)
        {
            UpdateUserContactValueCommand command = new()
            {
                ContactId = id,
                Value = dto.Value,
            };

            ResponseContract<UserContactEntity> result = await mediator.Send(command);

            return result.ToDto<UserContactFto>();
        }

        [HttpPatch("{id}/type")]
        public async Task<ResponseContract<UserContactFto>> UpdateUserContactType([FromRoute] Guid id, [FromBody] UserContactToUpdateTypeFto dto)
        {
            UpdateUserContactTypeCommand command = new()
            {
                ContactId = id,
                Type = dto.Type,
            };

            ResponseContract<UserContactEntity> result = await mediator.Send(command);

            return result.ToDto<UserContactFto>();
        }
    }
}


