using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(string Username, string Password, string Role) : IRequest<ErrorOr<Unit>>;
}
