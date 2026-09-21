using MediatR;

namespace Application.Behaviour.Users
{
    public class LogoutUserCommand : IRequest
    {
        public required Guid UserId { get; set; }
    }
}
