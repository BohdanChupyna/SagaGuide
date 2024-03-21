using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.Features;
using SagaGuide.Core.Domain.Prerequisite;

namespace SagaGuide.Core.Domain.EquipmentAggregate;

public class Equipment : AuditableEntity
{
    public string Name { get; set; }  = null!;
    public List<BookReference> BookReferences { get; set; }  = null!;
    public string? Notes { get; set; }
    public string? TechLevel { get; set; }
    public string? LegalityClass { get; set; }

    public double? Cost { get; set; }
    public string? Weight { get; set; }
    public int? RatedStrength { get; set; }
    public int? MaxUses { get; set; }
    public List<string> Tags { get; set; }  = null!;
    public PrerequisiteGroup? Prerequisites { get; set; }
    public List<IFeature> Features { get; set; } = new ();
    public List<Attack> Attacks { get; set; }  = new ();
    public List<EquipmentModifier> Modifiers { get; set; }  = new ();
    public bool IgnoreWeightForSkills { get; set; }
}