using Application.Behaviour.ProjectPosition;
using Application.Behaviour.Skill;
using Application.Utils;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Fto.StudentGroupNumber;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/student-group-number")]
    public class StudentGroupNumberController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ResponseContract<StudentGroupNumberFto>> CreateStudentGroupNumber([FromBody] StudentGroupNumberToCreateOrUpdateNameFto dto)
        {
            CreateStudentGroupNumberCommand command = new()
            {
                Name = dto.Name,
            };

            ResponseContract<StudentGroupNumberEntity> result = await mediator.Send(command);

            return result.ToDto<StudentGroupNumberFto>();
        }

        [HttpGet]
        public async Task<PaginationContract<StudentGroupNumberFto>> CreateStudentGroupNumber([FromQuery] StudentGroupNumberQueryFto query)
        {
            PaginationContract<StudentGroupNumberEntity> result = await mediator.Send(query.Adapt<GetStudentGroupNumbersQuery>());

            return result.ToPagination<StudentGroupNumberFto>();
        }

        [HttpDelete("{id}")]
        public Task<ResponseContract> DeleteUserStudentGroupNumber([FromRoute] Guid id)
        {
            DeleteStudentGroupNumberCommand command = new()
            {
                GroupId = id,
            };

            return mediator.Send(command);
        }

        [HttpPatch("{id}/name")]
        public async Task<ResponseContract<StudentGroupNumberFto>> UpdateStudentGroupNumberName([FromRoute] Guid id, [FromBody] StudentGroupNumberToCreateOrUpdateNameFto dto)
        {
            UpdateStudentGroupNumberNameCommand command = new()
            {
                GroupId = id,
                Name = dto.Name
            };

            ResponseContract<StudentGroupNumberEntity> result = await mediator.Send(command);

            return result.ToDto<StudentGroupNumberFto>();
        }
    }
}


