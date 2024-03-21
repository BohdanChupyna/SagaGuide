using MediatR;

namespace SagaGuide.Core.Domain.Common;

public interface IEventSource
{
    IEnumerable<INotification> CollectEvents();
}