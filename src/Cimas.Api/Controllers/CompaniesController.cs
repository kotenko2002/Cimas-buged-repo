using Cimas.Application.Features.Companies.Commands.CreateCompany;
using Cimas.Contracts.Companies;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cimas.Api.Controllers
{
    [Route("[controller]")]
    public class CompaniesController : BaseController
    {
        public CompaniesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyRequest request)
        {
            var command = new CreateCompanyCommand(request.Name);

            var createCompanyResult = await _mediator.Send(command);

            return createCompanyResult.Match(
                Ok,
                Problem
            );
        }
    }
}
