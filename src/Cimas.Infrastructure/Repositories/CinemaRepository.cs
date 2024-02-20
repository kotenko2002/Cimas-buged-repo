using Cimas.Application.Interfaces;
using Cimas.Domain.Cinemas;
using Cimas.Infrastructure.Common;

namespace Cimas.Infrastructure.Repositories
{
    public class CinemaRepository : BaseRepository<Cinema>, ICinemaRepository
    {
        public CinemaRepository(CimasDbContext context) : base(context) {}
    }
}
