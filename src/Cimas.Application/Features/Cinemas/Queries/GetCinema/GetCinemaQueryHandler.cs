using Cimas.Application.Interfaces;
using Cimas.Domain.Cinemas;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Cimas.Application.Features.Cinemas.Queries.GetCinema
{
    public class GetCinemaQueryHandler : IRequestHandler<GetCinemaQuery, ErrorOr<Cinema>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomUserManager _userManager;

        public GetCinemaQueryHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<Cinema>> Handle(GetCinemaQuery query, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(query.UserId.ToString());
            if (user is null)
            {
                return Error.NotFound(description: "User with such id does not exist");
            }

            return await _uow.CinemaRepository.GetByIdAsync(query.CinemaId);
        }
    }
}
