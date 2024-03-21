import {IFeature} from "./IFeature";
import {AttributeTypeEnum} from "../attribute/IAttribute";
import {IStringCriteria} from "../prerequisite/IStringCriteria";
import {INumericCriteria} from "../prerequisite/INumericCriteria";
import {IAttack} from "../equipment/IEquipment";



export interface IAttributeBonusFeature extends IFeature
{
    bonusLimitation: IAttributeBonusFeatureBonusLimitationEnum,
    attributeType: AttributeTypeEnum,
}

export function isIAttributeBonusFeature(feature: IAttributeBonusFeature|IFeature): feature is IAttributeBonusFeature
{
    return (feature as IAttributeBonusFeature).bonusLimitation !== undefined && (feature as IAttributeBonusFeature).attributeType !== undefined;
}

export enum IAttributeBonusFeatureBonusLimitationEnum
{
    None = "None",
    StrikingOnly = "StrikingOnly",
    LiftingOnly = "LiftingOnly",
    ThrowingOnly = "ThrowingOnly",
}

export interface IAttributeCostReductionFeature extends IFeature
{
    attributeType: AttributeTypeEnum,
}

export interface IConditionalModifierBonusFeature extends IFeature
{
    situation: string|null,
}

export interface IContainedWeightReductionFeature extends IFeature
{
    isInPercent: boolean,
}

export interface IDamageReductionBonusFeature extends IFeature
{
    location: string,
    specialization: IStringCriteria|null,
}

export function isIDamageReductionBonusFeature(feature: IDamageReductionBonusFeature|IFeature): feature is IDamageReductionBonusFeature
{
    return (feature as IDamageReductionBonusFeature).location !== undefined;
}

export interface IReactionBonusFeature extends IFeature
{
    situation: string|null,
}

export interface ISkillBonusFeature extends IFeature
{
    nameCriteria: IStringCriteria|null,
    specializationCriteria: IStringCriteria|null,
    tagsCriteria: IStringCriteria|null,
    skillSelectionType: ISkillBonusFeatureSkillSelectionTypeEnum,
}

export enum ISkillBonusFeatureSkillSelectionTypeEnum
{
    SkillsWithName = "SkillsWithName",
    ThisWeapon = "ThisWeapon",
    WeaponsWithName = "WeaponsWithName",
}

export interface ISkillPointBonusFeature extends IFeature
{
    nameCriteria: IStringCriteria|null,
    specializationCriteria: IStringCriteria|null,
    tagsCriteria: IStringCriteria|null,
}

export interface ISpellBonusFeature extends IFeature
{
    nameCriteria: IStringCriteria|null,
    tagsCriteria: IStringCriteria|null,
    spellMatchType: SpellMatchTypeEnum,
}

export enum SpellMatchTypeEnum
{
    AllColleges = "AllColleges",
    CollegeName = "CollegeName",
    PowerSource = "PowerSource",
    SpellName = "SpellName",
}

export interface ISpellPointBonusFeature extends IFeature
{
    nameCriteria: IStringCriteria|null,
    tagsCriteria: IStringCriteria|null,
    spellMatchType: SpellMatchTypeEnum,
}

export interface IWeaponBonusFeature extends IFeature
{
    nameCriteria: IStringCriteria|null,
    tagsCriteria: IStringCriteria|null,
    specializationCriteria: IStringCriteria|null,
    relativeLevelCriteria: INumericCriteria|null,
    weaponSelectionType: WeaponSelectionTypeEnum,
    weaponBonusType: WeaponBonusTypeEnum,
}

export enum WeaponSelectionTypeEnum
{
    WithRequiredSkill = "WithRequiredSkill",
    ThisWeapon = "ThisWeapon",
    WithName = "WithName",
}

export enum WeaponBonusTypeEnum
{
    Damage = "Damage",
    DamageReductionDivisor = "DamageReductionDivisor",
}
