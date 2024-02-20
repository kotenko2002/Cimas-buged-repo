﻿using Cimas.Application.Interfaces;
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
        private readonly ICustomUserManager _userManager;

        public DeleteCinemaCommandHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<Success>> Handle(DeleteCinemaCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId.ToString());
            if(user is null)
            {
                return Error.NotFound(description: "User with such id does not exist");
            }

            if (!await _userManager.IsInRoleAsync(user, Roles.Owner))
            {
                return Error.Unauthorized(description: "You do not have the necessary permissions to perform this action");
            }

            return Result.Success;
        }
    }
}