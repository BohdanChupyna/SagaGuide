using SagaGuide.Core.Domain.IRepository;

namespace SagaGuide.Core.Domain.CharacterAggregate;

public interface ICharacterRepository : IAggregateRootRepository
{
    void Update(Character character);
    Character Add(Character character);
    Task<Character?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Character?> GetAsNoTrackingAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}