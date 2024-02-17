using Cimas.Domain.Users;
using System.IdentityModel.Tokens.Jwt;

namespace Cimas.Domain.Auth
{
    public class TokensPair
    {
        public Token AccessToken { get; set; }
        public Token RefreshToken { get; set; }
    }
}
