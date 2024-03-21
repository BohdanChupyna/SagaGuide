using SagaGuide.Core.Domain.CharacterAggregate;
using SagaGuide.Core.Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace SagaGuide.Infrastructure.EntityFramework.Repositories;

public class CharacterRepository : ICharacterRepository
{
    private readonly GurpsDbContext _context;

    public CharacterRepository(GurpsDbContext context) => _context = context;

    public IUnitOfWork UnitOfWork => _context;
    
    public void Update(Character character)
    {
        _context.Update(character);
    }

    public Character Add(Character character)
    {
        var result = _context.Characters
            .Add(character).Entity;
        return result;
    }

    public async Task<Character?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return result;
    }

    public async Task<Character?> GetAsNoTrackingAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _context.Characters.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return result;
    }
    
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var character = await GetAsync(id, cancellationToken);
        if (character == null)
        {
            return false;
        }
        
        _context.Characters.Remove(character);
        return true;
    }
}