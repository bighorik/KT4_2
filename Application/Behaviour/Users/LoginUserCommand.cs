using Application.Dto;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.BaseRepositories;
using Application.Interfaces.Services;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;
using System.Text.RegularExpressions;

namespace Application.Behaviour.Users
{
    public class LoginUserCommand : IRequest<ResponseContract<LoginDto>>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string UserAgent { get; set; }
    }

    public class LoginUserCommandHandler(
        ICryptoService cryptoService,
        IUserRepository userRepository,
        ITokenService tokenService,
        IRefreshTokenRepository tokenRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<LoginUserCommand, ResponseContract<LoginDto>>
    {
        public async Task<ResponseContract<LoginDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {

            UserEntity? user = await userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                return new ResponseContract<LoginDto>(ErrorCodes.UserNotFound);

            if (!user.EmailConfirmed)
                return new ResponseContract<LoginDto>(ErrorCodes.EmailNotConfirmed);


            if (!cryptoService.VerifyPassword(request.Password, user.PasswordHash))
                return new ResponseContract<LoginDto>(ErrorCodes.PasswordNotCorrect);

            (string access, string refresh) = tokenService.GenerateTokens(user);

            string refreshHash = cryptoService.GetRefreshTokenHash(refresh);
            string userAgentHash = cryptoService.GetUserAgentHash(request.UserAgent);

            RefreshTokenEntity tokenEntity = new()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMonths(1),
                RevokedAt = null,
                TokenHash = refreshHash,
                UserAgentHash = userAgentHash,
            };

            await tokenRepository.CreateAsync(tokenEntity);
            await unitOfWork.SaveChangesAsync();

            LoginDto result = new() { AccessToken = access, RefreshToken = refresh };
            return new ResponseContract<LoginDto>(result);
        }
    }

    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(command => command.Email).EmailAddress();
            RuleFor(command => command.Password).MinimumLength(8).MaximumLength(24).NotEmpty()
                .Must(password => Regex.IsMatch(password, @"^\S+$")).WithMessage("В пароле не допустимы пробелы");
            RuleFor(command => command.UserAgent).NotEmpty();
        }
    }
}
