using SagaGuide.Core.Domain.IRepository;
using SagaGuide.Core.Domain.SkillAggregate;
using Microsoft.EntityFrameworkCore;

namespace SagaGuide.Infrastructure.EntityFramework.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly GurpsDbContext _context;

    public SkillRepository(GurpsDbContext context) => _context = context;

    public IUnitOfWork UnitOfWork => _context;
    
    public async Task<Skill?> GetSkill(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _context.Skills.FirstOrDefaultAsync(skill => skill.Id == id, cancellationToken);
        
        return result;
    }
}