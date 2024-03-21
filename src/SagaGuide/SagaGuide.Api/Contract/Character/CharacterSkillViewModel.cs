using SagaGuide.Core.Domain.SkillAggregate;
using SagaGuide.Api.Contract.Skill;

namespace SagaGuide.Api.Contract.Character;

public class CharacterSkillViewModel : GuidViewModel
{ 
    public SkillViewModel Skill { get; set; } = null!;
    public int SpentPoints { get; set; }
    public string? OptionalSpecialty { get; set; }
    public SkillDefault? DefaultedFrom { get; set; } = null!;
    //public int SkillLevel { get; set; }
}