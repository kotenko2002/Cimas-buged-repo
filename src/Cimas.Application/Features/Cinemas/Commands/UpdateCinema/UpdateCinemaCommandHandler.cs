using Cimas.Application.Interfaces;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Cinemas.Commands.UpdateCinema
{
    public class UpdateCinemaCommandHandler : IRequestHandler<UpdateCinemaCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _uow;

        public UpdateCinemaCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<ErrorOr<Success>> Handle(UpdateCinemaCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
