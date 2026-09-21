using Application.Behaviour.Users;
using Application.Dto;
using Application.Utils;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Fto.User;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/authorization")]
    public class AuthorizationController(IMediator mediator) : ControllerBase
    {
        [HttpGet("me")]
        public async Task<ResponseContract<UserFto>> GetMe([FromBody] UserToCreateFto userDto)
        {
            ResponseContract<UserEntity> result = await mediator.Send(new GetUserByIdQuery()
            {
                UserId = User.UserId(),
            });

            return result.ToDto<UserFto>();
        }

        [HttpPost("register")]
        public async Task<ResponseContract<UserFto>> Register([FromBody] UserToCreateFto userDto)
        {
            ResponseContract<UserEntity> result = await mediator.Send(userDto.Adapt<CreateUserCommand>());

            return result.ToDto<UserFto>();
        }

        [HttpPost("login")]
        public async Task<ResponseContract> Login([FromBody] LoginDto loginDto)
        {
            ResponseContract<LoginDto> result = await mediator.Send(loginDto.Adapt<LoginUserCommand>());

            if (result.Ok)
            {
                Response.Cookies.Append("accessToken", result.Data!.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(15),
                    Path = "/"
                });

                Response.Cookies.Append("refreshToken", result.Data!.RefreshToken, new CookieOptions
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

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken)
                || string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized();
            }

            ResponseContract<LoginDto> result = await mediator.Send(
                new RefreshUserTokenCommand()
                {
                    RefreshToken = refreshToken,
                });

            if (result.Ok)
            {
                Response.Cookies.Append("accessToken", result.Data!.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(15),
                    Path = "/"
                });

                Response.Cookies.Append("refreshToken", result.Data!.RefreshToken, new CookieOptions
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
                    UserId = User.UserId(),
                });

            Response.Cookies.Delete("accessToken", new CookieOptions { Path = "/" });
            Response.Cookies.Delete("refreshToken", new CookieOptions { Path = "/" });

            return Ok();
        }
    }
}


