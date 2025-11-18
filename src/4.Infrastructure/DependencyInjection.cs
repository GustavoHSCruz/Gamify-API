using Domain.Shared.Interfaces;
using Infrastructure.Contexts;
using Infrastructure.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddContextInjections(this IServiceCollection services, IConfiguration configuration)
        {
            string db = configuration.GetConnectionString("Database");

            services.AddDbContext<ReadContext>(options => options.UseNpgsql(db));
            services.AddDbContext<WriteContext>(options => options.UseNpgsql(db));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IReadRepository, ReadRepository>();
            services.AddScoped<IWriteRepository, WriteRepository>();
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            services.AddScoped<IJwtService, JwtService>();

            var rabbitSection = configuration.GetSection("RabbitMQ");
            var rabbitHost = rabbitSection.GetValue<string>("Host");
            var rabbitExchange = rabbitSection.GetValue<string>("Exchange");
        }
    }
}
