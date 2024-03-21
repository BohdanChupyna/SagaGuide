using SagaGuide.Core.Domain.EnvironmentAbstractions;
using SagaGuide.Core.Domain.IRepository;

namespace SagaGuide.Core.Domain.CharacterAggregate;

public class CharacterFactory
{
    private readonly IBasicAttributeRepository _repository;
    private readonly IUserAccessor _userAccessor;
    public CharacterFactory(IBasicAttributeRepository repository, IUserAccessor userAccessor)
    {
        _repository = repository;
        _userAccessor = userAccessor;
    }

    public async Task<Character> CreateAsync(CancellationToken cancellationToken)
    {
        var character = new Character
        {
            UserId = _userAccessor.GetCurrentUserId(),
            Name = "New Character"
        };
        
        await CreateAttributesAsync(character, cancellationToken);

        return character;
    }
    
    private async Task CreateAttributesAsync(Character character, CancellationToken cancellationToken)
    {
        var basicAttributes = await _repository.GetAllAttributesAsync(cancellationToken);
        
        character.Attributes = basicAttributes.Select(basic => new CharacterAttribute
        {
            Attribute = basic,
            SpentPoints = 0
        }).ToList();
    }
}