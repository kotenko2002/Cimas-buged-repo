using Cimas.Application.Interfaces;
using Cimas.Domain.Users;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Cimas.Application.Features.Cinemas.Commands.DeleteCinema
{
    public class DeleteCinemaCommandHandler : IRequestHandler<DeleteCinemaCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;

        public DeleteCinemaCommandHandler(
            IUnitOfWork uow,
            UserManager<User> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<Success>> Handle(DeleteCinemaCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.CinemaId.ToString());
            if(user is null)
            {
                return Error.NotFound(description: "User with such id does not exist");
            }

            if (!await _userManager.IsInRoleAsync(user, Roles.Owner))
            {
                return Error.Unauthorized(description: "You do not have the necessary permissions to perform this action");
            }


        }
    }
}
