import React from 'react'
import {ICharacterTechnique, ICharacterTrait} from "./ICharacter";
import {SkillDifficultyLevelEnum} from "../skill/ISkill";
import {
    getCharacterTechniqueDefaultName,
    getCharacterTechniqueNameWithOptionalSpeciality,
    getCharacterTraitSpentPoints
} from "./characterDomainLogicHelper";
import {
    SelfControlRollAdjustmentEnum,
    TraitModifierCostAffectTypeEnum,
    TraitModifierCostTypeEnum
} from "../trait/ITrait";


test('getCharacterTechniqueName returns correct name with custom specialization', async () => {
    let characterTechnique: ICharacterTechnique = 
    {
        defaultNameSpecialization: null,
        id: "",
        nameSpecialization: "Cats",
        spentPoints: 0,
        technique: {
            name: "Pet (@Custom Name@)",
            tags: [],
            default: {
                attributeType: null,
                name: null,
                specialization: null,
                modifier: 1,
                level: null,
                adjustedLevel: null,
                points: null,
            },
            pointsCost: 1,
            prerequisites: null,
            bookReferences: [],
            techniqueLimitModifier: null,
            difficultyLevel: SkillDifficultyLevelEnum.Easy,
            createdBy: "",
            CreatedOn: "",
            ModifiedBy: "",
            ModifiedOn: "",
            id: "",
        }
    }

    let result = getCharacterTechniqueNameWithOptionalSpeciality(characterTechnique);
    
    expect(result).toBe("Pet (Cats)");
})

test('getCharacterTechniqueDefaultName returns correct name with custom specialization', async () => {
    let characterTechnique: ICharacterTechnique =
        {
            defaultNameSpecialization: "Dogs",
            id: "",
            nameSpecialization: "Cats",
            spentPoints: 0,
            technique: {
                name: "Pet (@Custom@)",
                tags: [],
                default: {
                    attributeType: null,
                    name: "Hold (@Specialization@)",
                    specialization: null,
                    modifier: 1,
                    level: null,
                    adjustedLevel: null,
                    points: null,
                },
                pointsCost: 1,
                prerequisites: null,
                bookReferences: [],
                techniqueLimitModifier: null,
                difficultyLevel: SkillDifficultyLevelEnum.Easy,
                createdBy: "",
                CreatedOn: "",
                ModifiedBy: "",
                ModifiedOn: "",
                id: "",
            }
        }

    let result = getCharacterTechniqueDefaultName(characterTechnique);

    expect(result).toBe("Hold (Dogs)");
})

test('getCharacterTraitSpentPoints returns correct total cost', async () => {
    let characterTrait: ICharacterTrait =
        {
            id: "",
            level: 3,
            optionalSpecialty: null,
            selectedTraitModifiers: [
                {
                    traitModifierId: "1",
                    traitModifierLevel: 1,
                },
                {
                    traitModifierId: "2",
                    traitModifierLevel: 1,
                },
                {
                    traitModifierId: "3",
                    traitModifierLevel: 1,
                },
                {
                    traitModifierId: "4",
                    traitModifierLevel: 1,
                },
                {
                    traitModifierId: "5",
                    traitModifierLevel: 1,
                }
            ],
            trait: {
                name: 'Test Trait',
                localNotes: null,
                tags: [],
                prerequisites: null,
                pointsCostPerLevel: 4,
                basePointsCost: 10,
                selfControlRoll: 0,
                canLevel: true,
                roundCostDown: false,
                selfControlRollAdjustment: SelfControlRollAdjustmentEnum.None,
                features: [],
                modifiers: [
                    {
                        name: 'points total',
                        localNotes: null,
                        tags: [],
                        bookReferences: [],
                        features: [],
                        pointsCost: 7,
                        costType: TraitModifierCostTypeEnum.Points,
                        costAffectType: TraitModifierCostAffectTypeEnum.Total,
                        canLevel: false,
                        id: '1'
                    },
                    {
                        name: 'multiplier total',
                        localNotes: null,
                        tags: [],
                        bookReferences: [],
                        features: [],
                        pointsCost: 2,
                        costType: TraitModifierCostTypeEnum.Multiplier,
                        costAffectType: TraitModifierCostAffectTypeEnum.Total,
                        canLevel: false,
                        id: '2'
                    },
                    {
                        name: 'points total',
                        localNotes: null,
                        tags: [],
                        bookReferences: [],
                        features: [],
                        pointsCost: 3,
                        costType: TraitModifierCostTypeEnum.Points,
                        costAffectType: TraitModifierCostAffectTypeEnum.Total,
                        canLevel: false,
                        id: '3'
                    },
                    {
                        name: 'Percentage BaseOnly',
                        localNotes: null,
                        tags: [],
                        bookReferences: [],
                        features: [],
                        pointsCost: 50,
                        costType: TraitModifierCostTypeEnum.Percentage,
                        costAffectType: TraitModifierCostAffectTypeEnum.BaseOnly,
                        canLevel: false,
                        id: '4'
                    }
                ],
                modifierGroups: [
                    {
                        name: 'used group',
                        bookReferences: [],
                        modifiers: [
                            {
                                name: 'Multiplier LevelsOnly',
                                localNotes: null,
                                tags: [],
                                bookReferences: [],
                                features: [],
                                pointsCost: 2,
                                costType: TraitModifierCostTypeEnum.Multiplier,
                                costAffectType: TraitModifierCostAffectTypeEnum.LevelsOnly,
                                canLevel: false,
                                id: '5'
                            }
                        ],
                        id: ''
                    },
                    {
                        name: 'unused group',
                        bookReferences: [],
                        modifiers: [
                            {
                                name: '',
                                localNotes: null,
                                tags: [],
                                bookReferences: [],
                                features: [],
                                pointsCost: 50,
                                costType: TraitModifierCostTypeEnum.Percentage,
                                costAffectType: TraitModifierCostAffectTypeEnum.BaseOnly,
                                canLevel: false,
                                id: ''
                            }
                        ],
                        id: ''
                    }
                ],
                bookReferences: [],
                createdBy: '',
                CreatedOn: '',
                ModifiedBy: '',
                ModifiedOn: '',
                id: ''
            }


        }
        
    expect(getCharacterTraitSpentPoints(characterTrait)).toBe(98);
})