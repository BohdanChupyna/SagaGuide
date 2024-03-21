using MediatR;

namespace SagaGuide.Core.Domain.Common;

public abstract class Entity<T> : IIdentifiable<T>, IEventSource
    where T : IEquatable<T>, new()
{
    private readonly Queue<INotification> _eventQueue = new();

    IEnumerable<INotification> IEventSource.CollectEvents()
    {
        var list = new List<INotification>();

        while (_eventQueue.Any()) list.Add(_eventQueue.Dequeue());

        return list;
    }

    public virtual T Id { get; set; } = new();

    object IIdentifiable.Id => Id;

    protected virtual void RegisterEvent(INotification domainEvent)
    {
        _eventQueue.Enqueue(domainEvent);
    }
}