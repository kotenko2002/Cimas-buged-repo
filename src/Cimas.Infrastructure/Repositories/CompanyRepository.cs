﻿using Cimas.Infrastructure.Common;
using Cimas.Domain.Companies;
using Cimas.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cimas.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(CimasDbContext context) : base(context) {}

        public async Task<Company> GetCompanyByUserIdAsync(Guid userId)
        {
            return await Sourse
                .FirstOrDefaultAsync(company => company.Id == userId);
        }
    }
}
