using Cimas.Application.Interfaces;
using Cimas.Domain.Cinemas;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Cinemas.Commands.CreateCinema
{
    public class CreateCinemaCommandHandler : IRequestHandler<CreateCinemaCommand, ErrorOr<Cinema>>
    {
        private readonly IUnitOfWork _uow;

        public CreateCinemaCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ErrorOr<Cinema>> Handle(CreateCinemaCommand command, CancellationToken cancellationToken)
        {
            var company = await _uow.CompanyRepository.GetCompanyByUserIdAsync(command.UserId);
            if(company is null)
            {
                throw new InvalidOperationException("User is not linked to any company");
            }

            var cinema = new Cinema()
            {
                Id = Guid.NewGuid(),
                CompanyId = company.Id,
                Name = command.Name,
                Adress = command.Adress
            };

            await _uow.CinemaRepository.AddAsync(cinema);
            await _uow.CompleteAsync();

            return cinema;
        }
    }
}
