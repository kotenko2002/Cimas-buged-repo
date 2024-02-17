using Microsoft.AspNetCore.Identity;

namespace Cimas.Domain.Users
{
    public class User : IdentityUser
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
