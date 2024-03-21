namespace SagaGuide.Core.Domain.Features;

public class AttributeCostReductionFeature: FeatureBase
{
    public override IFeature.FeatureTypeEnum FeatureType => IFeature.FeatureTypeEnum.AttributeCostReduction;
    
    public Attribute.AttributeType AttributeType { get; set; }
}