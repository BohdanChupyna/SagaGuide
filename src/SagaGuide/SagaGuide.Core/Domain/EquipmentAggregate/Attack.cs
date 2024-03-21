using SagaGuide.Core.Domain.SkillAggregate;

namespace SagaGuide.Core.Domain.EquipmentAggregate;

public abstract class Attack
{
    public Guid Id { get; set; }

    public Damage Damage { get; set; }  = null!;
    public string MinimumStrength { get; set; }  = null!;
    public string? Usage { get; set; }
    public string? UsageNotes { get; set; }
    public List<SkillDefault> Defaults { get; set; } = new();
}
