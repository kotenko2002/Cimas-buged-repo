using Cimas.Application.Interfaces;
using Cimas.Domain.Users;
using Cimas.Infrastructure.Auth;
using Cimas.Infrastructure.Common;
using Cimas.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cimas.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDatabaseServices(configuration)
                .AddScoped<IUnitOfWork, UnitOfWork>();

            services
                .Configure<JwtConfig>(configuration.GetSection(JwtConfig.Section))
                .AddScoped<IJwtTokensService, JwtTokensService>();

            return services;
        }

        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CimasDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<CimasDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
