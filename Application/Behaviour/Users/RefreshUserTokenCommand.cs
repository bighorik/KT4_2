using Application.Dto;
using Application.Interfaces.Repositories.BaseRepositories;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Users
{
    public class RefreshUserTokenCommand : IRequest<ResponseContract<LoginDto>>
    {
        public required string RefreshToken { get; set; }
        public required string UserAgent { get; set; }
    }

    public class RefreshUserTokenCommandHandler(
        ICryptoService cryptoService,
        ITokenService tokenService,
        IRefreshTokenRepository tokenRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<RefreshUserTokenCommand, ResponseContract<LoginDto>>
    {
        public async Task<ResponseContract<LoginDto>> Handle(RefreshUserTokenCommand request, CancellationToken cancellationToken)
        {
            string refreshHash = cryptoService.GetRefreshTokenHash(request.RefreshToken);
            string userAgentHash = cryptoService.GetUserAgentHash(request.UserAgent);

            RefreshTokenEntity? tokenEntity = await tokenRepository.GetActiveAsync(refreshHash, userAgentHash);
            if (tokenEntity == null)
                return new ResponseContract<LoginDto>(ErrorCodes.InvalidRefreshToken);

            await tokenRepository.MarkAsUsedAsync(refreshHash);

            (string access, string refresh) = tokenService.GenerateTokens(tokenEntity.User);


            string newRefreshHash = cryptoService.GetRefreshTokenHash(refresh);

            RefreshTokenEntity newTokenEntity = new()
            {
                Id = Guid.NewGuid(),
                UserId = tokenEntity.UserId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMonths(1),
                RevokedAt = null,
                TokenHash = newRefreshHash,
                UserAgentHash = userAgentHash,
            };

            await tokenRepository.CreateAsync(newTokenEntity);
            await unitOfWork.SaveChangesAsync();

            LoginDto result = new() { AccessToken = access, RefreshToken = refresh };
            return new ResponseContract<LoginDto>(result);
        }
    }

    public class RefreshUserTokenCommandValidator : AbstractValidator<RefreshUserTokenCommand>
    {
        public RefreshUserTokenCommandValidator()
        {
            RuleFor(command => command.RefreshToken).NotEmpty();
            RuleFor(command => command.UserAgent).NotEmpty();
        }
    }
}
