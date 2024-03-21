using SagaGuide.Core.Domain.Prerequisite;

namespace SagaGuide.Core.Domain.Features;

public class SpellPointBonusFeature : FeatureBase
{
    public override IFeature.FeatureTypeEnum FeatureType => IFeature.FeatureTypeEnum.SpellPointBonus;
    public SpellMatchTypeEnum SpellMatchType { get; set; }
    public StringCriteria? NameCriteria { get; set; }
    public StringCriteria? TagsCriteria { get; set; }
}