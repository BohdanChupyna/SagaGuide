using SagaGuide.Core.Domain.CharacterAggregate;

namespace SagaGuide.Api.Contract.Character;

public class CharacterViewModel : AuditableViewModel
{
    public Guid UserId { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string Player { get; set; } = string.Empty;
    public string Campaign { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Handedness { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Race { get; set; } = string.Empty;
    public string Religion { get; set; } = string.Empty;
    public double Age { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public int TechLevel { get; set; }
    public double Size { get; set; }
    public int HpLose { get; set; }
    public int FpLose { get; set; }
    public int TotalPoints { get; set; }
    public List<CharacterAttributeViewModel> Attributes { get; set; } = new ();
    public List<CharacterSkillViewModel> Skills { get; set; } = new ();
    public List<CharacterTechniqueViewModel> Techniques { get; set; } = new();
    public List<CharacterTraitViewModel> Traits { get; set; } = new ();
    public List<CharacterEquipment> Equipments { get; set; } = new();
}