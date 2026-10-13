using Application.Behaviour.Users;
using Application.Dto;
using Application.Interfaces.Services;
using Application.Utils;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Fto.Authorization;
using WebApi.Fto.User;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/authorization")]
    public class AuthorizationController(IMediator mediator, ITokenService tokenService) : ControllerBase
    {
        [Authorize]
        [HttpGet("me")]
        public async Task<ResponseContract<UserFto>> GetMe()
        {
            ResponseContract<UserEntity> result = await mediator.Send(new GetUserByIdQuery()
            {
                UserId = User.GetId(),
            });

            return result.ToDto<UserFto>();
        }

        [HttpPost("register")]
        public async Task<ResponseContract<UserFto>> Register([FromBody] UserToCreateFto userDto)
        {
            ResponseContract<UserEntity> result = await mediator.Send(userDto.Adapt<CreateUserCommand>());

            return result.ToDto<UserFto>();
        }

        [HttpPost("confirm/{code}")]
        public Task<ResponseContract> Register([FromRoute] string code)
        {
            ConfirmUserEmailCommand command = new() { Code = code };

            return mediator.Send(command);
        }

        [HttpPost("login")]
        public async Task<ResponseContract> Login([FromBody] LoginFto loginDto)
        {
            LoginUserCommand command = loginDto.Adapt<LoginUserCommand>();
            command.UserAgent = Request.Headers.UserAgent!;
            ResponseContract<LoginDto> result = await mediator.Send(command);

            if (result.Ok)
            {
                Response.Cookies.Append("access-token", result.Data!.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(15),
                    Path = "/"
                });

                Response.Cookies.Append("refresh-token", result.Data!.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays(7),
                    Path = "/"
                });
            }

            return result.Adapt<ResponseContract>();
        }

        public record Res(string Access, string Refresh);

        [HttpPost("fake-login")]
        public async Task<Res> FakeLogin([FromBody] LoginFto loginDto)
        {

            UserEntity user = new()
            {
                Id = Guid.Parse("73819CFB-F51B-4C99-801F-A30C65C23416"),
                Nickname = "bighorik",
                Email = "bighorik@ya.ru",
                PasswordHash = "",
                StudentGroupNumberId = Guid.NewGuid(),
            };

            var a = tokenService.GenerateTokens(user);

            return new(a.AccessToken, a.RefreshToken);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            if (!Request.Cookies.TryGetValue("refresh-token", out var refreshToken)
                || string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized();
            }

            ResponseContract<LoginDto> result = await mediator.Send(
                new RefreshUserTokenCommand()
                {
                    RefreshToken = refreshToken,
                    UserAgent = Request.Headers.UserAgent!
                });

            if (result.Ok)
            {
                Response.Cookies.Append("access-token", result.Data!.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(15),
                    Path = "/"
                });

                Response.Cookies.Append("refresh-token", result.Data!.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays(7),
                    Path = "/"
                });

                return Ok();
            }

            return Unauthorized();
        }

        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            await mediator.Send(
                new LogoutUserCommand()
                {
                    UserId = User.GetId(),
                });

            Response.Cookies.Delete("accessToken", new CookieOptions { Path = "/" });
            Response.Cookies.Delete("refreshToken", new CookieOptions { Path = "/" });

            return Ok();
        }
    }
}


