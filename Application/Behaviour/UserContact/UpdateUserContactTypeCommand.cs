using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.InternetResourceLink
{
    public class UpdateUserContactTypeCommand : IRequest<ResponseContract<UserContactEntity>>
    {
        public required UserContactType Type { get; set; }
        public Guid ContactId { get; set; }
    }

    public class UpdateUserContactTypeCommandHandler(
        IUserContactRepository contactRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<UpdateUserContactTypeCommand, ResponseContract<UserContactEntity>>
    {
        public async Task<ResponseContract<UserContactEntity>> Handle(UpdateUserContactTypeCommand request, CancellationToken cancellationToken)
        {
            if (!await contactRepository.ExistsAsync(request.ContactId))
                return new ResponseContract<UserContactEntity>(ErrorCodes.UserContactNotFound);

            UserContactEntity link = await contactRepository.UpdateTypeAsync(request.ContactId, request.Type);
            await unitOfWork.SaveChangesAsync();

            return new ResponseContract<UserContactEntity>(link);
        }
    }

    public class UpdateUserContactTypeCommandValidator : AbstractValidator<UpdateUserContactTypeCommand>
    {
        public UpdateUserContactTypeCommandValidator()
        {
            RuleFor(command => command.ContactId).NotEmpty();
            RuleFor(command => command.Type).NotEmpty();
        }
    }
}