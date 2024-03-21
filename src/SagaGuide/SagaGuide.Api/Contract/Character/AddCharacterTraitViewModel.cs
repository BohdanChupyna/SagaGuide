namespace SagaGuide.Api.Contract.Character;

public class AddCharacterTraitViewModel
{
    public Guid FeatureId { get; set; }
    public int SpentPoints { get; set; }
    public string? OptionalSpecialty { get; set; }
}