namespace SagaGuide.Core.IntegrationEvents;

public interface IIntegrationEventService
{
    Task AddAndSaveEventAsync(object evt);
    Task PublishEventsThroughEventBusAsync(Guid transactionId);
}