using SagaGuide.Core.Domain.Prerequisite;

namespace SagaGuide.Core.Domain.Features;

public class DamageReductionBonusFeature: FeatureBase
{
    public override IFeature.FeatureTypeEnum FeatureType => IFeature.FeatureTypeEnum.DamageReductionBonus;
    
    public string Location { get; set; } = null!;
    public string? Specialization { get; set; }
}
