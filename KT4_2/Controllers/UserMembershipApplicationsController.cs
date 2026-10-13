using Application.Behaviour.MembershipApplication;
using Application.Behaviour.Project;
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
    [Route("/api/user/membership-application")]
    public class UserMembershipApplicationsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ResponseContract<UserMembershipApplicationFto>> CreateMembershipApplication([FromBody] MembershipApplicationToCreateFto dto)
        {
            CreateMembershipApplicationCommand command = new()
            {
                PositionId = dto.PositionId,
                UserId = User.GetId(),
                Message = dto.Message,
            };

            ResponseContract<MembershipApplicationEntity> result = await mediator.Send(command);

            return result.ToDto<UserMembershipApplicationFto>();
        }

        [HttpDelete("{id}")]
        public Task<ResponseContract> DeleteMembershipApplication([FromRoute] Guid id)
        {
            DeleteMembershipApplicationCommand command = new()
            {
                ApplicationId = id
            };

            return mediator.Send(command);
        }

        [HttpGet]
        public async Task<PaginationContract<UserMembershipApplicationFto>> GetUserMembershipApplications([FromBody] GetUserMembershipApplicationsQueryFto dto)
        {
            PaginationContract<MembershipApplicationEntity> result = await mediator.Send(dto.Adapt<GetUserMembershipApplicationsQuery>());

            return result.ToPagination<UserMembershipApplicationFto>();
        }


        [HttpGet("count-by-all-projects")]
        public Task<ResponseContract<int>> GetUserMembershipApplicationsCount()
        {
            GetUserProjectsMemberApplicationsCountQuery command = new()
            {
                UserId = User.GetId(),
            };

            return mediator.Send(command);
        }
    }
}


