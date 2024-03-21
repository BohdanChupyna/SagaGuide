import IGuid, {IAuditable} from "../IGuid";
import IBookReference from "../IBookReference";
import {IPrerequisiteGroup} from "../prerequisite/IPrerequisiteGroup";
import {IFeature} from "../feature/IFeature";
import {numberToSignString} from "../../stringUtils";
import {IsUndefinedOrNull} from "../../commonUtils";


export enum SelfControlRollAdjustmentEnum
{
    None = "None",
    ActionPenalty = "ActionPenalty",
    ReactionPenalty = "ReactionPenalty",
    FrightCheckPenalty = "FrightCheckPenalty",
    FrightCheckBonus = "FrightCheckBonus",
    MinorCostOfLivingIncrease = "MinorCostOfLivingIncrease",
    MajorCostOfLivingIncrease = "MajorCostOfLivingIncrease",
}

export default interface ITrait extends IAuditable
{
    name: string,
    localNotes: string|null,
    tags: Array<string>,
    prerequisites: IPrerequisiteGroup|null,
    pointsCostPerLevel: number,
    basePointsCost: number,
    selfControlRoll: number,
    canLevel: boolean,
    roundCostDown: boolean,
    selfControlRollAdjustment: SelfControlRollAdjustmentEnum,
    features: Array<IFeature>,
    modifiers: Array<ITraitModifier>,
    modifierGroups: Array<ITraitModifierGroup>,
    bookReferences: Array<IBookReference>,
}

export enum TraitModifierCostTypeEnum
{
    Percentage = "Percentage",
    Points = "Points",
    Multiplier = "Multiplier",
}

export enum TraitModifierCostAffectTypeEnum
{
    Total = "Total",
    BaseOnly = "BaseOnly",
    LevelsOnly = "LevelsOnly",
}

export enum TraitTags
{
    Advantage = "Advantage",
    Disadvantage = "Disadvantage",
    Mental = "Mental",
    Physical = "Physical",
    Social = "Social",
    Exotic = "Exotic",
    Supernatural = "Supernatural",
}

export interface ITraitModifier extends IGuid
{
    name: string,
    localNotes: string|null,
    tags: Array<string>,
    bookReferences: Array<IBookReference>,
    features: Array<IFeature>,
    pointsCost: number,
    costType: TraitModifierCostTypeEnum,
    costAffectType: TraitModifierCostAffectTypeEnum,
    canLevel: boolean,
}

export interface ITraitModifierGroup extends IGuid
{
    name: string,
    bookReferences: Array<IBookReference>,
    modifiers: Array<ITraitModifier>,
}

export function sortTraitModifiersByPointsCost(modifiers: ITraitModifier[]): ITraitModifier[]
{
    return [...modifiers].sort((a, b) => a.pointsCost - b.pointsCost);
}

export function TraitModifierPointsCostToString(modifier: ITraitModifier): string
{
    switch (modifier.costType) {
        case TraitModifierCostTypeEnum.Percentage:
            return `${numberToSignString(modifier.pointsCost)}%`;
        case TraitModifierCostTypeEnum.Points:
            return `${numberToSignString(modifier.pointsCost)} pt`;
        case TraitModifierCostTypeEnum.Multiplier:
            return `x${modifier.pointsCost}`;
    }
}

export function ApplyTraitModifierCost(modifier: ITraitModifier, totalCost: number): number
{
    switch (modifier.costType) {
        case TraitModifierCostTypeEnum.Percentage:
            return totalCost + (totalCost * modifier.pointsCost / 100);
        case TraitModifierCostTypeEnum.Points:
            return totalCost + modifier.pointsCost;
        case TraitModifierCostTypeEnum.Multiplier:
            return totalCost * modifier.pointsCost;
    }
}
