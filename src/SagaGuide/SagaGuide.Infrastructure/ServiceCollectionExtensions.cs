using SagaGuide.Core.Domain;
using SagaGuide.Core.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SagaGuide.Infrastructure.EntityFramework;
using SagaGuide.Infrastructure.EntityFramework.Configuration;
using SagaGuide.Infrastructure.EntityFramework.DataSeeders;
using SagaGuide.Infrastructure.EntityFramework.GcsMasterLibrarySeeder;

namespace SagaGuide.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddPostgreSqlDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DbContext, GurpsDbContext>(options =>
        {
            var mySqlDbConfig = configuration.GetSection(MyDbConfig.ConfigSection).Get<MyDbConfig>();
            options.UseNpgsql(mySqlDbConfig!.ConnectionString());
        });
        
        services.Scan(scan => scan.FromAssembliesOf(typeof(GurpsDbContext))
            .AddClasses(classes => classes.AssignableTo<IRepository>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services
            .AddScoped<IUnitOfWork>(x => x.GetRequiredService<GurpsDbContext>())
            .AddScoped<IReadOnlyGurpsDbContext, GurpsDbContext>()
            .AddTransient<IDataSeeder, EfDataSeeder>()
            .AddTransient<GcsMasterLibrarySeeder, GcsMasterLibrarySeeder>();
    }
}