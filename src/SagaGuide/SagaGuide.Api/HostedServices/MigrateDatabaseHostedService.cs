using SagaGuide.Infrastructure.EntityFramework.DataSeeders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SagaGuide.Api.Constants;
using SagaGuide.Api.Controllers;
using SagaGuide.Api.HealthChecks;
using SagaGuide.Api.Queries.Equipment;
using SagaGuide.Api.Queries.Skill;
using SagaGuide.Api.Queries.Technique;
using SagaGuide.Api.Queries.Trait;
using Serilog;
using ILogger = Serilog.ILogger;

namespace SagaGuide.Api.HostedServices;

public class MigrateDatabaseHostedService : BackgroundService
{
    private readonly DatabaseHealthCheck _healthCheck;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly MemoryCacheProvider _memoryCacheProvider;

    public MigrateDatabaseHostedService(IServiceScopeFactory serviceScopeFactory, DatabaseHealthCheck healthCheck, MemoryCacheProvider memoryCacheProvider)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _healthCheck = healthCheck;
        _memoryCacheProvider = memoryCacheProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var log = Log.ForContext<MigrateDatabaseHostedService>();

        var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
        while (await dbContext.Database.CanConnectAsync(stoppingToken) == false)
        {
            log.Error("Can't connect to Gurps Database!");
            //log.Information(dbContext.Database.GetConnectionString());
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }

        log.Information("Creating/Migrating Gurps database.");
        await dbContext.Database.MigrateAsync(stoppingToken);

        log.Information("Database created/migrated");

        log.Information("Starting to seed the database");
        var dataSeeder = scope.ServiceProvider.GetService<IDataSeeder>();
        await dataSeeder!.SeedAsync();
        log.Information("Finished to seed the database");

        _healthCheck.MigrationCompleted = true;

        await InitializeCache(scope, log);
    }

    private async Task InitializeCache(AsyncServiceScope scope, ILogger log)
    {
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        
        log.Information("Initializing equipments cache.");
        var query = new GetEquipmentsQuery(new List<Guid>());
        var result = (await mediator.Send(query, CancellationToken.None)).ToList();
        _memoryCacheProvider.UpdateCacheKey(CacheKeys.GetAllEquipments, result);
        log.Information("Finished initializing equipments cache.");
        
        log.Information("Initializing skills cache.");
        var skillsQuery = new GetSkillsQuery(new List<Guid>());
        var skillsResult = (await mediator.Send(skillsQuery, CancellationToken.None)).ToList();
        _memoryCacheProvider.UpdateCacheKey(CacheKeys.GetAllSkills, skillsResult);
        log.Information("Finished initializing skills cache.");
        
        log.Information("Initializing techniques cache.");
        var techniquesQuery = new GetTechniquesQuery(new List<Guid>());
        var techniquesResult = (await mediator.Send(techniquesQuery, CancellationToken.None)).ToList();
        _memoryCacheProvider.UpdateCacheKey(CacheKeys.GetAllTechniques, techniquesResult);
        log.Information("Finished initializing techniques cache.");

        log.Information("Initializing traits cache.");
        var traitsQuery = new GetTraitsQuery(new List<Guid>());
        var traitsResult = (await mediator.Send(traitsQuery, CancellationToken.None)).ToList();
        _memoryCacheProvider.UpdateCacheKey(CacheKeys.GetAllTraits, traitsResult);
        log.Information("Finished initializing traits cache.");
        log.Information("Finished initializing cache.");
    }
}