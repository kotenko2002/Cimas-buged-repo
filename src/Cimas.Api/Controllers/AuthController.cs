using Cimas.Application.Features.Auth.Commands.Login;
using Cimas.Application.Features.Auth.Commands.RefreshTokens;
using Cimas.Application.Features.Auth.Commands.Register;
using Cimas.Contracts.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cimas.Api.Controllers
{
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var command = new RegisterCommand(
                request.Username,
                request.Password,
                request.Role);

            var loginResult = await _mediator.Send(command);

            return loginResult.Match(
                res => Ok(),
                Problem
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var command = new LoginCommand(request.Username, request.Password);

            var loginResult = await _mediator.Send(command);

            return loginResult.Match(
                Ok,
                Problem
            );
        }

        [HttpPost("refresh-tokens")]
        public async Task<IActionResult> RefreshTokens(RefreshTokensRequest request)
        {
            var command = new RefreshTokensCommand(request.AccessToken, request.RefreshToken);

            var refreshTokensResult = await _mediator.Send(command);

            return refreshTokensResult.Match(
                Ok,
                Problem
            );
        }
    }
}
