using SagaGuide.Core.Domain.IRepository;

namespace SagaGuide.Core.Domain.TraitAggregate;

public interface ITraitRepository : IAggregateRootRepository
{
    Task<Trait?> GetFeatureAsync(Guid id, CancellationToken cancellationToken = default);
}
