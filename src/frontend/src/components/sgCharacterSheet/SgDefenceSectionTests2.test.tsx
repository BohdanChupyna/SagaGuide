import React from 'react'
import {rest} from 'msw'
import {setupServer} from 'msw/node'
import {act, screen} from '@testing-library/react'
// We're using our own custom render function and not RTL's render.
import {ExtendedRenderOptions, FrodoCharacterPage, renderWithProviders} from "../../Tests/testUtils";
import {charactersUrl} from "../../redux/slices/api/apiSlice";
import {ICharacter} from "../../domain/interfaces/character/ICharacter";
import {frodoCharacter} from "../../Tests/characterTestData";
import {SgCharacterSheetAriaLabels} from "./SgCharacterSheetAriaLabels";
import {AttributeTypeEnum} from "../../domain/interfaces/attribute/IAttribute";
import {
    getCharacterBlock,
    getCharacterDodge,
    getCharacterParry,
    IActiveDefenceData
} from "../../domain/interfaces/character/characterActiveDefenceHelper";
import userEvent from "@testing-library/user-event";
import {AppStore, setupStore} from "../../redux/store";
import {changeCurrentCharacter} from "../../redux/slices/currentCharacter/currentCharacterSlice";
import fs from "fs";

const jsonData =
    {
        name: 'Frodo',
        player: 'Mike',
        campaign: 'MiddleEarth',
        title: 'Sneaky hobbit',
        handedness: 'right',
        gender: 'make',
        race: 'hobbit',
        religion: 'none',
        age: 33,
        height: 150,
        weight: 40,
        techLevel: 7,
        size: 2,
        hpLose: 0,
        fpLose: 0,
        totalPoints: 0,
        attributes: [
            {
                attribute: {
                    type: 'BasicMove',
                    pointsCostPerLevel: 5,
                    valueIncreasePerLevel: 1,
                    dependOnAttributeType: 'DexterityAndHealth',
                    bookReference: {
                        pageNumber: 17,
                        sourceBook: 'BasicSet'
                    },
                    id: '04422688-c74c-4e0a-a4d1-be2f27f3c16d'
                },
                spentPoints: 0,
                id: 'b3ce392b-8a19-490d-bc6a-94e566301625'
            },
            {
                attribute: {
                    defaultValue: 10,
                    type: 'Strength',
                    pointsCostPerLevel: 10,
                    valueIncreasePerLevel: 1,
                    bookReference: {
                        pageNumber: 14,
                        sourceBook: 'BasicSet'
                    },
                    id: '053a5747-aad1-402b-9a5b-e8a8b770fa8f'
                },
                spentPoints: 0,
                id: '2a56344e-8b13-4976-89de-c8feb31a2b26'
            },
            {
                attribute: {
                    defaultValue: 10,
                    type: 'Health',
                    pointsCostPerLevel: 10,
                    valueIncreasePerLevel: 1,
                    bookReference: {
                        pageNumber: 15,
                        sourceBook: 'BasicSet'
                    },
                    id: '0f6b9935-b6ee-4329-9c65-d5172461fe1f'
                },
                spentPoints: 0,
                id: '524fdc61-a793-4e4b-8cfc-55eb9cf67146'
            },
            {
                attribute: {
                    type: 'BasicSpeed',
                    pointsCostPerLevel: 5,
                    valueIncreasePerLevel: 0.25,
                    dependOnAttributeType: 'DexterityAndHealth',
                    bookReference: {
                        pageNumber: 17,
                        sourceBook: 'BasicSet'
                    },
                    id: '22dc26b6-690a-4e02-8b7e-7c491bfdd1e3'
                },
                spentPoints: 0,
                id: 'fdd69de2-5f1e-43e8-a885-73d68193868e'
            },
            {
                attribute: {
                    type: 'Perception',
                    pointsCostPerLevel: 5,
                    valueIncreasePerLevel: 1,
                    dependOnAttributeType: 'Intelligence',
                    bookReference: {
                        pageNumber: 16,
                        sourceBook: 'BasicSet'
                    },
                    id: '2a208147-6ab9-4776-abac-a3f8b11ee724'
                },
                spentPoints: 0,
                id: 'd1a1796c-1995-4b2b-8aa7-70b060ab765d'
            },
            {
                attribute: {
                    type: 'HitPoints',
                    pointsCostPerLevel: 2,
                    valueIncreasePerLevel: 1,
                    dependOnAttributeType: 'Strength',
                    bookReference: {
                        pageNumber: 16,
                        sourceBook: 'BasicSet'
                    },
                    id: '2d3328b4-b808-4960-b42e-46c0840b36c6'
                },
                spentPoints: 0,
                id: 'a5721a45-46b9-4274-aaeb-82ef6903e161'
            },
            {
                attribute: {
                    defaultValue: 10,
                    type: 'Dexterity',
                    pointsCostPerLevel: 20,
                    valueIncreasePerLevel: 1,
                    bookReference: {
                        pageNumber: 15,
                        sourceBook: 'BasicSet'
                    },
                    id: '5d38d3a9-e454-41b6-9387-e89ad557ca14'
                },
                spentPoints: 0,
                id: 'c071215d-8be3-4a9b-9638-2793628a2db3'
            },
            {
                attribute: {
                    type: 'FatiguePoints',
                    pointsCostPerLevel: 3,
                    valueIncreasePerLevel: 1,
                    dependOnAttributeType: 'Health',
                    bookReference: {
                        pageNumber: 16,
                        sourceBook: 'BasicSet'
                    },
                    id: '7bb8d32c-ccdd-4527-a967-1f90a9e72d5c'
                },
                spentPoints: 0,
                id: '9477cabb-e036-4f6f-a5ce-a6d52cbf5186'
            },
            {
                attribute: {
                    defaultValue: 10,
                    type: 'Intelligence',
                    pointsCostPerLevel: 20,
                    valueIncreasePerLevel: 1,
                    bookReference: {
                        pageNumber: 15,
                        sourceBook: 'BasicSet'
                    },
                    id: '7e87f715-c559-484d-93da-75ff68b0dbee'
                },
                spentPoints: 0,
                id: '23530912-38e6-4922-b18a-df3b66ff6f56'
            },
            {
                attribute: {
                    type: 'Will',
                    pointsCostPerLevel: 5,
                    valueIncreasePerLevel: 1,
                    dependOnAttributeType: 'Intelligence',
                    bookReference: {
                        pageNumber: 16,
                        sourceBook: 'BasicSet'
                    },
                    id: 'f32ee13d-89c2-45cd-9d05-16344d4490fd'
                },
                spentPoints: 0,
                id: 'b093c8ab-7ea1-442f-8beb-ce947a19ccda'
            }
        ],
        skills: [
            {
                skill: {
                    name: 'Judo',
                    tags: [
                        'Combat',
                        'Melee Combat',
                        'Weapon'
                    ],
                    difficultyLevel: 'Hard',
                    pointsCost: 1,
                    encumbrancePenaltyMultiplier: 1,
                    defaults: [],
                    attributeType: 'Dexterity',
                    bookReferences: [
                        {
                            pageNumber: 203,
                            sourceBook: 'BasicSet'
                        },
                        {
                            pageNumber: 57,
                            sourceBook: 'MartialArts'
                        }
                    ],
                    localNotes: 'Allows parrying two different attacks per turn, one with each hand.',
                    createdBy: '00000000-0000-0000-0000-000000000000',
                    createdOn: '2023-10-29T19:51:05.807373Z',
                    modifiedBy: '00000000-0000-0000-0000-000000000000',
                    modifiedOn: '2023-10-29T19:51:05.8073732Z',
                    id: '5ac0e358-c9bb-486e-bcaa-b56d2e1afacc'
                },
                spentPoints: 8,
                id: '4111d1f8-71ef-4121-8482-ec568540b5a7'
            },
            {
                skill: {
                    name: 'Axe/Mace',
                    tags: [
                        'Combat',
                        'Melee Combat',
                        'Weapon'
                    ],
                    difficultyLevel: 'Average',
                    pointsCost: 1,
                    encumbrancePenaltyMultiplier: 1,
                    defaults: [
                        {
                            attributeType: 'Dexterity',
                            modifier: -5
                        },
                        {
                            name: 'Two-Handed Axe/Mace',
                            modifier: -3
                        },
                        {
                            name: 'Flail',
                            modifier: -4
                        }
                    ],
                    attributeType: 'Dexterity',
                    bookReferences: [
                        {
                            pageNumber: 208,
                            sourceBook: 'BasicSet'
                        }
                    ],
                    createdBy: '00000000-0000-0000-0000-000000000000',
                    createdOn: '2023-10-29T19:51:05.814298',
                    modifiedBy: '00000000-0000-0000-0000-000000000000',
                    modifiedOn: '2023-10-29T19:51:05.814298',
                    id: '88c17f64-d52b-4db8-ace5-9efe1832b4b5'
                },
                spentPoints: 1,
                optionalSpecialty: '',
                defaultedFrom: {
                    attributeType: 'Dexterity',
                    modifier: -5
                },
                id: '5a85d1ad-c1dc-4891-8f6f-443d4e7c343a'
            }
        ],
        techniques: [],
        traits: [],
        equipments: [
            {
                selectedModifiers: [],
                equipment: {
                    name: 'Throwing Axe',
                    bookReferences: [
                        {
                            pageNumber: 271,
                            sourceBook: 'BasicSet'
                        }
                    ],
                    techLevel: '0',
                    cost: 60,
                    weight: '4 lb',
                    tags: [
                        'Melee Weapon',
                        'Missile Weapon'
                    ],
                    features: [],
                    attacks: [
                        {
                            $type: 'SagaGuide.Core.Domain.EquipmentAggregate.MeleeAttack, SagaGuide.Core',
                            reach: '1',
                            parry: '0U',
                            block: 'No',
                            id: '8eaf7588-b9f4-4641-a762-9012cf3a039b',
                            damage: {
                                damageType: 'cut',
                                attackType: 'sw',
                                baseDamage: '2'
                            },
                            minimumStrength: '11',
                            usage: 'Swung',
                            defaults: [
                                {
                                    attributeType: 'Dexterity',
                                    modifier: -5
                                },
                                {
                                    name: 'Axe/Mace',
                                    modifier: 0
                                },
                                {
                                    name: 'Flail',
                                    modifier: -4
                                },
                                {
                                    name: 'Two-Handed Axe/Mace',
                                    modifier: -3
                                }
                            ]
                        },
                        {
                            $type: 'SagaGuide.Core.Domain.EquipmentAggregate.RangedAttack, SagaGuide.Core',
                            accuracy: '2',
                            range: 'x1/x1.5',
                            rateOfFire: '1',
                            shots: 'T(1)',
                            bulk: '-3',
                            id: '5d2a970a-01a3-4ef9-9090-3e81835b4e95',
                            damage: {
                                damageType: 'cut',
                                attackType: 'sw',
                                baseDamage: '2'
                            },
                            minimumStrength: '11',
                            usage: 'Thrown',
                            defaults: [
                                {
                                    attributeType: 'Dexterity',
                                    modifier: -4
                                },
                                {
                                    name: 'Thrown Weapon',
                                    specialization: 'Axe/Mace',
                                    modifier: 0
                                }
                            ]
                        }
                    ],
                    modifiers: [],
                    ignoreWeightForSkills: false,
                    createdOn: '2023-10-29T19:51:05.983832',
                    createdBy: '00000000-0000-0000-0000-000000000000',
                    modifiedOn: '2023-10-29T19:51:05.983832',
                    modifiedBy: '00000000-0000-0000-0000-000000000000',
                    id: '244ed05d-f5c4-452f-aded-88f092e6fa09'
                },
                quantity: 1,
                isEquipped: true,
                id: '586f04e7-aef4-46fc-9dfd-18b3e082039d'
            },
            {
                selectedModifiers: [],
                equipment: {
                    name: 'Scale Leggings',
                    bookReferences: [
                        {
                            pageNumber: 283,
                            sourceBook: 'BasicSet'
                        }
                    ],
                    techLevel: '2',
                    legalityClass: '3',
                    cost: 250,
                    weight: '21 lb',
                    tags: [
                        'Limb Armor'
                    ],
                    features: [
                        {
                            $type: 'SagaGuide.Core.Domain.Features.DamageReductionBonusFeature, SagaGuide.Core',
                            featureType: 'DamageReductionBonus',
                            location: 'leg',
                            amount: 4,
                            isScalingWithLevel: false
                        }
                    ],
                    attacks: [],
                    modifiers: [],
                    ignoreWeightForSkills: false,
                    createdOn: '2023-10-29T19:51:05.978714',
                    createdBy: '00000000-0000-0000-0000-000000000000',
                    modifiedOn: '2023-10-29T19:51:05.978714',
                    modifiedBy: '00000000-0000-0000-0000-000000000000',
                    id: 'f1ab1686-327b-42b5-9393-8b94d33cdc63'
                },
                quantity: 1,
                isEquipped: true,
                id: 'edd527c5-a3f3-4246-b9e2-98ae56337b97'
            },
            {
                selectedModifiers: [],
                equipment: {
                    name: 'Scale Armor',
                    bookReferences: [
                        {
                            pageNumber: 283,
                            sourceBook: 'BasicSet'
                        }
                    ],
                    techLevel: '2',
                    cost: 420,
                    weight: '35 lb',
                    tags: [
                        'Body Armor'
                    ],
                    features: [
                        {
                            $type: 'SagaGuide.Core.Domain.Features.DamageReductionBonusFeature, SagaGuide.Core',
                            featureType: 'DamageReductionBonus',
                            location: 'torso',
                            amount: 4,
                            isScalingWithLevel: false
                        },
                        {
                            $type: 'SagaGuide.Core.Domain.Features.DamageReductionBonusFeature, SagaGuide.Core',
                            featureType: 'DamageReductionBonus',
                            location: 'vitals',
                            amount: 4,
                            isScalingWithLevel: false
                        },
                        {
                            $type: 'SagaGuide.Core.Domain.Features.DamageReductionBonusFeature, SagaGuide.Core',
                            featureType: 'DamageReductionBonus',
                            location: 'groin',
                            amount: 4,
                            isScalingWithLevel: false
                        }
                    ],
                    attacks: [],
                    modifiers: [],
                    ignoreWeightForSkills: false,
                    createdOn: '2023-10-29T19:51:05.980177',
                    createdBy: '00000000-0000-0000-0000-000000000000',
                    modifiedOn: '2023-10-29T19:51:05.980177',
                    modifiedBy: '00000000-0000-0000-0000-000000000000',
                    id: '937a2a7a-76b4-4937-8949-7e1248afe3fe'
                },
                quantity: 1,
                isEquipped: true,
                id: '5e62888e-ddec-40fd-b61b-832fc8ef40da'
            },
            {
                selectedModifiers: [],
                equipment: {
                    name: 'Small Buckler',
                    bookReferences: [
                        {
                            pageNumber: 287,
                            sourceBook: 'BasicSet'
                        }
                    ],
                    techLevel: '1',
                    cost: 40,
                    weight: '8 lb',
                    tags: [
                        'Shield'
                    ],
                    features: [
                        {
                            $type: 'SagaGuide.Core.Domain.Features.AttributeBonusFeature, SagaGuide.Core',
                            featureType: 'AttributeBonus',
                            attributeType: 'Dodge',
                            bonusLimitation: 'None',
                            amount: 1,
                            isScalingWithLevel: false
                        },
                        {
                            $type: 'SagaGuide.Core.Domain.Features.AttributeBonusFeature, SagaGuide.Core',
                            featureType: 'AttributeBonus',
                            attributeType: 'Parry',
                            bonusLimitation: 'None',
                            amount: 1,
                            isScalingWithLevel: false
                        },
                        {
                            $type: 'SagaGuide.Core.Domain.Features.AttributeBonusFeature, SagaGuide.Core',
                            featureType: 'AttributeBonus',
                            attributeType: 'Block',
                            bonusLimitation: 'None',
                            amount: 1,
                            isScalingWithLevel: false
                        }
                    ],
                    attacks: [
                        {
                            $type: 'SagaGuide.Core.Domain.EquipmentAggregate.MeleeAttack, SagaGuide.Core',
                            reach: '1',
                            parry: 'No',
                            block: '+0',
                            id: '3e7b8a4b-5bea-4fa0-890e-f21ba658351b',
                            damage: {
                                damageType: 'cr',
                                attackType: 'thr'
                            },
                            minimumStrength: '0',
                            defaults: [
                                {
                                    attributeType: 'Dexterity',
                                    modifier: -4
                                },
                                {
                                    name: 'Shield',
                                    specialization: 'Shield',
                                    modifier: -2
                                },
                                {
                                    name: 'Shield',
                                    specialization: 'Force Shield',
                                    modifier: -2
                                },
                                {
                                    name: 'Shield',
                                    specialization: 'Buckler',
                                    modifier: 0
                                }
                            ]
                        }
                    ],
                    modifiers: [],
                    ignoreWeightForSkills: false,
                    createdOn: '2023-10-29T19:51:05.984715',
                    createdBy: '00000000-0000-0000-0000-000000000000',
                    modifiedOn: '2023-10-29T19:51:05.984715',
                    modifiedBy: '00000000-0000-0000-0000-000000000000',
                    id: 'e3c1f380-1380-4d3f-adc2-fce98369059e'
                },
                quantity: 1,
                isEquipped: true,
                id: 'e0137811-b08b-44bd-8015-586e295ba1e4'
            },
            {
                id: '27e03a43-5680-4887-b33c-175a5efb6c74',
                isEquipped: true,
                quantity: 1,
                equipment: {
                    name: 'Thrusting Bastard Sword',
                    bookReferences: [
                        {
                            pageNumber: 271,
                            sourceBook: 'BasicSet'
                        }
                    ],
                    techLevel: '3',
                    cost: 750,
                    weight: '5 lb',
                    tags: [
                        'Melee Weapon'
                    ],
                    features: [],
                    attacks: [
                        {
                            $type: 'SagaGuide.Core.Domain.EquipmentAggregate.MeleeAttack, SagaGuide.Core',
                            reach: '1,2',
                            parry: '0U',
                            block: 'No',
                            id: 'e996d369-f7c5-44d8-8ac3-27464dbf8502',
                            damage: {
                                damageType: 'cut',
                                attackType: 'sw',
                                baseDamage: '1'
                            },
                            minimumStrength: '11',
                            usage: 'Swung',
                            usageNotes: '1-handed',
                            defaults: [
                                {
                                    attributeType: 'Dexterity',
                                    modifier: -5
                                },
                                {
                                    name: 'Broadsword',
                                    modifier: 0
                                },
                                {
                                    name: 'Force Sword',
                                    modifier: -4
                                },
                                {
                                    name: 'Rapier',
                                    modifier: -4
                                },
                                {
                                    name: 'Saber',
                                    modifier: -4
                                },
                                {
                                    name: 'Shortsword',
                                    modifier: -2
                                },
                                {
                                    name: 'Two-Handed Sword',
                                    modifier: 0
                                },
                                {
                                    name: 'Sword!',
                                    modifier: 0
                                }
                            ]
                        },
                        {
                            $type: 'SagaGuide.Core.Domain.EquipmentAggregate.MeleeAttack, SagaGuide.Core',
                            reach: '2',
                            parry: '0U',
                            block: 'No',
                            id: 'f04d5b62-d226-4f09-8957-4cb473672ffa',
                            damage: {
                                damageType: 'imp',
                                attackType: 'thr',
                                baseDamage: '2'
                            },
                            minimumStrength: '11',
                            usage: 'Thrust',
                            usageNotes: '1-handed',
                            defaults: [
                                {
                                    attributeType: 'Dexterity',
                                    modifier: -5
                                },
                                {
                                    name: 'Broadsword',
                                    modifier: 0
                                },
                                {
                                    name: 'Force Sword',
                                    modifier: -4
                                },
                                {
                                    name: 'Rapier',
                                    modifier: -4
                                },
                                {
                                    name: 'Saber',
                                    modifier: -4
                                },
                                {
                                    name: 'Shortsword',
                                    modifier: -2
                                },
                                {
                                    name: 'Two-Handed Sword',
                                    modifier: 0
                                },
                                {
                                    name: 'Sword!',
                                    modifier: 0
                                }
                            ]
                        },
                        {
                            $type: 'SagaGuide.Core.Domain.EquipmentAggregate.MeleeAttack, SagaGuide.Core',
                            reach: '1,2',
                            parry: '0',
                            block: 'No',
                            id: 'bc2ec4e2-1643-46df-9d14-2a0e1704f386',
                            damage: {
                                damageType: 'cut',
                                attackType: 'sw',
                                baseDamage: '2'
                            },
                            minimumStrength: '10†',
                            usage: 'Swung',
                            usageNotes: '2-handed',
                            defaults: [
                                {
                                    attributeType: 'Dexterity',
                                    modifier: -5
                                },
                                {
                                    name: 'Two-Handed Sword',
                                    modifier: 0
                                },
                                {
                                    name: 'Broadsword',
                                    modifier: -4
                                },
                                {
                                    name: 'Force Sword',
                                    modifier: -4
                                },
                                {
                                    name: 'Sword!',
                                    modifier: 0
                                }
                            ]
                        },
                        {
                            $type: 'SagaGuide.Core.Domain.EquipmentAggregate.MeleeAttack, SagaGuide.Core',
                            reach: '2',
                            parry: '0',
                            block: 'No',
                            id: 'a7b7ba35-ab9b-4d6d-be17-bdb82be674da',
                            damage: {
                                damageType: 'imp',
                                attackType: 'thr',
                                baseDamage: '3'
                            },
                            minimumStrength: '10†',
                            usage: 'Thrust',
                            usageNotes: '2-handed',
                            defaults: [
                                {
                                    attributeType: 'Dexterity',
                                    modifier: -5
                                },
                                {
                                    name: 'Two-Handed Sword',
                                    modifier: 0
                                },
                                {
                                    name: 'Broadsword',
                                    modifier: -4
                                },
                                {
                                    name: 'Force Sword',
                                    modifier: -4
                                },
                                {
                                    name: 'Sword!',
                                    modifier: 0
                                }
                            ]
                        }
                    ],
                    modifiers: [],
                    ignoreWeightForSkills: false,
                    createdBy: '00000000-0000-0000-0000-000000000000',
                    createdOn: '2023-10-29T19:51:05.984245',
                    modifiedBy: '00000000-0000-0000-0000-000000000000',
                    modifiedOn: '2023-10-29T19:51:05.984245',
                    id: 'f4a38ef1-ef41-4a9e-bd30-54c7324fbbd9'
                },
                selectedModifiers: []
            }
        ],
        createdBy: '00000000-0000-0000-0000-000000000000',
        createdOn: '2023-10-29T19:51:06.07214',
        modifiedBy: '00000000-0000-0000-0000-000000000000',
        modifiedOn: '2023-10-31T14:25:10.704745',
        id: '4b704106-7bea-467d-aa73-725196018a75'
    }

const testCharacter: ICharacter = jsonData as unknown as ICharacter;
export const handlers = [
    rest.get(`${charactersUrl}/${frodoCharacter.id}`, (req, res, ctx) => {
        return res(ctx.json(testCharacter), ctx.delay(0))
    }),
]

const server = setupServer(...handlers)

// Enable API mocking before tests.
beforeAll(() => server.listen())

// Reset any runtime request handlers we may add during the tests.
afterEach(() => server.resetHandlers())

// Disable API mocking after the tests are done.
afterAll(() => server.close())

test('Block is calculated and shown right', async () =>
{
    //const store = setupStore()

    let render;

    act(() => {
        render = renderWithProviders(<FrodoCharacterPage/>); //{store: store}
    });
    //store.dispatch(changeCurrentCharacter(testCharacter));

    expect(await screen.findByLabelText(SgCharacterSheetAriaLabels.CharacterName)).toBeInTheDocument();

    let block: IActiveDefenceData = getCharacterBlock(testCharacter);
    let blockLabels = SgCharacterSheetAriaLabels.getActiveDefenceDataLabels(block);
    expect(await screen.findByLabelText(blockLabels.defenceName)).toBeInTheDocument();
    let blockProviderName = await screen.findByLabelText(blockLabels.defenceProviderName);
    // const fs = require('fs');
    // fs.writeFile("D:/test.txt", document.documentElement.outerHTML, function(err) {
    //     if (err) {
    //         return console.log(document.documentElement.outerHTML);
    //     }
    // });


    let buckler = testCharacter.equipments.find(t => t.equipment.name === "Small Buckler")!;
    let bucklerEquipmentRow = await screen.findByLabelText(SgCharacterSheetAriaLabels.getEquipmentRowLabel(buckler));
    expect(bucklerEquipmentRow).toBeInTheDocument();

    let isBucklerEquipped = (await screen.findByLabelText(SgCharacterSheetAriaLabels.getEquipmentIsEquippedCheckBoxLabel(buckler))) as HTMLInputElement;
    expect(isBucklerEquipped).toBeChecked();

    expect(blockProviderName.textContent).toBe("Small Buckler");
    let blockValue = await screen.findByLabelText(blockLabels.value);
    expect(blockValue.textContent).toBe("6");
    
    act(() => {
        userEvent.click(isBucklerEquipped);
    })
    expect(isBucklerEquipped).not.toBeChecked();

    blockProviderName = await screen.findByLabelText(blockLabels.defenceProviderName);
    expect(blockProviderName.textContent).toBe("");
    blockValue = await screen.findByLabelText(blockLabels.value);
    expect(blockValue.textContent).toBe("No");
})