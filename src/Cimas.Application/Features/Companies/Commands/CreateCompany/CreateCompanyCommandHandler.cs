﻿using Cimas.Application.Interfaces.Uow;
using Cimas.Domain.Entities.Companies;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, ErrorOr<Company>>
    {
        private readonly IUnitOfWork _uow;

        public CreateCompanyCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ErrorOr<Company>> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
        {
            var company = new Company()
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
            };

            await _uow.CompanyRepository.AddAsync(company);
            await _uow.CompleteAsync();

            return company;
        }
    }
}
