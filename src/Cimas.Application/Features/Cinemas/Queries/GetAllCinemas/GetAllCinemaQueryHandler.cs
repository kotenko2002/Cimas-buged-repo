using Cimas.Application.Interfaces;
using Cimas.Domain.Cinemas;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Cinemas.Queries.GetAllCinemas
{
    public class GetAllCinemaQueryHandler : IRequestHandler<GetAllCinemaQuery, ErrorOr<List<Cinema>>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllCinemaQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<ErrorOr<List<Cinema>>> Handle(GetAllCinemaQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
