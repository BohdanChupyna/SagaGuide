using SagaGuide.Core.Domain.SkillAggregate;

namespace SagaGuide.Api.Contract.Character;

public class AddCharacterSkillViewModel
{
    public Guid SkillId { get; set; }
    public int SpentPoints { get; set; }
    public string? OptionalSpecialty { get; set; }
    public SkillDefault DefaultedFrom { get; set; } = null!;
}