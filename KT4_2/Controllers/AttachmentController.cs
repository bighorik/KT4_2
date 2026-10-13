using Application.Behaviour.Attachment;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Fto.Attachments;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/attachments/")]
    public class AttachmentController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ResponseContract<AttachmentFto>> UploadPhoto(IFormFile file)
        {
            CreateAttachmentCommand command = new()
            {
                File = file,
                AttachmentType = AttachmentType.Photo,
                UserId = User.GetId(),
            };

            ResponseContract<AttachmentEntity> result = await mediator.Send(command);

            return result.ToDto<AttachmentFto>();
        }
    }
}


