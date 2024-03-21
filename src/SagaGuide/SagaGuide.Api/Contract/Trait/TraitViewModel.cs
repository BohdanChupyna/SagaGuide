using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.Features;
using SagaGuide.Core.Domain.Prerequisite;
using SagaGuide.Core.Domain.TraitAggregate;

namespace SagaGuide.Api.Contract.Trait;

public class TraitViewModel : AuditableViewModel
{
    public string Name { get; set; } = null!;
    public string? LocalNotes { get; set; }
  
    public List<string> Tags { get; set; } = new();
    public PrerequisiteGroup? Prerequisites { get; set; }
    public List<BookReference> BookReferences { get; set; } = new();
    
    public int PointsCostPerLevel { get; set; }
    public int BasePointsCost { get; set; }
    public bool CanLevel { get; set; }
    public bool RoundCostDown { get; set; }

    public List<IFeature> Features { get; set; } = new ();
    public List<TraitModifier> Modifiers { get; set; } = new ();
    public List<TraitModifierGroup> ModifierGroups { get; set; } = new ();

    public int SelfControlRoll { get; set; }
    public Core.Domain.TraitAggregate.Trait.SelfControlRollAdjustmentEnum SelfControlRollAdjustment { get; set; }
}