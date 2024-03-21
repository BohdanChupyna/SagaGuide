using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

namespace SagaGuide.Api.HealthChecks;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public DatabaseHealthCheck(IServiceScopeFactory serviceScopeFactory) => _serviceScopeFactory = serviceScopeFactory;

    private volatile bool _isMigrationCompleted;

    public bool MigrationCompleted
    {
        get => _isMigrationCompleted;
        set => _isMigrationCompleted = value;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var log = Log.ForContext<DatabaseHealthCheck>();

        var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
        var isHealthy = await dbContext.Database.CanConnectAsync(cancellationToken);

        switch (isHealthy)
        {
            case true when _isMigrationCompleted:
            {
                log.Information("Database available.");
                return HealthCheckResult.Healthy("Database available.");
            }
            case true when !_isMigrationCompleted:
            {
                log.Information("Database is migrating.");
                return HealthCheckResult.Unhealthy("Database is migrating.");
            }
            default:
            {
                log.Error("Database unavailable");
                return new HealthCheckResult(context.Registration.FailureStatus, "Database unavailable");
            }
        }
    }
}