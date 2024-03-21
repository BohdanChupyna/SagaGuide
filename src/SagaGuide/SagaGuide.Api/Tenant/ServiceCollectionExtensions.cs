using System.Security.Claims;
using SagaGuide.Core.Domain.EnvironmentAbstractions;
using SagaGuide.Api.Constants;

namespace SagaGuide.Api.Tenant;

public static class ServiceCollectionExtensions
{
    public static void AddUserAndTenantToContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient(s =>
        {
            var httpContext = s.GetService<IHttpContextAccessor>();
            if (httpContext?.HttpContext == null) // TODO tenants
            {
                var logger = s.GetService<ILogger<IUserAccessor>>();
                logger?.LogWarning("Unable to get ClaimsPrinciple from context in current DI context");

                //eg: migrations run without HTTP context user
                return new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new(ClaimTypes.NameIdentifier, Authorizations.MaintenanceId) }));
            }

            return httpContext.HttpContext.User;
        });
        
        services.AddTransient<IUserAccessor, UserAccessor>();
    }
}