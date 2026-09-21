using Application.Dto;
using Application.Utils;
using MediatR;

namespace Application.Behaviour.Users
{
    public class LoginUserCommand : IRequest<ResponseContract<LoginDto>>
    {
    }
}
