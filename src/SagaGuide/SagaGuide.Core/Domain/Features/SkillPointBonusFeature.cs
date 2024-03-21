using SagaGuide.Core.Domain.Prerequisite;

namespace SagaGuide.Core.Domain.Features;

public class SkillPointBonusFeature : FeatureBase
{
    public override IFeature.FeatureTypeEnum FeatureType => IFeature.FeatureTypeEnum.SkillPointBonus;
    
    public StringCriteria NameCriteria { get; set; } = null!;
    public StringCriteria? SpecializationCriteria { get; set; }
    public StringCriteria? TagsCriteria { get; set; }
}
