using Cimas.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cimas.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public RolesController(RoleManager<IdentityRole<Guid>> roleManager)
            => _roleManager = roleManager;

        [HttpPost]
        public async Task AddRolesToDb() 
        {
            foreach (var role in Roles.GetRoles())
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }
    }
}
