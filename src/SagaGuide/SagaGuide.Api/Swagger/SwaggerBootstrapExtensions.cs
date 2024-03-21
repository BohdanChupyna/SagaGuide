using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using SagaGuide.Api.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SagaGuide.Api.Swagger;

public static class SwaggerBootstrapExtensions
{
    /// <summary>
    ///     Adds a Swagger page, configured to use OpenIDConnect.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        var authConfig = configuration.GetSection(AuthConfig.ConfigSection).Get<AuthConfig>();

        services.AddSwaggerGen(options =>
        {
            var apiInfo = new OpenApiInfo
            {
                Title = "SagaGuide",
                Version = "v1",
                Description = "API for OpenId Connect Test"
            };
            
            options.SwaggerDoc("v1", apiInfo);
            
            //First we define the security scheme
            options.AddSecurityDefinition("Bearer", //Name the security scheme
                new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                    Scheme = JwtBearerDefaults.AuthenticationScheme //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme, //The name of the previously defined security scheme.
                            Type = ReferenceType.SecurityScheme
                        }
                    },new List<string>()
                }
            });
            
            options.IncludeCurrentAssemblyXmlComments();
           
            //use fully qualified object names
            options.CustomSchemaIds(x => x.FullName);
        });
    }
    
    public static void IncludeCurrentAssemblyXmlComments(this SwaggerGenOptions options, bool includeControllerXmlComments = false) =>
        options.IncludeXmlComments(
            Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetCallingAssembly().GetName().Name}.xml"),
            includeControllerXmlComments);

    public static void IncludeXmlCommentsFromAssemblyContaining<TAssembleMarkerType>(this SwaggerGenOptions options,
        bool includeControllerXmlComments = false) => options.IncludeXmlComments(
        Path.Combine(AppContext.BaseDirectory, $"{typeof(TAssembleMarkerType).Assembly.GetName().Name}.xml"),
        includeControllerXmlComments);
}