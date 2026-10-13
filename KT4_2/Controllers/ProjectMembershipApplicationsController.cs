using Application.Behaviour.MembershipApplication;
using Application.Utils;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Fto.MembershipApplication;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/project/{projectId}/membership-applications")]
    public class ProjectMembershipApplicationsController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("{id}/reject")]
        public Task<ResponseContract> RejectMembershipApplication([FromRoute] Guid id)
        {
            RejectMembershipApplicationCommand command = new()
            {
                ApplicationId = id
            };

            return mediator.Send(command);
        }


        [HttpPatch("{id}/apply")]
        public Task<ResponseContract> ApplyMembershipApplication([FromRoute] Guid id)
        {
            ApplyMembershipApplicationCommand command = new()
            {
                ApplicationId = id
            };

            return mediator.Send(command);
        }

        [HttpGet]
        public async Task<PaginationContract<ProjectMembershipApplicationFto>> GetUserMembershipApplications([FromBody] GetProjectMembershipApplicationsQueryFto dto)
        {
            GetProjectMembershipApplicationsQuery query = dto.Adapt<GetProjectMembershipApplicationsQuery>();
            query.UserId = User.GetId();
            PaginationContract<MembershipApplicationEntity> result = await mediator.Send(query);

            return result.ToPagination<ProjectMembershipApplicationFto>();
        }
    }
}


