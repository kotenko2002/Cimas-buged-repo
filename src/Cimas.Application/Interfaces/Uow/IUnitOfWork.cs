using Cimas.Application.Interfaces.Repositories;

namespace Cimas.Application.Interfaces.Uow
{
    public interface IUnitOfWork
    {
        ICompanyRepository CompanyRepository { get; }

        Task CompleteAsync();
    }
}
