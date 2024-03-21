using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using SagaGuide.Api.Configuration;
using SagaGuide.Api.Constants;

namespace SagaGuide.Api.Authorization;

public static class ConfigureAuthenticationServiceExtensions
{
    /// <summary>
    ///     Configures the service with JWT bearer token security and authorization using OAuth scopes
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        var authConfig = configuration.GetSection(AuthConfig.ConfigSection).Get<AuthConfig>();
        var isTests = string.IsNullOrEmpty(authConfig?.JwtPublicKey) && !string.IsNullOrEmpty(authConfig?.TestKey);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            //options.Authority = dummyIssuer ? null : authConfig?.InternalIssuer;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = !isTests,
                ValidIssuers = new[] { authConfig?.InternalIssuer, authConfig?.ExternalIssuer },
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = isTests ? new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig!.TestKey)) : BuildRsaKey(authConfig!.JwtPublicKey),
                ValidateLifetime = true,
            };
            
            options.Events = new JwtBearerEvents()
            {
                OnTokenValidated = c =>
                {
                    Console.WriteLine("User successfully authenticated");
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = c =>
                {
                    c.NoResult();
                    Console.WriteLine($"An error occured processing your authentication.{c.Exception.ToString()}");
                    return Task.CompletedTask;
                }
            };
        });

        
        
        services.AddAuthorizationCore(options =>
        {
            foreach (var authorization in Authorizations.All)
                options.AddPolicy(authorization,
                    policy => policy.Requirements.Add(new HasScopeRequirement(authorization, authConfig!.ExternalIssuer)));
        });
        services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
    }
    
    private static RsaSecurityKey BuildRsaKey(string publicKeyJwt)
    {
        RSA rsa = RSA.Create();

        rsa.ImportSubjectPublicKeyInfo(

            source: Convert.FromBase64String(publicKeyJwt),
            bytesRead: out _
        );

        var issuerSigningKey = new RsaSecurityKey(rsa);

        return issuerSigningKey;
    }
}