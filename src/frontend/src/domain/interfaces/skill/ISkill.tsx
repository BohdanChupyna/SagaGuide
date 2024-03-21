import {IAuditable} from "../IGuid";
import {AttributeTypeEnum, getAttributeAbbreviation} from "../attribute/IAttribute";
import IBookReference from "../IBookReference";
import {IPrerequisiteGroup} from "../prerequisite/IPrerequisiteGroup";
import {IsUndefinedOrNull} from "../../commonUtils";
import {numberToSignString} from "../../stringUtils";
import exp from "constants";

export interface ISkill extends IAuditable
{
    name: string,
    tags: string [],
    specialization: string|null,
    localNotes: string|null,
    defaults: ISkillDefault [],
    modifiers: string,
    techLevel: number|null,
    attributeType: AttributeTypeEnum,
    pointsCost: number,
    encumbrancePenaltyMultiplier: number|null,

    difficultyLevel: SkillDifficultyLevelEnum,
    prerequisites: IPrerequisiteGroup|null,
    bookReferences: IBookReference [],
}

export enum SkillDifficultyLevelEnum
{
    Easy = "Easy",
    Average = "Average",
    Hard = "Hard",
    VeryHard = "VeryHard",
    Wildcard = "Wildcard",
}

export interface ISkillDefault
{
    attributeType: AttributeTypeEnum|null,
    name: string|null,
    specialization: string|null,
    modifier: number,
    level: number|null,
    adjustedLevel: number|null,
    points: number|null,
}

export function getSkillDefaultsAsString(defaults: ISkillDefault[])
{
    if(IsUndefinedOrNull(defaults))
    {
        throw new Error("Skill must have at least one SkillDefault");
    }
    
    let result = "";
    for(let i = 0; i <defaults.length; ++i)
    {
        result += getSkillDefaultAsString(defaults[i]);
        if(i !== (defaults.length-1))
        {
            result += ", ";
        }
    }
    
    return result;
}

export function getSkillDefaultAsString(skillDefault: ISkillDefault)
{
    let result = "";
    
    if(!IsUndefinedOrNull(skillDefault.name))
    {
        result += skillDefault.name;

        if(!IsUndefinedOrNull(skillDefault.specialization))
        {
            result += ` (${skillDefault.specialization})`;
        }
    }
    else
    {
        result += `${getAttributeAbbreviation(skillDefault.attributeType!)}`;
        
    }
    
    result += numberToSignString(skillDefault.modifier);
    return result;
}

export function getSkillNameWithSpecialization(skill: ISkill)
{
    return  `${skill.name} ${ skill.specialization ? `(${skill.specialization})` : ''}`;
}