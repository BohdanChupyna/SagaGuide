using SagaGuide.Core;
using SagaGuide.Core.IntegrationEvents;
using SagaGuide.Infrastructure.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog.Context;

namespace SagaGuide.Api.MediatRBehavior;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly GurpsDbContext _dbContext;
    private readonly IIntegrationEventService _integrationEventService;
    private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;

    public TransactionBehaviour(GurpsDbContext dbContext,
        IIntegrationEventService integrationEventService,
        ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
        _integrationEventService = integrationEventService ?? throw new ArgumentException(nameof(integrationEventService));
        _logger = logger ?? throw new ArgumentException(nameof(ILogger));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = default(TResponse);
        var typeName = request.GetGenericTypeName();

        try
        {
            if (_dbContext.Transaction.HasActiveTransaction)
                return await next();

            var strategy = _dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                Guid transactionId;

                using var transaction = await _dbContext.Transaction.BeginTransactionAsync();
                if (transaction == null) throw new Exception($"Couldn't start a transaction while executing command: {typeName}");
                using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
                {
                    _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                    response = await next();

                    if(response is CommandResponseBase { IsSuccess: false })
                    {
                        _logger.LogInformation("----- Rollback transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);
                        _dbContext.Transaction.RollbackTransaction();
                        return;
                    }
                    
                    _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                    await _dbContext.Transaction.CommitTransactionAsync(transaction);

                    transactionId = transaction.TransactionId;
                }

                await _integrationEventService.PublishEventsThroughEventBusAsync(transactionId);
            });

#pragma warning disable CS8603 // Possible null reference return.
            return response;
#pragma warning restore CS8603 // Possible null reference return.
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);

            throw;
        }
    }
}