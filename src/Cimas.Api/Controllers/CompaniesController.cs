using Cimas.Application.Features.Companies.Commands.CreateCompany;
using Cimas.Contracts.Companies;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cimas.Api.Controllers
{
    [Route("[controller]")]
    public class CompaniesController : BaseController
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGym(CreateCompanyRequest request)
        {
            var command = new CreateCompanyCommand(request.Name);

            var createGymResult = await _mediator.Send(command);

            return createGymResult.Match(
                Ok,
                Problem);
        }
    }
}
