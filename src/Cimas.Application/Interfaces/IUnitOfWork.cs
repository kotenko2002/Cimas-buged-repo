namespace Cimas.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICompanyRepository CompanyRepository { get; }

        Task CompleteAsync();
    }
}
