namespace SagaGuide.Core.Domain.Features;

public class ContainedWeightReductionFeature: FeatureBase
{
    public override IFeature.FeatureTypeEnum FeatureType => IFeature.FeatureTypeEnum.ContainedWeightReduction;
    
    public bool IsInPercent { get; set; }
}