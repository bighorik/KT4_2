using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Users
{
    public class GetUserByIdQuery : IRequest<ResponseContract<UserEntity>>
    {
        public required Guid UserId { get; set; }
    }

    public class GetUserByIdQueryHandler(
        IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, ResponseContract<UserEntity>>
    {
        public async Task<ResponseContract<UserEntity>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            UserEntity? user = await userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                return new ResponseContract<UserEntity>(ErrorCodes.UserNotFound);

            return new ResponseContract<UserEntity>(user);
        }
    }

    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
        }
    }
}
