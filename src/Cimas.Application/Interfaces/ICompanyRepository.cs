using Cimas.Domain.Companies;

namespace Cimas.Application.Interfaces
{
    public interface ICompanyRepository : IBaseRepository<Company>
    {
        Task<Company> GetCompanyByUserIdAsync(Guid userId);
    }
}
