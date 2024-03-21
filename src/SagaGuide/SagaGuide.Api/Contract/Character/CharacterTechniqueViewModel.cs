using SagaGuide.Api.Contract.Technique;

namespace SagaGuide.Api.Contract.Character;

public class CharacterTechniqueViewModel : GuidViewModel
{
    public TechniqueViewModel Technique { get; set; } = null!;
    public int SpentPoints { get; set; }
    public string? NameSpecialization { get; set; }
    public string? DefaultNameSpecialization { get; set; }
}