﻿using Cimas.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Cimas.Domain.Auth;
using ErrorOr;

namespace Cimas.Infrastructure.Auth
{
    public class JwtTokensService : IJwtTokensService
    {
        private readonly JwtConfig _config;

        public JwtTokensService(IOptions<JwtConfig> jwtOptions)
        {
            _config = jwtOptions.Value;
        }

        public TokensPair GenerateTokens(List<Claim> authClaims)
        {
            JwtSecurityToken accessToken = GenerateAccessToken(authClaims);
            string refreshToken = GenerateRefreshToken();

            return new TokensPair()
            {
                AccessToken = new Token()
                {
                    Value = new JwtSecurityTokenHandler().WriteToken(accessToken),
                    ValidTo = accessToken.ValidTo
                },
                RefreshToken = new Token()
                {
                    Value = refreshToken,
                    ValidTo = DateTime.Now.AddDays(_config.RefreshTokenValidityInDays)
                }
            };
        }

        public ErrorOr<ClaimsPrincipal> GetPrincipalFromExpiredToken(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret)),
                ValidateLifetime = false
            };

            ClaimsPrincipal principal = new JwtSecurityTokenHandler()
                .ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return Error.Failure(description: "Invalid access or refresh tokens");
            }

            return principal;
        }

        private JwtSecurityToken GenerateAccessToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret));

            return new JwtSecurityToken(
                issuer: _config.ValidIssuer,
                audience: _config.ValidAudience,
                expires: DateTime.Now.AddMinutes(_config.TokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
