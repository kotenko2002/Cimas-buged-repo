using Cimas.Application.Interfaces.Uow;
using Cimas.Infrastructure.Common;
using Cimas.Infrastructure.Uow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cimas.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<CimasDbContext>(options =>
                options.UseSqlServer("Data Source=DESKTOP-OF64QVK\\SQLEXPRESS;Initial Catalog=Cimasdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
