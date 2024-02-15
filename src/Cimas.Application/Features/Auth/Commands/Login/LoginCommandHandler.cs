using Microsoft.AspNetCore.Identity;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<Unit>>
    {
        private readonly UserManager<User> _userManager;
        public Task<ErrorOr<Unit>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
