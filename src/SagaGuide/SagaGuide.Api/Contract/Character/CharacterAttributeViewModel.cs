namespace SagaGuide.Api.Contract.Character;

public class CharacterAttributeViewModel : GuidViewModel
{
    public AttributeViewModel Attribute { get; set; } = null!;
    public int SpentPoints { get; set; }
}