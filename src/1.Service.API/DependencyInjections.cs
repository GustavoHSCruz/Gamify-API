using Application.Common;
using Domain.Core.Mapper;
using FluentValidation;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Service.API.Interfaces;
using Service.API.Localization;
using System.Reflection;
using System.Text;

namespace Service.API
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddDependencyInjections(this IServiceCollection services, IConfiguration configuration)
        {
            var appAssembly = Assembly.Load("Application");
            var domainCoreAssembly = Assembly.Load("Domain.Core");
            var thisAssembly = Assembly.GetExecutingAssembly();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddValidatorsFromAssembly(appAssembly);
            services.AddHttpContextAccessor();

            // Register MediatR
            services.AddMediatR(cfg =>
            {
                cfg.LicenseKey = configuration.GetValue<string>("MediatRAutoMapper:LicenseKey");
                cfg.RegisterServicesFromAssemblies(appAssembly, domainCoreAssembly, thisAssembly);
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.LicenseKey = configuration.GetValue<string>("MediatRAutoMapper:LicenseKey");
            }, typeof(MappedEntities));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestBehavior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(QueryRequestBehavior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<IErrorMessageProvider, ErrorLocalizer>();

            services.AddContextInjections(configuration);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        var cfg = configuration.GetSection("Jwt");
                        var keyBytes = Encoding.UTF8.GetBytes(cfg["Key"]!);

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = cfg["Issuer"],
                            ValidateAudience = true,
                            ValidAudience = cfg["Audience"],
                            ValidateLifetime = true,
                            RequireExpirationTime = true,
                            ClockSkew = TimeSpan.FromMinutes(1),
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
                        };

                        // Opcional: respostas mais “limpas” de 401/403
                        options.Events = new JwtBearerEvents
                        {
                            OnChallenge = context =>
                            {
                                // Evita o desafio padrão e devolve 401 simples
                                context.HandleResponse();
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                return context.Response.WriteAsync("Unauthorized");
                            },
                            OnForbidden = context =>
                            {
                                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                                return context.Response.WriteAsync("Forbidden");
                            }
                        };
                    });

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                // Exemplo de policy por role/claim
                //options.AddPolicy("AdminOnly", p => p.RequireRole("admin"));
            });

            return services;
        }
    }
}
