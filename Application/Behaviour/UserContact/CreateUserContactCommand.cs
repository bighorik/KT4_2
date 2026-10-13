using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.InternetResourceLink
{
    public class CreateUserContactCommand : IRequest<ResponseContract<UserContactEntity>>
    {
        public Guid UserId { get; set; }
    }

    public class CreateUserContactCommandHandler(
        IUserContactRepository contactRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<CreateUserContactCommand, ResponseContract<UserContactEntity>>
    {
        public async Task<ResponseContract<UserContactEntity>> Handle(CreateUserContactCommand request, CancellationToken cancellationToken)
        {
            UserContactEntity contact = new()
            {
                Id = Guid.NewGuid(),
                Value = "",
                Type = UserContactType.Telegram,
                UserId = request.UserId,
            };

            await contactRepository.CreateAsync(contact);
            await unitOfWork.SaveChangesAsync();

            return new ResponseContract<UserContactEntity>(contact);
        }
    }

    public class CreateUserContactCommandValidator : AbstractValidator<CreateUserContactCommand>
    {
        public CreateUserContactCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
        }
    }
}