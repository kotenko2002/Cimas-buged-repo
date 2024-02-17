using Cimas.Domain.Users;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Cimas.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<Success>>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterCommandHandler(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ErrorOr<Success>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            User existsUser = await _userManager.FindByNameAsync(command.Username);
            if (existsUser is not null)
            {
                return Error.Conflict(description: "User with such username is already exists");
            }

            var user = new User()
            {
                UserName = command.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            IdentityResult result = await _userManager.CreateAsync(user, command.Password);
            if (!result.Succeeded)
            {
                return result.Errors
                    .Select(e => Error.Validation(
                        code: e.Code,
                        description: e.Description))
                    .ToList();
            }

            if(!await _roleManager.RoleExistsAsync(command.Role))
            {
                throw new InvalidOperationException("Role doesn't exists");
            }

            await _userManager.AddToRoleAsync(user, command.Role);

            return Result.Success;
        }
    }
}
