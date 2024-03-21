import IGuid, {IAuditable} from "../IGuid";
import ITrait from "../trait/ITrait";
import {IAttribute} from "../attribute/IAttribute";
import {ISkill, ISkillDefault} from "../skill/ISkill";
import {ITechnique} from "../technique/ITechnique";
import * as React from "react";
import {IEquipment, IEquipmentModifier} from "../equipment/IEquipment";

export interface ICharacter extends IAuditable
{
    userId: string,
    name: string,
    player: string,
    campaign: string,
    title: string,
    handedness: string,
    gender: string,
    race: string,
    religion: string,
    age: number,
    height: number,
    weight: number,
    techLevel: number,
    size: number,
    hpLose: number,
    fpLose: number,
    totalPoints: number,
    version: number,

    attributes: ICharacterAttribute[],
    traits: ICharacterTrait [],
    skills: ICharacterSkill[],
    techniques: ICharacterTechnique[],
    equipments: ICharacterEquipment[],
}

export interface ICharacterInfo extends IAuditable 
{
    name: string,
    player: string,
    campaign: string,
    title: string,
}

export interface ICharacterAttribute extends IGuid
{
    attribute: IAttribute,
    spentPoints: number,
}

export interface ICharacterTrait extends IGuid
{
    trait: ITrait
    optionalSpecialty: string|null,
    level: number,
    selectedTraitModifiers: ICharacterTraitModifier[]
}

export interface ICharacterTraitModifier
{
    traitModifierId: string,
    traitModifierLevel: number,
}


export interface ICharacterSkill extends IGuid
{
    skill: ISkill
    spentPoints: number,
    //if skill.specialization includes @ symbol then user must provide ICharacterSkill.optionalSpecialty
    optionalSpecialty: string|null,
    defaultedFrom: ISkillDefault,
}

export interface ICharacterTechnique extends IGuid
{
    technique: ITechnique
    spentPoints: number,
    nameSpecialization: string|null,
    defaultNameSpecialization: string|null,
}

export interface ICharacterPointsStatistic
{
    totalPoints: number,
    remainingPoints: number,
    attributes: number,
    skills: number,
    techniques: number,
    advantages: number,
    disadvantages: number,
}

export interface ICharacterEquipment extends IGuid
{
    equipment: IEquipment,
    selectedModifiers: IEquipmentModifier [],
    quantity: number,
    isEquipped: boolean,
    leftUses: number|null,
}
