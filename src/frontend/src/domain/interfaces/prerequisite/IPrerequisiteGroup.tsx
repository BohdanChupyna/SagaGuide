import {INumericCriteria} from "./INumericCriteria";
import {AttributeTypeEnum} from "../attribute/IAttribute";
import {IStringCriteria} from "./IStringCriteria";

export interface IPrerequisiteGroup
{
    whenTechLevel: INumericCriteria|null,
    shouldAllBeSatisfied: boolean,
    prerequisites: IPrerequisiteBase []
}

export enum PrerequisiteTypeEnum
{
    Attribute = "Attribute",
    ContainedWeight = "ContainedWeight",
    ContainedQuantity = "ContainedQuantity",
    Skill = "Skill",
    Spell = "Spell",
    Trait = "Trait",
    Group = "Group",
}

export interface IPrerequisiteBase
{
    shouldBe: boolean,
    prerequisiteType: PrerequisiteTypeEnum
}

export interface IAttributePrerequisite extends IPrerequisiteBase
{
    qualifierCriteria: INumericCriteria,
    required: AttributeTypeEnum,
    combinedWith: AttributeTypeEnum|null
}

export interface ISkillPrerequisite extends IPrerequisiteBase
{
    nameCriteria: IStringCriteria,
    levelCriteria: INumericCriteria|null,
    specializationCriteria: IStringCriteria|null
}

export interface ITraitPrerequisite extends IPrerequisiteBase
{
    nameCriteria: IStringCriteria,
    levelCriteria: INumericCriteria|null,
    notesCriteria: IStringCriteria|null
}
