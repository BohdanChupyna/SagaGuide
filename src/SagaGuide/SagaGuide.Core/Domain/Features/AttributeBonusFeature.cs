namespace SagaGuide.Core.Domain.Features;

public class AttributeBonusFeature : FeatureBase
{
    public enum BonusLimitationEnum
    {
        None,
        StrikingOnly,
        LiftingOnly,
        ThrowingOnly,
    }
    
    public override IFeature.FeatureTypeEnum FeatureType => IFeature.FeatureTypeEnum.AttributeBonus;
    
    public Attribute.AttributeType AttributeType { get; set; }
    public BonusLimitationEnum BonusLimitation { get; set; }
}
