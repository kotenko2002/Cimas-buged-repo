using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using MediatR;
using Cimas.Contracts.Cinemas;
using Cimas.Application.Features.Cinemas.Commands.CreateCinema;
using Cimas.Application.Features.Cinemas.Queries.GetCinema;
using Cimas.Application.Features.Cinemas.Queries.GetAllCinemas;
using Cimas.Application.Features.Cinemas.Commands.UpdateCinema;
using Cimas.Application.Features.Cinemas.Commands.DeleteCinema;

namespace Cimas.Api.Controllers
{
    [Route("cinemas"), Authorize]
    public class CinemaController : BaseController
    {
        public CinemaController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateCinema(CreateCinemaRequest request)
        {
            var command = _mapper.Map<CreateCinemaCommand>(request);

            var createCinemaResult = await _mediator.Send(command);

            return createCinemaResult.Match(
                Ok,
                Problem
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCinema(int id)
        {
            var command = new GetCinemaQuery { Id = id };

            var getCinemaResult = await _mediator.Send(command);

            return getCinemaResult.Match(
                Ok,
                Problem
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCinema()
        {
            var command = new GetAllCinemaQuery();

            var getCinemasResult = await _mediator.Send(command);

            return getCinemasResult.Match(
                Ok,
                Problem
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCinema(int id, UpdateCinemaRequest request)
        {
            var command = _mapper.Map<UpdateCinemaCommand>(request);
            command.Id = id;

            var updateCinemaResult = await _mediator.Send(command);

            return updateCinemaResult.Match(
                Ok,
                Problem
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCinema(int id)
        {
            var command = new DeleteCinemaCommand { Id = id };

            var deleteCinemaResult = await _mediator.Send(command);

            return deleteCinemaResult.Match(
                Ok,
                Problem
            );
        }
    }
}
