using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SagaGuide.Api.HostedServices;
using SagaGuide.Infrastructure.EntityFramework;
using SagaGuide.Infrastructure.EntityFramework.DataSeeders;
using SagaGuide.TestData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Npgsql;
using Respawn;
using Serilog;
using Testcontainers.PostgreSql;
using Xunit.Abstractions;
using ILogger = Serilog.ILogger;

namespace SagaGuide.IntegrationTests.Setup;

public class ITestOutputHelperProvider
{
    public void ChangeTestOutputHelper(ITestOutputHelper testOutputHelper)
    {
        TestOutputHelper = testOutputHelper;
        XUnitLoggerProvider = new XUnitLoggerProvider(TestOutputHelper);
    }

    public ITestOutputHelper TestOutputHelper { get; private set; } = null!;

    internal XUnitLoggerProvider XUnitLoggerProvider { get; private set; } = null!;
}

public class BaseWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    public IConfiguration Configuration;
    public HttpClient Client { get; private set; } = null!;
    
    public ITestOutputHelperProvider TestOutputHelperProvider { get; init; } = new ITestOutputHelperProvider();
    private readonly PostgreSqlContainer  _dbContainer = null!;
    private DbConnection _dbConnection = null!;
    private Respawner _respawner = null!;


    public BaseWebApplicationFactory()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
        Configuration ??= new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.Testing.json", false, false)
            .Build();
        
        _dbContainer = new PostgreSqlBuilder().WithDatabase("testDb").WithUsername("dbUser").WithPassword("dbPas$word!").WithPrivileged(true).Build();
    }
    
    private static ILogger CreateSerilogLogger()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, false).Build();

        return new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .CreateLogger();
    }

    public void ChangeTestOutputHelper(ITestOutputHelper testOutputHelper) => TestOutputHelperProvider.ChangeTestOutputHelper(testOutputHelper);

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        
        //Create and seed DB
        Client = CreateClient();
        { 
            await using var scope = Services.CreateAsyncScope(); 
            await using var context = scope.ServiceProvider.GetService<GurpsDbContext>(); 
            await context.Database.EnsureDeletedAsync(); 
            await context.Database.EnsureCreatedAsync(); 
            var dataSeeder = scope.ServiceProvider.GetService<IDataSeeder>(); 
            await dataSeeder!.SeedAsync();
       }
        var str = _dbContainer.GetConnectionString();
        _dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());
        await _dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions()
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = new[]{"public"},
        });
    }
    
    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
        await using var scope = Services.CreateAsyncScope();
        var dataSeeder = scope.ServiceProvider.GetService<IDataSeeder>();;
        await dataSeeder!.SeedAsync();
    }
    
    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .UseSerilog(CreateSerilogLogger())
            .ConfigureServices(services =>
            {
                services.RemoveAll<DbContextOptions<GurpsDbContext>>();
                services.RemoveAll<GurpsDbContext>();
                services.AddDbContext<DbContext, GurpsDbContext>(options => { options.UseNpgsql(_dbContainer.GetConnectionString()); });
                
                var descriptor = services.Single(s => s.ImplementationType == typeof(MigrateDatabaseHostedService));
                services.Remove(descriptor);
                
                RemoveServiceBinding(services, typeof(IServiceScopeFactory));
                builder.ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.Services.AddSingleton<ILoggerProvider>(serviceProvider => TestOutputHelperProvider.XUnitLoggerProvider);
                });
            });
        
        builder.ConfigureAppConfiguration((context, b) =>
        {
            b.AddInMemoryCollection(
                new Dictionary<string, string>
                {
                    ["Auth:JwtPublicKey"] = "",
                    ["Auth:ExternalIssuer"] = TestConstants.TestIssuer,
                    ["Auth:InternalIssuer"] = TestConstants.TestIssuer,
                    ["Auth:TestKey"] = TestConstants.TestKey
                }!);
        });
        builder.ConfigureLogging((hostingContext, logging) =>
        {
            //ToDO: research tests output
            //logging.AddXunit(output);
        });
    }
    
    protected void RemoveServiceBinding(IServiceCollection services, Type service)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == service);
        if (descriptor != null) services.Remove(descriptor);
    }
}