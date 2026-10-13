using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Users
{
    public class UpdateUserDescriptionCommand : IRequest<ResponseContract<UserEntity>>
    {
        public required Guid UserId { get; set; }
        public required string Description { get; set; }
    }

    public class UpdateUserDescriptionCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<UpdateUserDescriptionCommand, ResponseContract<UserEntity>>
    {
        public async Task<ResponseContract<UserEntity>> Handle(UpdateUserDescriptionCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = await userRepository.UpdateUserDescriptionAsync(request.UserId, request.Description);
            await unitOfWork.SaveChangesAsync();

            return new ResponseContract<UserEntity>(user);
        }
    }

    public class UpdateUserDescriptionCommandValidator : AbstractValidator<UpdateUserDescriptionCommand>
    {
        public UpdateUserDescriptionCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.Description).MaximumLength(500);
        }
    }
}
