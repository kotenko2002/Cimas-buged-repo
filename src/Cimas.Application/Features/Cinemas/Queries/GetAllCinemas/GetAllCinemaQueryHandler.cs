using Cimas.Application.Interfaces;
using Cimas.Domain.Cinemas;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Cinemas.Queries.GetAllCinemas
{
    public class GetAllCinemaQueryHandler : IRequestHandler<GetAllCinemaQuery, ErrorOr<List<Cinema>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomUserManager _userManager;

        public GetAllCinemaQueryHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<List<Cinema>>> Handle(GetAllCinemaQuery query, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(query.UserId.ToString());
            if (user is null)
            {
                return Error.NotFound(description: "User with such id does not exist");
            }

            if (user.CompanyId == Guid.Empty)
            {
                return Error.Failure(description: "User is not linked to any company");
            }

            return await _uow.CinemaRepository.GetCinemasByCompanyIdAsync(user.CompanyId);
        }
    }
}
