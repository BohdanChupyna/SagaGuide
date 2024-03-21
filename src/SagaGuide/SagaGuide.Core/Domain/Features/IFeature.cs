namespace SagaGuide.Core.Domain.Features;

public interface IFeature
{
    public enum FeatureTypeEnum
    {
        AttributeBonus,
        ConditionalModifierBonus,
        DamageReductionBonus,
        ReactionBonus,
        SkillBonus,
        SkillPointBonus,
        SpellBonus,
        SpellPointBonus,
        WeaponBonus,
        WeaponDrDivisorBonus,
        AttributeCostReduction,
        ContainedWeightReduction
    }
    
    FeatureTypeEnum FeatureType { get; }
}