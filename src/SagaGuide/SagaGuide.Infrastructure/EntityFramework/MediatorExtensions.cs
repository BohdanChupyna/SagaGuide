using SagaGuide.Core.Domain.Common;
using MediatR;

namespace SagaGuide.Infrastructure.EntityFramework;

internal static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, GurpsDbContext ctx)
    {
        var domainEvents = ctx.ChangeTracker
            .Entries<IEventSource>()
            .SelectMany(x => x.Entity.CollectEvents())
            .ToList();

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}