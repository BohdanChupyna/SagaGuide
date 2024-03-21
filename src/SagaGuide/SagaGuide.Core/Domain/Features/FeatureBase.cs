namespace SagaGuide.Core.Domain.Features;

public class FeatureBase : IFeature
{
    public virtual IFeature.FeatureTypeEnum FeatureType => throw new NotImplementedException();
    
    public int Amount { get; set; }
    public bool IsScalingWithLevel { get; set; }
}