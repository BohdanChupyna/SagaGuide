using SagaGuide.Core.Domain;
using SagaGuide.Core.Domain.CharacterAggregate;
using SagaGuide.Core.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using Attribute = SagaGuide.Core.Domain.Attribute;

namespace SagaGuide.Infrastructure.EntityFramework.Repositories;

public class BasicAttributeRepository : IBasicAttributeRepository
{
    private readonly GurpsDbContext _context;

    public BasicAttributeRepository(GurpsDbContext context) => _context = context;

    public async Task<List<Attribute>> GetAllAttributesAsync(CancellationToken cancellationToken) => await _context.BasicAttributes.AsTracking().ToListAsync(cancellationToken);
}