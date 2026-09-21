using Application.Utils;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.Users
{
    public class GetUserByIdQuery : IRequest<ResponseContract<UserEntity>>
    {
        public required Guid UserId { get; set; }
    }
}
