using System.Threading.Tasks;
using SagaGuide.IntegrationTests.Setup;
using Xunit;
using Xunit.Abstractions;

namespace SagaGuide.IntegrationTests.HealthCheck;

[Collection($"{SharedTestCollection.CollectionName}")]
public class WhenProbingHealthCheck : TestBase
{
    public WhenProbingHealthCheck(ServerFixture serverFixture, ITestOutputHelper testOutputHelper)
        : base(serverFixture, testOutputHelper)
    {
    }
    
    [Fact]
    public async Task ThenCheckReturnsHealthy()
    {
        var result = await ServerFixture.Client.GetAsync("health");
        result.EnsureSuccessStatusCode();
    }
}