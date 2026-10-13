using Application.Behaviour.ProjectMember;
using Application.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/project/{projectId}/member")]
    public class ProjectMemberController(IMediator mediator) : ControllerBase
    {
        [HttpDelete("{id}")]
        public Task<ResponseContract> DeleteProjectMember([FromRoute] Guid id)
        {
            DeleteProjectMembershipCommand command = new()
            {
                MemberId = id
            };

            return mediator.Send(command);
        }
    }
}


