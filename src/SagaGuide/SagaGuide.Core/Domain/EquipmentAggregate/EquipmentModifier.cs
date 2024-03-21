using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.Features;

namespace SagaGuide.Core.Domain.EquipmentAggregate;

public class EquipmentModifier
{
    public enum CostTypeEnum
    {
        OriginalEquipmentModifier,
        BaseEquipmentModifier,
        FinalBaseEquipmentModifier,
        FinalEquipmentModifier,
    }

    public enum WeightTypeEnum
    {
        OriginalEquipmentModifier,
        BaseEquipmentModifier,
        FinalBaseEquipmentModifier,
        FinalEquipmentModifier,
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Notes { get; set; }
    public List<string> Tags { get; set; } = new();
    public List<BookReference> BookReferences { get; set; } = new();
    public List<IFeature> Features { get; set; } = new ();
    public string? TechLevel { get; set; } = null!;
   
    public CostTypeEnum CostType { get; set; }
    public string? Cost { get; set; }
    
    public WeightTypeEnum WeightType { get; set; }
    public string? Weight { get; set; }
}