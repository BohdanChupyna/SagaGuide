using SagaGuide.Core.Domain.TraitAggregate;
using SagaGuide.Core.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace SagaGuide.Infrastructure.EntityFramework.Repositories;

internal class TraitRepository : ITraitRepository
{
    private readonly GurpsDbContext _context;

    public TraitRepository(GurpsDbContext context) => _context = context;

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Trait?> GetFeatureAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _context.Traits.FirstOrDefaultAsync(feature => feature.Id == id, cancellationToken);

        return result;
    }
}
