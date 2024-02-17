using Cimas.Infrastructure.Common;
using Cimas.Domain.Companies;
using Cimas.Application.Interfaces;

namespace Cimas.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(CimasDbContext context) : base(context)
        {
        }
    }
}
