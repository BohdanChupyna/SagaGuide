import {IAuditable} from "../IGuid";
import {IPrerequisiteGroup} from "../prerequisite/IPrerequisiteGroup";
import IBookReference from "../IBookReference";
import {ISkillDefault, SkillDifficultyLevelEnum} from "../skill/ISkill";

export interface ITechnique extends IAuditable
{
    name: string,
    tags: string [],
    default: ISkillDefault,
    pointsCost: number,
    prerequisites: IPrerequisiteGroup|null,
    bookReferences: Array<IBookReference>,
    techniqueLimitModifier: number|null,
    difficultyLevel: SkillDifficultyLevelEnum
}