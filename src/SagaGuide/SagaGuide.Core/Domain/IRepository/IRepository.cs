namespace SagaGuide.Core.Domain.IRepository;

/// <summary>
///     marker interface for DI
/// </summary>
public interface IRepository
{
}

public interface IAggregateRootRepository : IRepository
{
    IUnitOfWork UnitOfWork { get; }
}