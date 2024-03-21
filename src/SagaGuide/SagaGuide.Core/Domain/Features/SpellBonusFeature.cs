using SagaGuide.Core.Domain.Prerequisite;

namespace SagaGuide.Core.Domain.Features;

public enum SpellMatchTypeEnum
{
    AllColleges,
    CollegeName,
    PowerSource,
    SpellName,
}

public class SpellBonusFeature : FeatureBase
{
    public override IFeature.FeatureTypeEnum FeatureType => IFeature.FeatureTypeEnum.SpellBonus;
    public SpellMatchTypeEnum SpellMatchType { get; set; }
    public StringCriteria? NameCriteria { get; set; }
    public StringCriteria? TagsCriteria { get; set; }
}