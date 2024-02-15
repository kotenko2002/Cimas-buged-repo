using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Cimas.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<Unit>>
    {
        private readonly UserManager<User> _userManager;

        public RegisterCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ErrorOr<Unit>> Handle(RegisterCommand command, CancellationToken cancellationToken)
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

            // add check does role exists
            //await _userManager.AddToRoleAsync(user, command.Role);

            return Unit.Value;
        }
    }
}
