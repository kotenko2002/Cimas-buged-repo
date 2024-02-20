using Mapster;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Cimas.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();

            services
                .AddHttpContextAccessor()
                .AddEndpointsApiExplorer()
                .AddSwagger()
                .AddProblemDetails()
                .AddMapster();

            return services;
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"Bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }
    }
}
