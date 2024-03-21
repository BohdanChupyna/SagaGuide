namespace SagaGuide.Core.Domain.IRepository;

public interface IUnitOfWork
{
    Guid? GetCurrentTransactionId();
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}