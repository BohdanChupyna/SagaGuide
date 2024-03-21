import {
    ICharacter,
    ICharacterAttribute,
    ICharacterEquipment,
    ICharacterPointsStatistic,
    ICharacterSkill,
    ICharacterTechnique,
    ICharacterTrait
} from "./ICharacter";
import {
    getSkillDefaultAsString,
    getSkillNameWithSpecialization,
    ISkill,
    ISkillDefault,
    SkillDifficultyLevelEnum
} from "../skill/ISkill";
import {AttributeTypeEnum} from "../attribute/IAttribute";
import {v4 as uuidv4} from 'uuid';
import ITrait, {
    ApplyTraitModifierCost,
    ITraitModifier,
    TraitModifierCostAffectTypeEnum,
    TraitModifierCostTypeEnum,
    TraitTags
} from "../trait/ITrait";
import {ITechnique} from "../technique/ITechnique";
import {IsUndefinedOrNull} from "../../commonUtils";
import {SagaGuideCurrentCharacterState} from "../../../redux/slices/currentCharacter/currentCharacterSlice";
import {hasSpecializationSymbol, SpecializationReplacementRegExp} from "../../stringUtils";
import {IEquipment} from "../equipment/IEquipment";
import {
    constructErrorToast,
    constructInfoToast,
    constructWarningToast,
} from "../../../redux/slices/toasts/IToastProviderSlice";
import {IAttributeBonusFeature, isIAttributeBonusFeature} from "../feature/features";

export function _changeCharacterSkillPointsImpl(state: SagaGuideCurrentCharacterState, skillId: string, newPoints: number)
{
    if(state.character === null)
        return;

    const skill = state.character.skills.find(skill => skill.id === skillId);

    if(skill === undefined)
        throw new Error(`Can't find character skill with id: ${skillId}`);
    
    skill.spentPoints = newPoints;
}

function _skillDifficultyPenalty(difficultyLevel: SkillDifficultyLevelEnum): number 
{
    switch (difficultyLevel) {
        case SkillDifficultyLevelEnum.Easy:
            return 0;
        case SkillDifficultyLevelEnum.Average:
            return -1;
        case SkillDifficultyLevelEnum.Hard:
            return -2;
        case SkillDifficultyLevelEnum.VeryHard:
            return -3;
        default:
            throw new Error(`Unknown Skill Difficulty level ${difficultyLevel}`);
    }
}

export function _changeCharacterAttributeImpl(state: SagaGuideCurrentCharacterState, attributeType:AttributeTypeEnum, newValue: number)
{
    if(state.character === null)
        return;

    const characterAttribute = getCharacterAttribute(state.character, attributeType);

    if(characterAttribute === undefined)
        throw new Error("Wrong character attribute type");

    const attributeDelta = newValue - getCharacterAttributeValue(state.character, attributeType);
    
    characterAttribute.spentPoints += characterAttribute.attribute.pointsCostPerLevel * (attributeDelta/characterAttribute.attribute.valueIncreasePerLevel);
}

export function getCharacterSkillLevel (character: ICharacter, characterSkill: ICharacterSkill): number
{
    const attributeValue = getCharacterAttributeValue(character, characterSkill.skill.attributeType);
    const penalty = _skillDifficultyPenalty(characterSkill.skill.difficultyLevel);
    if (characterSkill.spentPoints < 4)
    {
        return Math.floor(attributeValue + penalty + characterSkill.spentPoints / 2);
    }
    
    return Math.floor(attributeValue + penalty + 1 + characterSkill.spentPoints / 4);
}

export function getCharacterTechniqueLevel (character: ICharacter, characterTechnique: ICharacterTechnique): number
{
    let attributeValue = 0;
    let skill = getCharacterTechniqueRelatedSkill(character, characterTechnique.technique);
    if(!skill)
        return 0;

    attributeValue = getCharacterSkillLevel(character, skill!);
    attributeValue += characterTechnique.technique.default.modifier;
   
    const penalty = _skillDifficultyPenalty(characterTechnique.technique.difficultyLevel);
    if (characterTechnique.spentPoints < 4)
    {
        return Math.floor(attributeValue + penalty + characterTechnique.spentPoints / 2);
    }

    return Math.floor(attributeValue + penalty + 1 + characterTechnique.spentPoints / 4);
}

export function getCharacterAttribute(character: ICharacter, attributeType: AttributeTypeEnum): ICharacterAttribute
{
    const result = character.attributes.find(a => a.attribute.type === attributeType);
    if(result === undefined)
        throw new Error(`Can't find Character Basic Attributes ${attributeType}`);

    return result
}

export function getCharacterAttributeValue(character: ICharacter, attributeType: AttributeTypeEnum): number
{
    // secondary attributes that are "virtual" and calculates on the fly.
    if(attributeType === AttributeTypeEnum.Parry)
    {
        return _calculateParry(getCharacterAttributeValue(character, AttributeTypeEnum.Dexterity), _calculateEquipmentTotalBonusToAttribute(character.equipments, AttributeTypeEnum.Parry));
    }
    else if(attributeType === AttributeTypeEnum.Dodge)
    {
        let characterSpeed = getCharacterAttributeValue(character, AttributeTypeEnum.BasicSpeed);
        let equipmentBonus = _calculateEquipmentTotalBonusToAttribute(character.equipments, AttributeTypeEnum.Dodge);
        return Math.floor(characterSpeed + 3 + equipmentBonus)
    }

    // primary attributes that are stored as a part of the character and can be increase by points.
    let characterAttribute = getCharacterAttribute(character, attributeType);
    let attributePart: number = 0;
    
    if(characterAttribute.attribute.dependOnAttributeType === null || characterAttribute.attribute.dependOnAttributeType === undefined)
    {
        attributePart = characterAttribute.attribute.defaultValue!;
    }
    else if(characterAttribute.attribute.dependOnAttributeType === AttributeTypeEnum.DexterityAndHealth)
    {
        if(characterAttribute.attribute.type === AttributeTypeEnum.BasicSpeed)
        {
            attributePart = (getCharacterAttributeValue(character, AttributeTypeEnum.Dexterity) + getCharacterAttributeValue(character, AttributeTypeEnum.Health))/4;
        }
        else if(characterAttribute.attribute.type === AttributeTypeEnum.BasicMove)
        {
            attributePart = Math.floor(getCharacterAttributeValue(character, AttributeTypeEnum.BasicSpeed));
        }
        else throw new Error(`Unsupported BasicAttributeTypeEnum.DexterityAndHealth for ${characterAttribute.attribute.type}`);
    }
    else 
    {
        attributePart = getCharacterAttributeValue(character, characterAttribute.attribute.dependOnAttributeType);
    }
    
    const pointsPart = characterAttribute.attribute.valueIncreasePerLevel * Math.floor(characterAttribute.spentPoints/characterAttribute.attribute.pointsCostPerLevel);
    return attributePart + pointsPart;
}

export function getCharacterTraitSpentPoints(trait: ICharacterTrait)
{
    let modifiers = getCharacterTraitSelectedModifiers(trait);
    
    let baseOnly = modifiers.filter(x => x.costAffectType === TraitModifierCostAffectTypeEnum.BaseOnly);
    let levelsOnly = modifiers.filter(x => x.costAffectType === TraitModifierCostAffectTypeEnum.LevelsOnly);
    let total = modifiers.filter(x => x.costAffectType === TraitModifierCostAffectTypeEnum.Total);
    
    let baseCost = calculateModifiersCost(baseOnly, trait.trait.basePointsCost);
    let levelsCost = calculateModifiersCost(levelsOnly, trait.trait.pointsCostPerLevel) * trait.level;
    let spentPoints = calculateModifiersCost(total, baseCost + levelsCost);
    
    return Math.ceil(spentPoints);
}

function calculateModifiersCost(modifiers: Array<ITraitModifier>, startValue: number)
{
    let points = modifiers.filter(x => x.costType === TraitModifierCostTypeEnum.Points);
    let percents = modifiers.filter(x => x.costType === TraitModifierCostTypeEnum.Percentage);
    let multipliers = modifiers.filter(x => x.costType === TraitModifierCostTypeEnum.Multiplier);
    
    let result = points.reduce((accumulator, modifier) => ApplyTraitModifierCost(modifier, accumulator), startValue);
    result = percents.reduce((accumulator, modifier) => ApplyTraitModifierCost(modifier, accumulator), result);
    result = multipliers.reduce((accumulator, modifier) => ApplyTraitModifierCost(modifier, accumulator), result);
    return result;
}

export function getCharacterTraitSelectedModifiers(trait: ICharacterTrait): Array<ITraitModifier>
{
    if(trait.selectedTraitModifiers.length === 0)
    {
        return []
    }
    
    let modifiers = trait.trait.modifiers.filter(modifier => trait.selectedTraitModifiers.some(x => x.traitModifierId === modifier.id));
    return [...modifiers, ...trait.trait.modifierGroups.flatMap(x => x.modifiers).filter(modifier => trait.selectedTraitModifiers.some(x => x.traitModifierId === modifier.id))];
}

export function getSkillDifficultyAbbreviation(difficultyLevel: SkillDifficultyLevelEnum): string
{
    switch (difficultyLevel) {
        case SkillDifficultyLevelEnum.Easy:
            return "E";
        case SkillDifficultyLevelEnum.Average:
            return "A";
        case SkillDifficultyLevelEnum.Hard:
            return "H";
        case SkillDifficultyLevelEnum.VeryHard:
            return "VH";
        case SkillDifficultyLevelEnum.Wildcard:
            return "W";
    }
}

export function getCharacterSkillNameWithOptionalSpeciality(skill: ICharacterSkill): string
{
    if(!hasSpecializationSymbol(skill.skill.specialization))
    {
        return getSkillNameWithSpecialization(skill.skill);
    }
    return `${skill.skill.name} (${skill.optionalSpecialty})`;
}

export function getCharacterTechniqueNameWithOptionalSpeciality(characterTechnique: ICharacterTechnique): string
{
    if(!hasSpecializationSymbol(characterTechnique.technique.name))
        return characterTechnique.technique.name;
    
    return characterTechnique.technique.name.replace(SpecializationReplacementRegExp, characterTechnique.nameSpecialization!);
}

export function getCharacterTechniqueDefaultName(characterTechnique: ICharacterTechnique): string
{
    if(IsUndefinedOrNull(characterTechnique.technique.default.name))
        return getSkillDefaultAsString(characterTechnique.technique.default);
    
    if(!hasSpecializationSymbol(characterTechnique.technique.default.name))
        return characterTechnique.technique.default.name!;

    return characterTechnique.technique.default.name!.replace(SpecializationReplacementRegExp, characterTechnique.defaultNameSpecialization!);
}

export function getCharacterTechniqueRelatedSkill(character: ICharacter, technique: ITechnique): ICharacterSkill|undefined
{
    return character.skills.find(s => s.skill.name === technique.default.name
        && s.skill.specialization === technique.default.specialization);
}

export function getCharacterTraitNameWithOptionalSpeciality(characterTrait: ICharacterTrait|null): string
{
    if(characterTrait === null)
        return "";
    
    if(!hasSpecializationSymbol(characterTrait.trait.name))
        return characterTrait.trait.name;

    return characterTrait.trait.name.replace(SpecializationReplacementRegExp, characterTrait.optionalSpecialty!);
}

export function getCharacterPointsStatistic(character: ICharacter): ICharacterPointsStatistic
{
    if(IsUndefinedOrNull(character))
    {
        throw new Error("getCharacterPointsStatistic(): character cannot be null or undefined!");
    }
    
    let statistic: ICharacterPointsStatistic =
    {
        advantages: 0,
        attributes: 0,
        disadvantages: 0,
        remainingPoints: 0,
        skills: 0,
        techniques: 0,
        totalPoints: character.totalPoints
    }
    
    statistic.skills = character.skills.reduce((sum, skill) => sum + skill.spentPoints, 0);
    statistic.techniques = character.techniques.reduce((sum, technique) => sum + technique.spentPoints, 0);
    statistic.attributes = character.attributes.reduce((sum, attribute) => sum + attribute.spentPoints, 0);
    
    let advantages = character.traits.filter(t => t.trait.tags.some(tag => tag.toLowerCase() === TraitTags.Advantage.toLowerCase()));
    statistic.advantages = advantages.reduce((sum, advantage) => sum + getCharacterTraitSpentPoints(advantage), 0);

    let disadvantages = character.traits.filter(t => t.trait.tags.some(tag => tag.toLowerCase() === TraitTags.Disadvantage.toLowerCase()));
    statistic.disadvantages = disadvantages.reduce((sum, disadvantage) => sum + getCharacterTraitSpentPoints(disadvantage), 0);
    
    statistic.remainingPoints = statistic.totalPoints - statistic.advantages - statistic.attributes - statistic.disadvantages - statistic.skills - statistic.techniques;
    
    return statistic;
}

export function getBestDefaultForCharacter(character: ICharacter, skillDefaults: ISkillDefault[]): ISkillDefault
{
   let result = skillDefaults.reduce((max, current) => {
       return getSkillDefaultValueForCharacter(character, current)! > getSkillDefaultValueForCharacter(character, max)! ? current : max;
   }, skillDefaults[0]);
   
   return result;
}

export function getSkillDefaultValueForCharacter(character: ICharacter, skillDefault: ISkillDefault): number|undefined
{
    if(!IsUndefinedOrNull(skillDefault.attributeType))
    {
        let attributeValue = getCharacterAttributeValue(character, skillDefault.attributeType!)
        return attributeValue + skillDefault.modifier;
    }
    
   if(!IsUndefinedOrNull(skillDefault.name))
   {
       let chSkill = findCharacterSkillBySkillDefault(character, skillDefault);
       if(chSkill === undefined)
       {
           return chSkill;
       }
       
       return getCharacterSkillLevel(character, chSkill) + skillDefault.modifier;
   }
}

export function findCharacterSkillBySkillDefault(character: ICharacter, skillDefault: ISkillDefault): ICharacterSkill|undefined
{
    let skill = undefined;
    if(!IsUndefinedOrNull(skillDefault.name))
    {
        if(!IsUndefinedOrNull(skillDefault.specialization))
        {
            skill = character.skills.find(chSkill => chSkill.skill.name === skillDefault.name && chSkill.optionalSpecialty)
        }
        skill = character.skills.find(chSkill => chSkill.skill.name === skillDefault.name)
    }

    return skill;
}

export function _addCharacterSkillImpl(state: SagaGuideCurrentCharacterState, newSkill: ISkill, optionSpecialization: string)
{
    if(state.character === null)
        return;

    let sameSkills = state.character.skills.filter(s => s.skill.id === newSkill.id);
    if(sameSkills.some( skill => skill.optionalSpecialty === optionSpecialization))
    {
        state.toast = constructWarningToast(`${newSkill.name} skill is already added`);
        return;
    }

    const characterSkill: ICharacterSkill =
        {
            id: uuidv4(),
            optionalSpecialty: optionSpecialization,
            skill: newSkill,
            spentPoints: 1,
            defaultedFrom: newSkill.defaults.find(d => d.attributeType !== null)!,
        };
    
    state.character.skills.push(characterSkill);
    state.toast = constructInfoToast(`${characterSkill.skill.name} skill was added`);
}

export function _removeCharacterSkillByIdImpl(state: SagaGuideCurrentCharacterState, characterSkillId: string)
{
    if(state.character === null)
        return;

    state.character.skills = state.character.skills.filter(s => s.id !== characterSkillId);
}

export function _addCharacterTraitImpl(state: SagaGuideCurrentCharacterState, newTrait: ITrait, optionSpecialization: string, modifiers: ITraitModifier[])
{
    if(state.character === null)
        return;

    let sameTraits = state.character.traits.filter(t => t.trait.id === newTrait.id);
    if(sameTraits.some( trait => trait.optionalSpecialty === optionSpecialization))
    {
        state.toast = constructWarningToast(`${newTrait.name} trait is already added`);
        return;
    }
    
    const characterTrait: ICharacterTrait =
        {
            id: uuidv4(),
            optionalSpecialty: optionSpecialization,
            trait: newTrait,
            level: 1,
            selectedTraitModifiers: modifiers.map(modifier => ({traitModifierId: modifier.id, traitModifierLevel: 1}))
        };
    
    state.character.traits.push(characterTrait);
    state.toast = constructInfoToast(`${newTrait.name} trait was added`);
}

export function _changeCharacterTechniquePointsImpl(state: SagaGuideCurrentCharacterState, techniqueId: string, newPoints: number)
{
    if(state.character === null)
        return;

    const technique = state.character.techniques.find(technique => technique.id === techniqueId);

    if(technique === undefined)
        throw new Error(`Can't find character skill with id: ${techniqueId}`);

    technique.spentPoints = newPoints;
}

export function _removeCharacterTraitByIdImpl(state: SagaGuideCurrentCharacterState, characterTraitId: string)
{
    if(state.character === null)
        return;
    
    state.character.traits = state.character.traits.filter(s => s.id !== characterTraitId);
}

export function _addCharacterTechniqueImpl(state: SagaGuideCurrentCharacterState, newTechnique: ITechnique, nameSpecialization: string|null, defaultNameSpecialization: string|null)
{
    if(state.character === null)
        return;

    let skill = getCharacterTechniqueRelatedSkill(state.character, newTechnique);
    if(IsUndefinedOrNull(skill))
    {
        state.toast = constructErrorToast(`Skill ${newTechnique.default.name} ${newTechnique.default.specialization ? `with ${newTechnique.default.specialization} specialization` : ''} is required!`);
        return;
    }
    
    let sameTechniques = state.character.techniques.filter(t => t.technique.id === newTechnique.id);
    if(sameTechniques.some( trait => trait.nameSpecialization === nameSpecialization && trait.defaultNameSpecialization ===defaultNameSpecialization))
    {
        state.toast = constructWarningToast(`${newTechnique.name} technique is already added`);
        return;
    }
    
    const characterTechnique: ICharacterTechnique =
        {
            id: uuidv4(),
            technique: newTechnique,
            spentPoints: 1,
            nameSpecialization: nameSpecialization,
            defaultNameSpecialization: defaultNameSpecialization
        };

    state.character.techniques.push(characterTechnique);
    state.toast = constructInfoToast(`${newTechnique.name} technique was added`);
}

export function _removeCharacterTechniqueByIdImpl(state: SagaGuideCurrentCharacterState, characterTechniqueId: string)
{
    if(state.character === null)
        return;

    state.character.techniques = state.character.techniques.filter(s => s.id !== characterTechniqueId);
}

export function _addCharacterEquipmentImpl(state: SagaGuideCurrentCharacterState, newEquipment: IEquipment)
{
    if(state.character === null)
        return;
    
    const characterEquipment: ICharacterEquipment =
        {
            id: uuidv4(),
            isEquipped: true,
            quantity: 1,
            equipment: newEquipment,
            leftUses: newEquipment.maxUses,
            selectedModifiers: [],
        };

    state.character.equipments.push(characterEquipment);
    state.toast = constructInfoToast(`${newEquipment.name} equipment was added`);
}

export function _removeCharacterEquipmentByIdImpl(state: SagaGuideCurrentCharacterState, characterEquipmentId: string)
{
    if(state.character === null)
        return;

    state.character.equipments = state.character.equipments.filter(e => e.id !== characterEquipmentId);
}

export function _equipCharacterEquipmentByIdImpl(state: SagaGuideCurrentCharacterState, characterEquipmentId: string, isEquipped: boolean)
{
    if(state.character === null)
        return;

    let equipment = state.character.equipments.find(e => e.id === characterEquipmentId);
    
    if(IsUndefinedOrNull(equipment))
    {
        console.error(`Didn't find character equipmend with ID ${characterEquipmentId} for character ID ${state.character.id}`);
        return;
    }
    
    equipment!.isEquipped = isEquipped;
}

export function _calculateParry(skillLevel: number, modifier: number)
{
    return Math.floor(3 + skillLevel! / 2 + modifier);
}

export function _calculateEquipmentTotalBonusToAttribute(equipments: ICharacterEquipment[], attributeType: AttributeTypeEnum) {
    let equipped = equipments.filter(eq => eq.isEquipped
        && eq.equipment.features.some(f => isIAttributeBonusFeature(f))
    );

    let features = equipped.map(eq => eq.equipment.features.filter(f => isIAttributeBonusFeature(f)
        && f.attributeType === attributeType)).flat() as IAttributeBonusFeature[];

    return features.reduce((accumulator, feature) => {
        return accumulator += feature.amount;
    }, 0);
}