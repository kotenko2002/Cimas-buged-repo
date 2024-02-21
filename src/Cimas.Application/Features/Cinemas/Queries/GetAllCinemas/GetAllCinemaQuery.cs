using Cimas.Domain.Cinemas;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Cinemas.Queries.GetAllCinemas
{
    public record GetAllCinemaQuery(
        Guid UserId
    ) : IRequest<ErrorOr<List<Cinema>>>;
}
