using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.Features;

namespace SagaGuide.Core.Domain.TraitAggregate;

public class TraitModifier
{
    public enum CostTypeEnum
    {
        Percentage,
        Points,
        Multiplier,
    }
    
    public enum CostAffectTypeEnum
    {
        Total,
        BaseOnly,
        LevelsOnly,
    }
    
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public string? LocalNotes { get; set; }
    public List<string> Tags { get; set; } = new();
    public List<BookReference> BookReferences { get; set; } = new();
    public List<IFeature> Features { get; set; } = new ();
    
    public double PointsCost { get; set; }
    public CostTypeEnum CostType { get; set; }
    public CostAffectTypeEnum CostAffectType { get; set; }
    public bool CanLevel { get; set; }
}
// Levels     fxp.Int               `json:"levels,omitempty"`    - characterTraitModifierProperty
// Disabled   bool                  `json:"disabled,omitempty"`  - characterTraitModifierProperty
// VTTNotes   string                `json:"vtt_notes,omitempty"` - doesn't found any in GCS Library