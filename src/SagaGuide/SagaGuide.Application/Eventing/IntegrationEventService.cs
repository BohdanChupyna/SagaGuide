using System.Collections.Concurrent;
using SagaGuide.Core.Domain;
using SagaGuide.Core.Domain.IRepository;
using SagaGuide.Core.IntegrationEvents;
using Microsoft.Extensions.Logging;

namespace SagaGuide.Application.Eventing;

public class IntegrationEventService : IIntegrationEventService
{
    private static readonly ConcurrentDictionary<Guid, ConcurrentQueue<object>> _memoryOutbox = new();

    private readonly ILogger<IntegrationEventService> _logger;

    private readonly IUnitOfWork _unitOfWork;
    // private readonly IMessageSession _messageSession;

    public IntegrationEventService(IUnitOfWork unitOfWork,
        ILogger<IntegrationEventService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    /// <summary>
    ///     currently this uses a in memory outbox
    ///     (https://jimmybogard.com/life-beyond-distributed-transactions-an-apostates-implementation-relational-resources/)
    ///     if you want it more robust (eg: service bus failures), save event to sql in same transaction
    ///     after transaction commits, you can read events safely in PublishEventsThroughEventBusAsync
    /// </summary>
    public Task AddAndSaveEventAsync(object evt)
    {
        var transactionId = _unitOfWork.GetCurrentTransactionId();
        if (transactionId == null)
            throw new ApplicationException("Unable to add integration event without transaction");
        var queueForTransaction = _memoryOutbox.GetOrAdd(transactionId.Value, new ConcurrentQueue<object>());
        queueForTransaction.Enqueue(evt);
        return Task.CompletedTask;
    }

    public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
    {
        if (!_memoryOutbox.TryGetValue(transactionId, out var queueForTransaction)) return;

        var allgood = true;
        foreach (var logEvt in queueForTransaction)
        {
            _logger.LogInformation("----- Publishing integration event: {@IntegrationEvent}", logEvt);

            try
            {
                //await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                //await _messageSession.Publish(logEvt);
                //await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR publishing integration event: {@IntegrationEvent}", logEvt);
                allgood = false;
                //await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
            }
        }

        if (allgood) _memoryOutbox.TryRemove(transactionId, out var queue);
    }
}