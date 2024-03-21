using SagaGuide.Api;
using Xunit;

namespace SagaGuide.IntegrationTests.Setup;

[CollectionDefinition($"{SharedTestCollection.CollectionName}")]
public class SharedTestCollection : ICollectionFixture<ServerFixture>
{
    public const string CollectionName = "ContainerDataBase";
}