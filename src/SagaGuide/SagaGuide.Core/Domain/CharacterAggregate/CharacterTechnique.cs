using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.TechniqueAggregate;

namespace SagaGuide.Core.Domain.CharacterAggregate;

public class CharacterTechnique : GuidEntity
{
    public Technique Technique { get; set; } = null!;
    public int SpentPoints { get; set; }
    public string? NameSpecialization { get; set; }
    public string? DefaultNameSpecialization { get; set; }
    
    // ToDo: implement in case of need it's value on back-end side
    //public int TechniqueLevel => ComputeTechniqueLevel();
}