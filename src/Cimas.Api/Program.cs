using Cimas.Application;
using Cimas.Infrastructure;

namespace Cimas.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddProblemDetails(); //?

            builder.Services
               .AddApplication()
               .AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            app.UseExceptionHandler(); //?

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}