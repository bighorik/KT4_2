using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Utils;
using BCrypt.Net;
using Domain.Entities;
using FluentValidation;
using MediatR;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Application.Behaviour.Users
{
    public class CreateUserCommand : IRequest<ResponseContract<UserEntity>>
    {
        public required string NickName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required Guid StudentGroupNumberId { get; set; }
    }

    public class CreateUserCommandHandler(
        IUserRepository userRepository,
        IEmailService emailService,
        IAccountConfirmationCodeRepository codeRepository,
        ICryptoService cryptoService,
        IUnitOfWork unitOfWork,
        IStudentGroupRepository studentGroupRepository
        ) : IRequestHandler<CreateUserCommand, ResponseContract<UserEntity>>
    {
        public async Task<ResponseContract<UserEntity>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            UserEntity? userResult = await userRepository.GetByEmailAsync(request.Email);
            if (userResult != null)
            {
                if (userResult.EmailConfirmed)
                    return new ResponseContract<UserEntity>(ErrorCodes.EmailAlreadyTaken);

                AccountConfirmationCodeEntity? codeEntity = await codeRepository.GetByEmailAsync(request.Email);
                if (codeEntity != null)
                    return new ResponseContract<UserEntity>(ErrorCodes.AccountConfirmationCodeAlreadySent);
            }

            if (!await studentGroupRepository.ExistsAsync(request.StudentGroupNumberId))
                return new ResponseContract<UserEntity>(ErrorCodes.StudentGroupNotFound);

            string passwordHash = cryptoService.GetPasswordHash(request.Password);
            string code = cryptoService.GenerateConfirmationCode();
            StudentGroupNumberEntity groupStub = studentGroupRepository.GetStub(request.StudentGroupNumberId);

            UserEntity user = new()
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                NickName = request.NickName,
                PasswordHash = passwordHash,
                StudentGroupNumber = groupStub,
            };

            AccountConfirmationCodeEntity confirmationCode = new()
            {
                Id = Guid.NewGuid(),
                Code = code,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
            };

            await codeRepository.CreateAsync(confirmationCode);
            await userRepository.CreateAsync(user);
            await unitOfWork.SaveChangesAsync();
            await emailService.SendConfirmationMessage(request.Email, code);

            return new ResponseContract<UserEntity>(user);
        }
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Email).EmailAddress();
            RuleFor(command => command.Password).MinimumLength(8).MaximumLength(24).NotEmpty()
                .Must(password => Regex.IsMatch(password, @"^\S+$")).WithMessage("В пароле не допустимы пробелы");
            RuleFor(command => command.NickName).MinimumLength(2).MaximumLength(30);
            RuleFor(command => command.StudentGroupNumberId).NotEmpty();
        }
    }
}
