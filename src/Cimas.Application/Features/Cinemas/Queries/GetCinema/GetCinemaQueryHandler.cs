using Cimas.Application.Interfaces;
using Cimas.Domain.Cinemas;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Cinemas.Queries.GetCinema
{
    public class GetCinemaQueryHandler : IRequestHandler<GetCinemaQuery, ErrorOr<Cinema>>
    {
        private readonly IUnitOfWork _uow;

        public GetCinemaQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<ErrorOr<Cinema>> Handle(GetCinemaQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
