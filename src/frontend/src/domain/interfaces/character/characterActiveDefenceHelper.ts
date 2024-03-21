import {ICharacter, ICharacterEquipment, ICharacterSkill} from "./ICharacter";
import {
    getMeleeAttackBlockModifier,
    getMeleeAttackParryModifier,
    IMeleeAttack,
    isIMeleeAttack
} from "../equipment/IEquipment";
import {
    _calculateParry,
    getBestDefaultForCharacter,
    getCharacterAttributeValue,
    getCharacterSkillLevel,
    getSkillDefaultValueForCharacter,
    _calculateEquipmentTotalBonusToAttribute
} from "./characterDomainLogicHelper";
import {AttributeTypeEnum} from "../attribute/IAttribute";
import {IsUndefinedOrNull} from "../../commonUtils";

export interface IActiveDefenceData {
    defenceName: AttributeTypeEnum.Parry | AttributeTypeEnum.Dodge | AttributeTypeEnum.Block,
    defenceProviderName: string,
    value: number,
}

export function getCharacterBlock(character: ICharacter): IActiveDefenceData
{
    let bestWeapon = _getWeaponWithBestBlock(character);
    if(!IsUndefinedOrNull(bestWeapon))
    {
        return  {
            defenceName: AttributeTypeEnum.Block,
            defenceProviderName: bestWeapon!.equipment.name,
            value: _getWeaponBestBlockValue(character, bestWeapon!)!,
        };
    }

    return  {
        defenceName: AttributeTypeEnum.Block,
        defenceProviderName: "",
        value: Number.NaN,
    };
}

export function getCharacterDodge(character: ICharacter): IActiveDefenceData
{
    return  {
        defenceName: AttributeTypeEnum.Dodge,
        defenceProviderName: "",
        value: getCharacterAttributeValue(character, AttributeTypeEnum.Dodge),
    };
}

export function getCharacterParry(character: ICharacter): IActiveDefenceData {
    let result: IActiveDefenceData =
        {
            defenceName: AttributeTypeEnum.Parry,
            defenceProviderName: "",
            value: _calculateEquipmentTotalBonusToAttribute(character.equipments, AttributeTypeEnum.Parry),
        }

    let bestWeapon = _getWeaponWithBestParry(character);
    if (bestWeapon !== null) {
        result.defenceProviderName = bestWeapon.equipment.name;
        result.value += _getWeaponBestParryValue(character, bestWeapon)!;
        return result;
    }

    let bestParrySkill = _getBestCharacterParrySkill(character);
    if (bestParrySkill !== null) {
        result.defenceProviderName = bestParrySkill?.skill.name;
        result.value += _calculateParry(getCharacterSkillLevel(character, bestParrySkill), 0);
        return result;
    }
    
    result.defenceProviderName = AttributeTypeEnum.Dexterity;
    result.value += _calculateParry(getCharacterAttributeValue(character, AttributeTypeEnum.Dexterity), 0);
    return result;
}

// return null if character doesn't have equipped weapon which allow parry
function _getWeaponWithBestParry(character: ICharacter): ICharacterEquipment | null {
    let meleeWeapons = character?.equipments.filter(eq => eq.isEquipped && eq.equipment.attacks.some(attack => isIMeleeAttack(attack)));

    if (meleeWeapons.length === 0) {
        return null;
    }

    let bestWeapon = meleeWeapons.reduce((accumulator, currentValue) => {
        return _getWeaponBestParryValue(character, accumulator)! > _getWeaponBestParryValue(character, currentValue)! ? accumulator : currentValue
    }, meleeWeapons[0]);

    return _getWeaponBestParryValue(character, bestWeapon) !== null ? bestWeapon : null;
}

function _getWeaponBestParryValue(character: ICharacter, weapon: ICharacterEquipment): number | null {
    let meleeAttacks = weapon.equipment.attacks.filter(attack => isIMeleeAttack(attack)) as IMeleeAttack[];

    let bestParry = meleeAttacks.reduce((accumulator, currentValue) => {
        return _getAttackParryValue(character, accumulator)! > _getAttackParryValue(character, currentValue)! ? accumulator : currentValue
    }, meleeAttacks[0]);

    return _getAttackParryValue(character, bestParry);
}

function _getAttackParryValue(character: ICharacter, attack: IMeleeAttack): number | null {
    let parryModifier = getMeleeAttackParryModifier(attack);
    if (parryModifier === null)
        return null;

    let skillDefault = getBestDefaultForCharacter(character, attack.defaults);
    let skillValue = getSkillDefaultValueForCharacter(character, skillDefault);
    return _calculateParry(skillValue!, parryModifier);
}

function _getBestCharacterParrySkill(character: ICharacter): ICharacterSkill|null
{
    const parrySkills = new Set<string>(["Judo", "Boxing", "Brawling", "Judo","Karate", "Sumo Wrestling", "Wrestling"])
    let meleeCombatSkills = character.skills.filter(s => parrySkills.has(s.skill.name));

    if (meleeCombatSkills.length === 0) {
        return null;
    }

    return meleeCombatSkills.reduce((accumulator, currentSkill) => {
        return getCharacterSkillLevel(character, accumulator) > getCharacterSkillLevel(character, currentSkill) ? accumulator : currentSkill;
    }, meleeCombatSkills[0])
}

// return null if character doesn't have equipped weapon which allow parry
function _getWeaponWithBestBlock(character: ICharacter): ICharacterEquipment | null {
    let meleeWeapons = character?.equipments.filter(eq => eq.isEquipped && eq.equipment.attacks.some(attack => isIMeleeAttack(attack)));

    if (meleeWeapons.length === 0) {
        return null;
    }

    let bestWeapon = meleeWeapons.reduce((accumulator, currentValue) => {
        return _getWeaponBestBlockValue(character, accumulator)! > _getWeaponBestBlockValue(character, currentValue)! ? accumulator : currentValue
    }, meleeWeapons[0]);

    return _getWeaponBestBlockValue(character, bestWeapon) !== null ? bestWeapon : null;
}

function _getWeaponBestBlockValue(character: ICharacter, weapon: ICharacterEquipment): number | null {
    let meleeAttacks = weapon.equipment.attacks.filter(attack => isIMeleeAttack(attack)) as IMeleeAttack[];

    let bestBlock = meleeAttacks.reduce((accumulator, currentValue) => {
        return _getAttackBlockValue(character, accumulator)! > _getAttackBlockValue(character, currentValue)! ? accumulator : currentValue
    }, meleeAttacks[0]);

    return _getAttackBlockValue(character, bestBlock);
}

function _getAttackBlockValue(character: ICharacter, attack: IMeleeAttack): number | null {
    let blockModifier = getMeleeAttackBlockModifier(attack);
    if (blockModifier === null)
        return null;

    let skillDefault = getBestDefaultForCharacter(character, attack.defaults);
    let skillValue = getSkillDefaultValueForCharacter(character, skillDefault);
    return _calculateBlock(skillValue!, blockModifier);
}

function _calculateBlock(skillLevel: number, modifier: number)
{
    return Math.floor(3 + skillLevel! / 2 + modifier);
}