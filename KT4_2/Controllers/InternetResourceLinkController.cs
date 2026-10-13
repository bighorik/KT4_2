using Application.Behaviour.InternetResourceLink;
using Application.Utils;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Fto.InternetResourceLink;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/project/{projectId}/links")]
    public class InternetResourceLinkController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ResponseContract<InternetResourceLinkFto>> CreateInternetResourceLink([FromRoute] Guid projectId)
        {
            CreateInternetResourceLinkCommand command = new()
            {
                ProjectId = projectId,
            };

            ResponseContract<InternetResourceLinkEntity> result = await mediator.Send(command);

            return result.ToDto<InternetResourceLinkFto>();
        }

        [HttpDelete("{id}")]
        public Task<ResponseContract> DeleteUserInternetResourceLink([FromRoute] Guid id)
        {
            DeleteInternetResourceLinkCommand command = new()
            {
                LinkId = id,
            };

            return mediator.Send(command);
        }

        [HttpPatch("{id}/url")]
        public async Task<ResponseContract<InternetResourceLinkFto>> UpdateInternetResourceLinkUrl([FromRoute] Guid id, [FromBody] InternetResourceLinkToUpdateUrlFto dto)
        {
            UpdateInternetResourceLinkUrlCommand command = new()
            {
                LinkId = id,
                Url = dto.Url
            };

            ResponseContract<InternetResourceLinkEntity> result = await mediator.Send(command);

            return result.ToDto<InternetResourceLinkFto>();
        }

        [HttpPatch("{id}/type")]
        public async Task<ResponseContract<InternetResourceLinkFto>> UpdateInternetResourceLinkType([FromRoute] Guid id, [FromBody] InternetResourceLinkToUpdateTypeFto dto)
        {
            UpdateInternetResourceLinkTypeCommand command = new()
            {
                LinkId = id,
                Type = dto.Type
            };

            ResponseContract<InternetResourceLinkEntity> result = await mediator.Send(command);

            return result.ToDto<InternetResourceLinkFto>();
        }
    }
}


