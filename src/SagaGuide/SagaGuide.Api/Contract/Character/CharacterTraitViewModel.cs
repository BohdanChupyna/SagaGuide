using SagaGuide.Core.Domain.CharacterAggregate;
using SagaGuide.Api.Contract.Trait;

namespace SagaGuide.Api.Contract.Character;

public class CharacterTraitViewModel : GuidViewModel
{
    public TraitViewModel Trait { get; set; } = null!;
    public string? OptionalSpecialty { get; set; }
    public int Level { get; set; }

    public List<CharacterTraitModifier> SelectedTraitModifiers { get; set; } = new ();
}