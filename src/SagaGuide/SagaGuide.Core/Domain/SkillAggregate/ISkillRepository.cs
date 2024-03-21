using SagaGuide.Core.Domain.IRepository;

namespace SagaGuide.Core.Domain.SkillAggregate;

public interface ISkillRepository : IAggregateRootRepository
{
    Task<Skill?> GetSkill(Guid id, CancellationToken cancellationToken = default);
}