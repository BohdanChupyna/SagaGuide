
namespace SagaGuide.Core.Definitions.CharacterAggregate
{
    public class AddCharacterFeatureDefinition
    {
        public Guid CharacterId { get; set; }
        public Guid FeatureId { get; set; }
        public int SpentPoints { get; set; }
        public string? OptionalSpecialty { get; set; }
    }
}
