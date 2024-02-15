using Cimas.Application.Interfaces.Repositories;
using Cimas.Application.Interfaces.Uow;
using Cimas.Infrastructure.Common;
using Cimas.Infrastructure.Repositories;

namespace Cimas.Infrastructure.Uow
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly CimasDbContext _context;

        public ICompanyRepository CompanyRepository { get; }

        public UnitOfWork(CimasDbContext context)
        {
            _context = context;

            CompanyRepository = new CompanyRepository(_context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
