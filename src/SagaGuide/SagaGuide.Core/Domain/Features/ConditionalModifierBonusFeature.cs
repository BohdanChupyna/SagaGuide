using SagaGuide.Core.Domain.Prerequisite;

namespace SagaGuide.Core.Domain.Features;

public class ConditionalModifierBonusFeature: FeatureBase
{
    public override IFeature.FeatureTypeEnum FeatureType => IFeature.FeatureTypeEnum.ConditionalModifierBonus;
    
    public string Situation { get; set; } = null!;
}
