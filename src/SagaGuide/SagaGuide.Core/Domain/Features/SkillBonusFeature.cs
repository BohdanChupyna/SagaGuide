using SagaGuide.Core.Domain.Prerequisite;

namespace SagaGuide.Core.Domain.Features;

public class SkillBonusFeature : FeatureBase
{
    public enum SkillSelectionTypeEnum
    {
        SkillsWithName,
        ThisWeapon,
        WeaponsWithName,
    }
        
  
    public override IFeature.FeatureTypeEnum FeatureType => IFeature.FeatureTypeEnum.SkillBonus;
    
    public StringCriteria? NameCriteria { get; set; }
    public StringCriteria? SpecializationCriteria { get; set; }
    public StringCriteria? TagsCriteria { get; set; }
    public SkillSelectionTypeEnum SkillSelectionType { get; set; }
}
