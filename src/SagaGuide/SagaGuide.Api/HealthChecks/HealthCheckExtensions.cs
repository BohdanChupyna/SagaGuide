using System.Net.Mime;
using SagaGuide.Infrastructure.JsonConverters;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SagaGuide.Api.HealthChecks;

public static class HealthCheckExtensions
{
    private const string HealthUrl = "/health";
    private const string MySqlTag = "MySQL";

    public static void AddHealthCheckEndpoints(this IServiceCollection services)
    {
        services.AddSingleton<DatabaseHealthCheck>();
        services.AddHealthChecks()
            .AddCheck<DatabaseHealthCheck>(MySqlTag, tags: new[] { MySqlTag });
    }

    public static void MapCustomHealthChecks(this IEndpointRouteBuilder endpoint)
    {
        MapCustomHealthCheck(endpoint, HealthUrl, "AllChecks");
        MapCustomHealthCheck(endpoint, $"{HealthUrl}/{MySqlTag}", MySqlTag, MySqlTag);
    }

    private static string CreateJsonResponse(HealthReport report, string checkName)
    {
        var json = JsonConverterWrapper.Serialize(
            new
            {
                Name = checkName,
                Status = report.Status.ToString(),
                Duration = report.TotalDuration,
                Info = report.Entries
                    .Select(e =>
                        new
                        {
                            e.Key,
                            e.Value.Description,
                            e.Value.Duration,
                            Status = Enum.GetName(
                                typeof(HealthStatus),
                                e.Value.Status),
                            Error = e.Value.Exception?.Message,
                            e.Value.Data
                        })
                    .ToList()
            });

        return json;
    }

    private static Func<HealthCheckRegistration, bool> GetFilter(string tag)
    {
        Func<HealthCheckRegistration, bool> filterPredicate = string.IsNullOrWhiteSpace(tag)
            ? _ => true
            : check => check.Tags.Any(t => t == tag);

        return filterPredicate;
    }

    private static IEndpointConventionBuilder MapCustomHealthCheck(IEndpointRouteBuilder endpoint, string endpointUrl,
        string checkName, string tagToFilter = "")
    {
        var endpointConventionBuilder = endpoint.MapHealthChecks(endpointUrl, new HealthCheckOptions
            {
                Predicate = GetFilter(tagToFilter),
                ResponseWriter = async (context, report) =>
                {
                    var json = CreateJsonResponse(report, checkName);
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(json);
                }
            }
        );

        return endpointConventionBuilder;
    }
}