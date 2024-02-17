using Cimas.Application.Interfaces;
using Cimas.Domain.Auth;
using Cimas.Domain.Users;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Cimas.Application.Features.Auth.Commands.RefreshTokens
{
    internal class RefreshTokensHandler : IRequestHandler<RefreshTokensCommand, ErrorOr<TokensPair>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokensService _jwtTokensService;

        public RefreshTokensHandler(
            UserManager<User> userManager,
            IJwtTokensService jwtTokensService)
        {
            _userManager = userManager;
            _jwtTokensService = jwtTokensService;
        }

        public async Task<ErrorOr<TokensPair>> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
        {
            string accessToken = request.AccessToken;
            string refreshToken = request.RefreshToken;

            ErrorOr<ClaimsPrincipal> getPrincipalResult = _jwtTokensService.GetPrincipalFromExpiredToken(accessToken);
            if(getPrincipalResult.IsError)
            {
                return getPrincipalResult.Errors;
            }

            string username = getPrincipalResult.Value.Identity.Name;
            User user = await _userManager.FindByNameAsync(username);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return Error.Failure(description: "Invalid access token or refresh token");
            }

            List<Claim> authClaims = getPrincipalResult.Value.Claims.ToList();
            TokensPair tokens = _jwtTokensService.GenerateTokens(authClaims);

            user.RefreshToken = tokens.RefreshToken.Value;
            user.RefreshTokenExpiryTime = tokens.RefreshToken.ValidTo;
            await _userManager.UpdateAsync(user);

            return tokens;
        }
    }
}
