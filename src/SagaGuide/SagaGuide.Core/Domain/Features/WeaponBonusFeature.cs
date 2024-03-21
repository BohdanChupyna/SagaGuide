using SagaGuide.Core.Domain.Prerequisite;

namespace SagaGuide.Core.Domain.Features;

public class WeaponBonusFeature : FeatureBase
{
    public enum WeaponSelectionTypeEnum
    {
        WithRequiredSkill,
        ThisWeapon,
        WithName,
    }

    public enum WeaponBonusTypeEnum
    {
        Damage,
        DamageReductionDivisor,
    }
    
    public override IFeature.FeatureTypeEnum FeatureType => IFeature.FeatureTypeEnum.WeaponBonus;

    public StringCriteria? NameCriteria { get; set; } = null!;
    public StringCriteria? SpecializationCriteria { get; set; }
    public StringCriteria? TagsCriteria { get; set; }
    public IntegerCriteria? RelativeLevelCriteria { get; set; }
    public WeaponSelectionTypeEnum WeaponSelectionType { get; set; }
    public WeaponBonusTypeEnum WeaponBonusType { get; set; }

}
// Percent                bool                `json:"percent,omitempty"` - didn't found any entrance in the whole library
