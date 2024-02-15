using Microsoft.AspNetCore.Identity;

namespace Cimas.Domain.Entities.Users
{
    public class User : IdentityUser
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
