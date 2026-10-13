using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.InternetResourceLink
{
    public class UpdateUserContactValueCommand : IRequest<ResponseContract<UserContactEntity>>
    {
        public required string Value { get; set; }
        public Guid ContactId { get; set; }
    }

    public class UpdateUserContactValueCommandHandler(
        IUserContactRepository contactRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<UpdateUserContactValueCommand, ResponseContract<UserContactEntity>>
    {
        public async Task<ResponseContract<UserContactEntity>> Handle(UpdateUserContactValueCommand request, CancellationToken cancellationToken)
        {
            if (!await contactRepository.ExistsAsync(request.ContactId))
                return new ResponseContract<UserContactEntity>(ErrorCodes.UserContactNotFound);

            UserContactEntity link = await contactRepository.UpdateValueAsync(request.ContactId, request.Value);
            await unitOfWork.SaveChangesAsync();

            return new ResponseContract<UserContactEntity>(link);
        }
    }

    public class UpdateUserContactValueCommandValidator : AbstractValidator<UpdateUserContactValueCommand>
    {
        public UpdateUserContactValueCommandValidator()
        {
            RuleFor(command => command.ContactId).NotEmpty();
            RuleFor(command => command.Value).NotEmpty();
        }
    }
}