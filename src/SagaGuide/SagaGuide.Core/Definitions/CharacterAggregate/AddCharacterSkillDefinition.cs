using SagaGuide.Core.Domain.SkillAggregate;

namespace SagaGuide.Core.Definitions.CharacterAggregate;

public class AddCharacterSkillDefinition
{
    public Guid CharacterId { get; set; }
    public Guid SkillId { get; set; }
    public int SpentPoints { get; set; }
    public string? OptionalSpecialty { get; set; }
    public SkillDefault DefaultedFrom { get; set; } = null!;
}