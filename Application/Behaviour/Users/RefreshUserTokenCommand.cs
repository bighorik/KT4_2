using Application.Dto;
using Application.Utils;
using MediatR;

namespace Application.Behaviour.Users
{
    public class RefreshUserTokenCommand : IRequest<ResponseContract<LoginDto>>
    {
        public required string RefreshToken { get; set; }
    }
}
