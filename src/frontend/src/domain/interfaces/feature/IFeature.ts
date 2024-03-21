export interface IFeature
{
    featureType: FeatureTypeEnum,
    amount: number,
    isScalingWithLevel: boolean,
}

export enum FeatureTypeEnum
{
    AttributeBonus = "AttributeBonus",
    ConditionalModifierBonus = "ConditionalModifierBonus",
    DamageReductionBonus = "DamageReductionBonus",
    ReactionBonus = "ReactionBonus",
    SkillBonus = "SkillBonus",
    SkillPointBonus = "SkillPointBonus",
    SpellBonus = "SpellBonus",
    SpellPointBonus = "SpellPointBonus",
    WeaponBonus = "WeaponBonus",
    WeaponDrDivisorBonus = "WeaponDrDivisorBonus",
    AttributeCostReduction = "AttributeCostReduction",
    ContainedWeightReduction = "ContainedWeightReduction",
}

