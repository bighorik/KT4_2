using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Utils;
using BCrypt.Net;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Users
{
    public class ConfirmUserEmailCommand : IRequest<ResponseContract>
    {
        public required string Code { get; set; }
    }

    public class ConfirmUserEmailCommandHandler(
        IUserRepository userRepository,
        IAccountConfirmationCodeRepository codeRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<ConfirmUserEmailCommand, ResponseContract>
    {
        public async Task<ResponseContract> Handle(ConfirmUserEmailCommand request, CancellationToken cancellationToken)
        {
            AccountConfirmationCodeEntity? code = await codeRepository.GetCodeByCodeAsync(request.Code);
            if (code == null)
                return new ResponseContract(ErrorCodes.ActualAccountConfirmationCodeNotFound);

            await codeRepository.MarkCodeAsUsedAsync(code.Id);
            await userRepository.MarkEmailAsConfirmedAsync(code.UserId);

            await unitOfWork.SaveChangesAsync();

            return new ResponseContract();
        }
    }

    public class ConfirmUserEmailCommandValidator : AbstractValidator<ConfirmUserEmailCommand>
    {
        public ConfirmUserEmailCommandValidator()
        {
            RuleFor(command => command.Code).Empty();
        }
    }
}
