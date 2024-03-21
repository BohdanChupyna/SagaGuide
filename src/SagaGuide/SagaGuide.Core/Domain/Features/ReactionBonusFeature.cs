using SagaGuide.Core.Domain.Prerequisite;

namespace SagaGuide.Core.Domain.Features;

public class ReactionBonusFeature: FeatureBase
{
    public  override IFeature.FeatureTypeEnum FeatureType => IFeature.FeatureTypeEnum.ReactionBonus;
    
    public string Situation { get; set; } = null!;
}
