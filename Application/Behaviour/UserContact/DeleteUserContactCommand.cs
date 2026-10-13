using Application.Interfaces.Repositories;
using Application.Utils;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.InternetResourceLink
{
    public class DeleteUserContactCommand : IRequest<ResponseContract>
    {
        public Guid ContactId { get; set; }
    }

    public class DeleteUserContactCommandHandler(
        IUserContactRepository contactRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<DeleteUserContactCommand, ResponseContract>
    {
        public async Task<ResponseContract> Handle(DeleteUserContactCommand request, CancellationToken cancellationToken)
        {
            await contactRepository.DeleteByIdAsync(request.ContactId);

            await unitOfWork.SaveChangesAsync();
            return new ResponseContract();
        }
    }

    public class DeleteUserContactCommandValidator : AbstractValidator<DeleteUserContactCommand>
    {
        public DeleteUserContactCommandValidator()
        {
            RuleFor(command => command.ContactId).NotEmpty();
        }
    }
}