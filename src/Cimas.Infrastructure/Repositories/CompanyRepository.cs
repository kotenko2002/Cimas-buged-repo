using Cimas.Domain.Entities.Companies;
using Cimas.Application.Interfaces.Repositories;
using Cimas.Infrastructure.Common;

namespace Cimas.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(CimasDbContext context) : base(context)
        {
        }
    }
}
