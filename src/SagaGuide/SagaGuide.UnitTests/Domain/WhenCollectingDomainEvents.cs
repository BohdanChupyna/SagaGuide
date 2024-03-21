using SagaGuide.Core.Domain.Common;
using Xunit;

namespace SagaGuide.UnitTests.Domain;

public class WhenCollectingDomainEvents
{
    [Fact]
    public void ThenEventsAreCollectibleOnce()
    {
        var entity = new TestEntity();

        var events = (entity as IEventSource).CollectEvents();

        Assert.Single(events);

        events = (entity as IEventSource).CollectEvents();

        Assert.Empty(events);
    }
}