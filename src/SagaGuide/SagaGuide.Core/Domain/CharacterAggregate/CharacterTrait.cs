using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.TraitAggregate;

namespace SagaGuide.Core.Domain.CharacterAggregate;

public class CharacterTrait : GuidEntity
{
    public Trait Trait { get; set; } = null!;
    public string? OptionalSpecialty { get; set; }
    public int Level { get; set; }
    public List<CharacterTraitModifier> SelectedTraitModifiers { get; set; } = new ();
}