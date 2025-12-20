using System.Reflection;
using Application.Common;
using AutoMapper;
using Bogus;
using Domain.Core.Mapper;
using Domain.Shared.Interfaces;
using FluentValidation;
using Infrastructure.Contexts;
using Infrastructure.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Test.Common;

public abstract class IntegrationTestCommand : MemoryDatabase
{
    protected readonly IMediator _mediator;
    protected WriteRepository _repository;
    protected ReadRepository _readRepository;
    protected UnitOfWork _uow;
    // protected IMapper _mapper;
    protected readonly Faker _faker;

    
    //TODO: ONE DAY I WILL REFACTOR THIS
    protected IntegrationTestCommand()
    {
        _faker = new Faker();
        
        var appAssembly = Assembly.Load("Application");
        var domainCoreAssembly = Assembly.Load("Domain.Core");
        // var thisAssembly = Assembly.GetExecutingAssembly();
        var thisAssembly = Assembly.GetAssembly(typeof(Program));

        var services = new ServiceCollection();
        
        services.AddLogging();
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(appAssembly, domainCoreAssembly, thisAssembly); });
        services.AddValidatorsFromAssembly(appAssembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        services.AddScoped<IWriteRepository>(x => new WriteRepository(_writeContext));
        services.AddScoped<IReadRepository>(x => new ReadRepository(_readContext));
        services.AddScoped<IUnitOfWork>(x => new UnitOfWork(_writeContext));
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(MappedEntities).Assembly);
        });

        services.AddSingleton<IOptions<JwtOptions>>(sp =>
        {
            var opts = new JwtOptions()
            {
                Issuer = "TestIssuer",
                Audience = "TestAudience",
                Key = _faker.Random.String2(50),
                ExpirationMinutes = 60
            };

            return Options.Create(opts);
        });
        services.AddScoped<IJwtService, JwtService>();
        services.AddHttpContextAccessor();
        

        var serviceProvider = services.BuildServiceProvider();
        _mediator = serviceProvider.GetRequiredService<IMediator>();

        var seed = new Random().Next(0, int.MaxValue);
        Console.WriteLine($"SEED: {seed}");
        Randomizer.Seed = new Random(seed);
    }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        var mapperConfig = new MapperConfiguration(cfg => { cfg.AddMaps(typeof(MappedEntities).Assembly); }, NullLoggerFactory.Instance);

        _repository = new WriteRepository(_writeContext);
        _readRepository = new ReadRepository(_readContext);
        _uow = new UnitOfWork(_writeContext);
    }
}