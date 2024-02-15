using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Auth.Commands.Login
{
    public record LoginCommand(string Username, string Password) : IRequest<ErrorOr<Unit>>;
}
