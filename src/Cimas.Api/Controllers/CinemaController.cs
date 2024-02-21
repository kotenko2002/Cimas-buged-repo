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
using Cimas.Api.Common.Extensions;
using Mapster;

namespace Cimas.Api.Controllers
{
    [Route("cinemas"), Authorize]
    public class CinemaController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CinemaController(
            IMediator mediator,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
        ) : base(mediator, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
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

        [HttpGet("{cinemaId}")]
        public async Task<IActionResult> GetCinema(Guid cinemaId)
        {
            var userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if(userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            var command = new GetCinemaQuery(userIdResult.Value, cinemaId);

            var getCinemaResult = await _mediator.Send(command);

            return getCinemaResult.Match(
                Ok,
                Problem
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCinema()
        {
            var userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }
            var command = new GetAllCinemaQuery(userIdResult.Value);

            var getCinemasResult = await _mediator.Send(command);

            return getCinemasResult.Match(
                Ok,
                Problem
            );
        }

        [HttpPut("{cinemaId}")]
        public async Task<IActionResult> UpdateCinema(Guid cinemaId, UpdateCinemaRequest request)
        {
            var userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
            if (userIdResult.IsError)
            {
                return Problem(userIdResult.Errors);
            }

            var command = (userIdResult.Value, cinemaId, request).Adapt<UpdateCinemaCommand>();

            var updateCinemaResult = await _mediator.Send(command);

            return updateCinemaResult.Match(
                res => Ok(),
                Problem
            );
        }

        //[HttpDelete("{cinemaId}")]
        //public async Task<IActionResult> DeleteCinema(Guid cinemaId)
        //{
        //    var userIdResult = _httpContextAccessor.HttpContext.User.GetUserId();
        //    if (userIdResult.IsError)
        //    {
        //        return Problem(userIdResult.Errors);
        //    }

        //    var command = new DeleteCinemaCommand(Guid.Parse("75002AA9-9881-40FE-E31F-08DC318EE53E"), cinemaId);// ???
        //    command.CinemaId = cinemaId;
        //    command.UserId = userIdResult.Value;

        //    var deleteCinemaResult = await _mediator.Send(command);

        //    return deleteCinemaResult.Match(
        //        res => Ok(),
        //        Problem
        //    );
        //}
    }
}
