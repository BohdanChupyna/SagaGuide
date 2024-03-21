using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using SagaGuide.Api;
using SagaGuide.Infrastructure.EntityFramework.DataSeeders;
using Microsoft.Extensions.DependencyInjection;
using SagaGuide.IntegrationTests.Setup;
using Xunit;
using Xunit.Abstractions;

namespace SagaGuide.IntegrationTests;

public class ServerFixture : IAsyncLifetime
{
    private BaseWebApplicationFactory<Startup> _application = null!;

    public HttpClient Client => _application.Client;

    public IServiceProvider ServiceProvider => _application.Services;

    public ITestOutputHelper TestOutputHelper  => _application.TestOutputHelperProvider.TestOutputHelper;
    public IDataSeeder? DataSeeder => _application.Services.GetService<IDataSeeder>();

    public void ChangeTestOutputHelper(ITestOutputHelper testOutputHelper) => _application.ChangeTestOutputHelper(testOutputHelper);
    
    public Claim GetUserClaim(Guid userId)
    {
        return new Claim(ClaimTypes.NameIdentifier, userId.ToString());
    }
    
    public void SetToken(string token)
    {
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
    
    public void RemoveToken()
    {
        Client.DefaultRequestHeaders.Authorization = null;
    }
    
    public async Task  ResetDbAsync()
    {
        await _application.ResetDatabaseAsync();
    }
    
    public async Task InitializeAsync()
    {
        _application = new BaseWebApplicationFactory<Startup>();
        await _application.InitializeAsync();
    }
    
    public async Task DisposeAsync()
    {
        await _application.DisposeAsync();
    }
}