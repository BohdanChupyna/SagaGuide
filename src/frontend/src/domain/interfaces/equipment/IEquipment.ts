import IGuid, {IAuditable} from "../IGuid";
import IBookReference from "../IBookReference";
import {IPrerequisiteGroup} from "../prerequisite/IPrerequisiteGroup";
import {IFeature} from "../feature/IFeature";
import {ISkillDefault} from "../skill/ISkill";
import {numberToSignString} from "../../stringUtils";
import {IsUndefinedOrNull} from "../../commonUtils";

export interface IEquipment extends IAuditable
{
    name: string,
    bookReferences: IBookReference [],
    notes: string|null,
    techLevel: string|null,
    legalityClass: string|null,
    weight: string|null,
    cost: number|null,
    ratedStrength: number|null,
    maxUses: number|null,
    ignoreWeightForSkills: boolean,
    prerequisites: IPrerequisiteGroup|null,
    tags: string [],
    features: IFeature [],
    attacks: IAttack [],
    modifiers: IEquipmentModifier [],
}

export interface IDamage
{
    damageType: string|null,
    attackType: string|null,
    baseDamage: string|null,
    armorDivisor: number|null,
    fragmentation: string|null,
    fragmentationArmorDivisor: number|null,
    fragmentationAttackType: string|null,
    modifierPerDie: number|null,
}

export interface IAttack extends IGuid
{
    damage: IDamage;
    minimumStrength: string|null,
    usage: string|null,
    usageNotes: string|null,
    defaults: ISkillDefault [],
}

export interface IMeleeAttack extends IAttack
{
    reach: string|null,
    parry: string|null,
    block: string|null,
}

export interface IRangedAttack extends IAttack
{
    accuracy: string|null,
    range: string|null,
    rateOfFire: string|null,
    shots: string|null,
    bulk: string|null,
    recoil: string|null,
}

export enum EquipmentModifierCostTypeEnum
{
    OriginalEquipmentModifier = "OriginalEquipmentModifier",
    BaseEquipmentModifier = "BaseEquipmentModifier",
    FinalBaseEquipmentModifier = "FinalBaseEquipmentModifier",
    FinalEquipmentModifier = "FinalEquipmentModifier",
}

export enum EquipmentModifierWeightTypeEnum
{
    OriginalEquipmentModifier = "OriginalEquipmentModifier",
    BaseEquipmentModifier = "BaseEquipmentModifier",
    FinalBaseEquipmentModifier = "FinalBaseEquipmentModifier",
    FinalEquipmentModifier = "FinalEquipmentModifier",
}

export interface IEquipmentModifier extends IGuid
{
    name: string|null,
    notes: string|null,
    tags: string [],
    bookReferences: IBookReference [],
    features: IFeature [],
    techLevel: string|null,
    costType: EquipmentModifierCostTypeEnum,
    cost: string|null,
    weightType: EquipmentModifierWeightTypeEnum,
    weight: string|null,
}

export function getEquipmentWeightValue(equipment: IEquipment): number
{
    let weight = equipment.weight?.substring(0, equipment.weight?.indexOf(" ")) ?? "0";
    return parseFloat(weight);
}

export function getEquipmentTechLevelValue(equipment: IEquipment): number
{
    let result = parseInt(equipment.techLevel ?? "0", 10);
    return !Number.isNaN(result) ? result : 0;
}

export function getEquipmentLegalityClassValue(equipment: IEquipment): number
{
    let result = parseInt(equipment.legalityClass ?? "4", 10);
    return !Number.isNaN(result) ? result : 4;
}

// null means that weapon does not allow to parry
export function getMeleeAttackParryModifier(attack: IMeleeAttack): number|null
{
    if(IsUndefinedOrNull(attack.parry) || attack.parry?.toLowerCase() === "no")
        return null;

    const regex = /(\d+|\D+)/g;
    let splitResult = attack.parry!.split(regex).filter(Boolean);
    let result = parseInt(splitResult[0], 10);
    if(Number.isNaN(result))
    {
        console.error(`Wrong parry format in ${attack.parry}!`);
        return null;
    }
    return result;
}

export function getMeleeAttackBlockModifier(attack: IMeleeAttack): number|null
{
    if(IsUndefinedOrNull(attack.block) || attack.block?.toLowerCase() === "no")
        return null;
    
    let result = parseInt(attack.block!, 10);
    if(Number.isNaN(result))
    {
        console.error(`Wrong block format in ${attack.block}!`);
        return null;
    }
    return result;
}

export function isIAttack(attack: any | IAttack): attack is IAttack {
    return (attack as IAttack).damage !== undefined;
}

export function isIMeleeAttack(attack: IMeleeAttack | IAttack): attack is IMeleeAttack {
    return (attack as IMeleeAttack).reach !== undefined;
}
export function isIRangedAttack(attack: IRangedAttack | IAttack): attack is IRangedAttack {
    return (attack as IRangedAttack).accuracy !== undefined;
}

export function damageToString(damage: IDamage)
{
    let result = "";
    if(!IsUndefinedOrNull(damage.attackType))
        result += damage.attackType;

    if(!IsUndefinedOrNull(damage.baseDamage))
        result += damage.baseDamage?.includes("d") ? damage.baseDamage : numberToSignString(parseFloat(damage.baseDamage ?? ""));
    
    if(!IsUndefinedOrNull(damage.damageType))
        result += ` ${damage.damageType}`;
    return result
}
