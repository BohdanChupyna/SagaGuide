using SagaGuide.Api.Authorization;
using SagaGuide.Api.ExceptionHandling;
using SagaGuide.Api.HealthChecks;
using SagaGuide.Api.Swagger;
using SagaGuide.Api.Tenant;
using SagaGuide.Application.Eventing;
using SagaGuide.Core.Domain.CharacterAggregate;
using SagaGuide.Core.IntegrationEvents;
using SagaGuide.Infrastructure;
using SagaGuide.Infrastructure.EntityFramework.Configuration;
using SagaGuide.Infrastructure.JsonConverters;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SagaGuide.Api.Controllers;
using SagaGuide.Api.HostedServices;
using SagaGuide.Api.Mapping;

namespace SagaGuide.Api;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //c.OAuthClientId("SagaGuide-app");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SagaGuide v1");
            });
        }
        
        // ToDo: https://stackoverflow.com/questions/51385671/failed-to-determine-the-https-port-for-redirect-in-docker
        // Set-up https
        app.UseHttpsRedirection();
        
        app.UseRouting();
        if (env.IsDevelopment())
        {
            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:3000");
                builder.WithOrigins("http://localhost:5258");
                builder.WithOrigins("http://localhost/*");
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();

            });
        }
        else
        {
            // ToDo: specify CORS for production
            app.UseCors(builder =>
            {
                builder.WithOrigins("https://saga.guide");
                builder.WithOrigins("https://saga.guide/*");
                builder.WithMethods("GET", "PUT", "DELETE", "POST");
                builder.AllowAnyHeader();

            });
        }
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
            });
        });
        
        
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        MyMapsterConfiguration.ConfigureMapster();
        
        services.AddControllers(options => { options.UseExceptionHandling(); });
        services.AddExceptionHandling();
        services.AddSwagger(Configuration);
        services.AddSecurity(Configuration);
        services.AddMediatrHandlers();
        services.AddPostgreSqlDb(Configuration);
        services.AddHostedService<MigrateDatabaseHostedService>();
        services.AddHealthCheckEndpoints();
        services.AddUserAndTenantToContext(Configuration);
        services.AddScoped<IIntegrationEventService, IntegrationEventService>();
        services.AddTransient<CharacterFactory>();
        services.AddSingleton<MemoryCacheProvider>();

        services.AddMvc().AddNewtonsoftJson(options => JsonSettingsWrapper.Create(options.SerializerSettings));

        var mySqlDbConfig = Configuration.GetSection(MyDbConfig.ConfigSection);
        services.Configure<MyDbConfig>(mySqlDbConfig);
    }
}