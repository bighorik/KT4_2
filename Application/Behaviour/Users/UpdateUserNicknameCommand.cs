using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Users
{
    public class UpdateUserNicknameCommand : IRequest<ResponseContract<UserEntity>>
    {
        public required Guid UserId { get; set; }
        public required string Nickname { get; set; }
    }

    public class UpdateUserNicknameCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<UpdateUserNicknameCommand, ResponseContract<UserEntity>>
    {
        public async Task<ResponseContract<UserEntity>> Handle(UpdateUserNicknameCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = await userRepository.UpdateUserNicknameAsync(request.UserId, request.Nickname);
            await unitOfWork.SaveChangesAsync();

            return new ResponseContract<UserEntity>(user);
        }
    }

    public class UpdateUserNicknameCommandValidator : AbstractValidator<UpdateUserNicknameCommand>
    {
        public UpdateUserNicknameCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.Nickname).MinimumLength(2).MaximumLength(30);
        }
    }
}
