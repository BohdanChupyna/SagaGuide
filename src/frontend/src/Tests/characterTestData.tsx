import {ICharacter} from "../domain/interfaces/character/ICharacter";

export const frodoCharacterId = "4b704106-7bea-467d-aa73-725196018a75";

// @ts-ignore
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
        totalPoints: 100,
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
                id: '73422745-3852-40fd-bcf6-24a7fb30eadf'
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
                spentPoints: 10,
                id: '0755da89-9460-44c9-a378-7841e8b8d6f6'
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
                id: '726ea1eb-ea66-434b-816d-9f844e934fd8'
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
                spentPoints: 5,
                id: 'f9b5db32-e718-413f-9790-e2ba1e894f59'
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
                id: '157dd639-185f-49d9-a6ad-eb67f4913308'
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
                spentPoints: 2,
                id: '715ced66-ca6e-4346-b568-6f99034cb819'
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
                id: 'ad56eff8-f98b-4966-9df2-e1802f55d57b'
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
                spentPoints: 6,
                id: '85d1d0b0-d0c1-4159-9116-67b1e3760793'
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
                id: 'f582ddf5-7ae5-4e5e-a3ea-a7fbe7678cda'
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
                id: '597c5336-74b7-45f0-8722-0ddadbaa2d25'
            }
        ],
        skills: [
            {
                skill: {
                    name: 'Armoury (Battlesuits)',
                    tags: [],
                    difficultyLevel: 'Average',
                    pointsCost: 0,
                    defaults: [
                        {
                            attributeType: 'Intelligence',
                            modifier: -5
                        }
                    ],
                    attributeType: 'Intelligence',
                    bookReferences: [
                        {
                            pageNumber: 178,
                            sourceBook: 'BasicSet'
                        }
                    ],
                    createdBy: '00000000-0000-0000-0000-000000000000',
                    createdOn: '2023-09-08T11:48:01.7505126Z',
                    modifiedBy: '00000000-0000-0000-0000-000000000000',
                    modifiedOn: '2023-09-08T11:48:01.7505127Z',
                    id: '171cc970-e329-42f4-bf44-32b87baaae3c'
                },
                spentPoints: 8,
                defaultedFrom: {
                    attributeType: 'Intelligence',
                    modifier: -5
                },
                id: '847fb73c-f7f5-4d89-8a2c-b7af8db56d19'
            },
            {
                skill: {
                    name: 'Archaeology',
                    tags: [],
                    difficultyLevel: 'Hard',
                    pointsCost: 0,
                    defaults: [
                        {
                            attributeType: 'Intelligence',
                            modifier: -2
                        },
                        {
                            attributeType: 'Dexterity',
                            modifier: -4
                        }
                    ],
                    attributeType: 'Intelligence',
                    bookReferences: [
                        {
                            pageNumber: 234,
                            sourceBook: 'BasicSet'
                        }
                    ],
                    createdBy: '00000000-0000-0000-0000-000000000000',
                    createdOn: '2023-09-08T11:48:01.7499383Z',
                    modifiedBy: '00000000-0000-0000-0000-000000000000',
                    modifiedOn: '2023-09-08T11:48:01.7499383Z',
                    id: '219ce0c8-7534-4fbc-8c6e-6778ed7acbd7'
                },
                spentPoints: 8,
                defaultedFrom: {
                    attributeType: 'Intelligence',
                    modifier: -2
                },
                id: '7c502d1d-8baa-416d-897a-5803c7907f9e'
            }
        ],
        techniques: [
            {
                technique: {
                    name: 'Neck Snap',
                    tags: [
                        'Combat',
                        'Melee Combat',
                        'Technique',
                        'Weapon'
                    ],
                    bookReferences: [
                        {
                            pageNumber: 232,
                            sourceBook: 'BasicSet'
                        },
                        {
                            pageNumber: 77,
                            sourceBook: 'MartialArts'
                        }
                    ],
                    difficultyLevel: 'Hard',
                    pointsCost: 2,
                    'default': {
                        attributeType: 'Strength',
                        modifier: -4
                    },
                    techniqueLimitModifier: 3,
                    createdBy: '00000000-0000-0000-0000-000000000000',
                    createdOn: '2023-09-08T11:48:01.851963',
                    modifiedBy: '00000000-0000-0000-0000-000000000000',
                    modifiedOn: '2023-09-08T11:48:01.851963',
                    id: '3faabd25-9946-4c09-859c-56ceb9c3c84b'
                },
                spentPoints: 3,
                id: '960d93ea-ba27-4304-81cb-5d027609079f'
            }
        ],
        traits: [
            {
                trait: {
                    name: 'Amphibious',
                    tags: [
                        'Advantage',
                        'Exotic',
                        'Physical'
                    ],
                    bookReferences: [
                        {
                            pageNumber: 40,
                            sourceBook: 'BasicSet'
                        },
                        {
                            pageNumber: 42,
                            sourceBook: 'Powers'
                        }
                    ],
                    pointsCostPerLevel: 0,
                    basePointsCost: 10,
                    canLevel: false,
                    roundCostDown: false,
                    features: [],
                    modifiers: [],
                    modifierGroups: [],
                    selfControlRoll: 0,
                    selfControlRollAdjustment: 'None',
                    createdBy: '00000000-0000-0000-0000-000000000000',
                    createdOn: '2023-09-08T11:48:01.6241862Z',
                    modifiedBy: '00000000-0000-0000-0000-000000000000',
                    modifiedOn: '2023-09-08T11:48:01.6241864Z',
                    id: '003f1809-fb3f-48c6-b962-2c8177457f20'
                },
                level: 1,
                selectedTraitModifiers: [],
                id: '57251fd3-aab6-4f04-9af4-82a1d2c8525e'
            },
            {
                trait: {
                    name: 'Acrophobia (Heights)',
                    localNotes: 'You may not voluntarily go more than 15 feet above ground, unless you are inside a building and away from windows. If there is some chance of an actual fall, self-control rolls are at -5.',
                    tags: [
                        'Disadvantage',
                        'Mental'
                    ],
                    bookReferences: [
                        {
                            pageNumber: 150,
                            sourceBook: 'BasicSet'
                        }
                    ],
                    pointsCostPerLevel: 0,
                    basePointsCost: -10,
                    canLevel: false,
                    roundCostDown: false,
                    features: [],
                    modifiers: [],
                    modifierGroups: [],
                    selfControlRoll: 12,
                    selfControlRollAdjustment: 'ActionPenalty',
                    createdBy: '00000000-0000-0000-0000-000000000000',
                    createdOn: '2023-09-08T11:48:01.641417',
                    modifiedBy: '00000000-0000-0000-0000-000000000000',
                    modifiedOn: '2023-09-08T11:48:01.641417',
                    id: 'a47241c6-a068-43a9-b20a-5c9e597fe73a'
                },
                optionalSpecialty: '',
                level: 1,
                selectedTraitModifiers: [],
                id: '378ad704-f5f7-42c8-8cac-8052d038cd4a'
            },
            {
                trait: {
                    name: 'Absolute Direction',
                    tags: [
                        'Advantage',
                        'Mental',
                        'Physical'
                    ],
                    bookReferences: [
                        {
                            pageNumber: 34,
                            sourceBook: 'BasicSet'
                        }
                    ],
                    pointsCostPerLevel: 0,
                    basePointsCost: 5,
                    canLevel: false,
                    roundCostDown: false,
                    features: [
                        {
                            $type: 'GeneratedType_1, Mapster.Dynamic',
                            featureType: 'SkillBonus'
                        },
                        {
                            $type: 'GeneratedType_1, Mapster.Dynamic',
                            featureType: 'SkillBonus'
                        },
                        {
                            $type: 'GeneratedType_1, Mapster.Dynamic',
                            featureType: 'SkillBonus'
                        },
                        {
                            $type: 'GeneratedType_1, Mapster.Dynamic',
                            featureType: 'SkillBonus'
                        }
                    ],
                    modifiers: [
                        {
                            id: '940c9da3-6966-4ea6-9974-517614d0606b',
                            name: 'Requires signal',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 34,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: -20,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: '12730389-6652-4df8-8b34-ad078b76e408',
                            name: '3D Spatial Sense',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 34,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [
                                {
                                    $type: 'GeneratedType_1, Mapster.Dynamic',
                                    featureType: 'SkillBonus'
                                },
                                {
                                    $type: 'GeneratedType_1, Mapster.Dynamic',
                                    featureType: 'SkillBonus'
                                },
                                {
                                    $type: 'GeneratedType_1, Mapster.Dynamic',
                                    featureType: 'SkillBonus'
                                },
                                {
                                    $type: 'GeneratedType_1, Mapster.Dynamic',
                                    featureType: 'SkillBonus'
                                },
                                {
                                    $type: 'GeneratedType_1, Mapster.Dynamic',
                                    featureType: 'SkillBonus'
                                }
                            ],
                            pointsCost: 5,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        }
                    ],
                    modifierGroups: [],
                    selfControlRoll: 0,
                    selfControlRollAdjustment: 'None',
                    createdBy: '00000000-0000-0000-0000-000000000000',
                    createdOn: '2023-09-08T11:48:01.636811',
                    modifiedBy: '00000000-0000-0000-0000-000000000000',
                    modifiedOn: '2023-09-08T11:48:01.636811',
                    id: 'b074474a-27f9-4d63-8f35-3be884ae5343'
                },
                optionalSpecialty: '',
                level: 1,
                selectedTraitModifiers: [
                    {
                        traitModifierId: '940c9da3-6966-4ea6-9974-517614d0606b',
                        spentPoints: -20
                    }
                ],
                id: 'f33d1bda-99bd-4e8d-aa17-b5a1a41a3fb6'
            },
            {
                trait: {
                    name: 'Addiction (@Substance@)',
                    tags: [
                        'Disadvantage',
                        'Mental',
                        'Physical'
                    ],
                    bookReferences: [
                        {
                            pageNumber: 122,
                            sourceBook: 'BasicSet'
                        }
                    ],
                    pointsCostPerLevel: 0,
                    basePointsCost: 0,
                    canLevel: false,
                    roundCostDown: false,
                    features: [],
                    modifiers: [],
                    modifierGroups: [
                        {
                            modifiers: [
                                {
                                    id: '7b7bbaee-545a-4860-bbf7-c5b1b4675ca1',
                                    name: 'Illegal',
                                    tags: [],
                                    bookReferences: [
                                        {
                                            pageNumber: 122,
                                            sourceBook: 'BasicSet'
                                        }
                                    ],
                                    features: [],
                                    pointsCost: 0,
                                    costType: 'Points',
                                    costAffectType: 'Total',
                                    canLevel: false
                                },
                                {
                                    id: '4d21d742-337e-4ad6-b89e-4ee43576518c',
                                    name: 'Legal',
                                    tags: [],
                                    bookReferences: [
                                        {
                                            pageNumber: 122,
                                            sourceBook: 'BasicSet'
                                        }
                                    ],
                                    features: [],
                                    pointsCost: 5,
                                    costType: 'Points',
                                    costAffectType: 'Total',
                                    canLevel: false
                                }
                            ],
                            id: '111b11c6-e991-46d2-81d4-f76210fd5b23',
                            name: 'Legality Modifiers',
                            bookReferences: []
                        },
                        {
                            modifiers: [
                                {
                                    id: '8c2a3142-a939-4004-ad5d-499ae6fa3566',
                                    name: 'Cheap',
                                    tags: [],
                                    bookReferences: [
                                        {
                                            pageNumber: 122,
                                            sourceBook: 'BasicSet'
                                        }
                                    ],
                                    features: [],
                                    pointsCost: -5,
                                    costType: 'Points',
                                    costAffectType: 'Total',
                                    canLevel: false
                                },
                                {
                                    id: '8d31553a-eba7-4b1e-bb5d-95754c733ba6',
                                    name: 'Expensive',
                                    tags: [],
                                    bookReferences: [
                                        {
                                            pageNumber: 122,
                                            sourceBook: 'BasicSet'
                                        }
                                    ],
                                    features: [],
                                    pointsCost: -10,
                                    costType: 'Points',
                                    costAffectType: 'Total',
                                    canLevel: false
                                },
                                {
                                    id: 'b9d4f346-f139-475e-953f-f53d605bd61c',
                                    name: 'Very Expensive',
                                    tags: [],
                                    bookReferences: [
                                        {
                                            pageNumber: 122,
                                            sourceBook: 'BasicSet'
                                        }
                                    ],
                                    features: [],
                                    pointsCost: -20,
                                    costType: 'Points',
                                    costAffectType: 'Total',
                                    canLevel: false
                                }
                            ],
                            id: 'fbd5a34a-8756-4d14-a5af-2c06348aa54e',
                            name: 'Cost Modifiers',
                            bookReferences: []
                        },
                        {
                            modifiers: [
                                {
                                    id: '0ef02da7-a1ec-4f5d-814a-c2770b79ead0',
                                    name: 'Highly Addictive (-5 on withdrawal roll)',
                                    tags: [],
                                    bookReferences: [
                                        {
                                            pageNumber: 122,
                                            sourceBook: 'BasicSet'
                                        }
                                    ],
                                    features: [],
                                    pointsCost: -5,
                                    costType: 'Points',
                                    costAffectType: 'Total',
                                    canLevel: false
                                },
                                {
                                    id: 'bcf905ae-5148-48e8-8842-1b5b342a276d',
                                    name: 'Totally Addictive (-10 on withdrawal roll)',
                                    tags: [],
                                    bookReferences: [
                                        {
                                            pageNumber: 122,
                                            sourceBook: 'BasicSet'
                                        }
                                    ],
                                    features: [],
                                    pointsCost: -10,
                                    costType: 'Points',
                                    costAffectType: 'Total',
                                    canLevel: false
                                },
                                {
                                    id: '9d544143-e3b7-4eaa-8cdb-d6c856158971',
                                    name: 'Hallucinogenic',
                                    tags: [],
                                    bookReferences: [
                                        {
                                            pageNumber: 122,
                                            sourceBook: 'BasicSet'
                                        }
                                    ],
                                    features: [],
                                    pointsCost: -10,
                                    costType: 'Points',
                                    costAffectType: 'Total',
                                    canLevel: false
                                },
                                {
                                    id: '90ed43c8-dad6-4c78-bf22-cf13846b6489',
                                    name: 'Incapacitating',
                                    tags: [],
                                    bookReferences: [
                                        {
                                            pageNumber: 122,
                                            sourceBook: 'BasicSet'
                                        }
                                    ],
                                    features: [],
                                    pointsCost: -10,
                                    costType: 'Points',
                                    costAffectType: 'Total',
                                    canLevel: false
                                }
                            ],
                            id: '4e05c404-a365-4841-a5e3-1841bb3581ca',
                            name: 'Effect Modifiers',
                            bookReferences: []
                        }
                    ],
                    selfControlRoll: 0,
                    selfControlRollAdjustment: 'None',
                    createdBy: '00000000-0000-0000-0000-000000000000',
                    createdOn: '2023-09-08T11:48:01.641572',
                    modifiedBy: '00000000-0000-0000-0000-000000000000',
                    modifiedOn: '2023-09-08T11:48:01.641572',
                    id: '11b0f87e-a312-42bd-b84b-758a1f732811'
                },
                optionalSpecialty: 'ring',
                level: 1,
                selectedTraitModifiers: [
                    {
                        traitModifierId: 'b9d4f346-f139-475e-953f-f53d605bd61c',
                        spentPoints: -20
                    },
                    {
                        traitModifierId: '9d544143-e3b7-4eaa-8cdb-d6c856158971',
                        spentPoints: -10
                    }
                ],
                id: '0b0203db-1ebe-4099-a076-337ff8a52185'
            },
            {
                trait: {
                    name: '360° Vision',
                    tags: [
                        'Advantage',
                        'Exotic',
                        'Physical'
                    ],
                    bookReferences: [
                        {
                            pageNumber: 34,
                            sourceBook: 'BasicSet'
                        },
                        {
                            pageNumber: 39,
                            sourceBook: 'Powers'
                        }
                    ],
                    pointsCostPerLevel: 0,
                    basePointsCost: 25,
                    canLevel: false,
                    roundCostDown: false,
                    features: [],
                    modifiers: [
                        {
                            id: '49d8792f-35b1-4cf6-ba95-071c87b0bae6',
                            name: 'Easy to hit',
                            localNotes: 'Others can target your eyes at -6',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 34,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: -20,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: 'f7d427d0-1509-4a07-8896-bf7f40bbc808',
                            name: 'Panoptic 1',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 39,
                                    sourceBook: 'Powers'
                                }
                            ],
                            features: [],
                            pointsCost: 20,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: '5002586b-ad58-459d-b542-bc5ba3766505',
                            name: 'Panoptic 2',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 39,
                                    sourceBook: 'Powers'
                                }
                            ],
                            features: [],
                            pointsCost: 60,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        }
                    ],
                    modifierGroups: [],
                    selfControlRoll: 0,
                    selfControlRollAdjustment: 'None',
                    createdBy: '00000000-0000-0000-0000-000000000000',
                    createdOn: '2023-09-08T11:48:01.632882',
                    modifiedBy: '00000000-0000-0000-0000-000000000000',
                    modifiedOn: '2023-09-08T11:48:01.632883',
                    id: 'aa524785-968c-43c4-bd1a-94c1d2263b35'
                },
                optionalSpecialty: 'ring',
                level: 1,
                selectedTraitModifiers: [
                    {
                        traitModifierId: '5002586b-ad58-459d-b542-bc5ba3766505',
                        spentPoints: 60
                    }
                ],
                id: '4395283b-6f1a-45e7-a631-29c0a1bf9e26'
            }
        ],
        equipments: [],
        createdBy: '00000000-0000-0000-0000-000000000000',
        createdOn: '2023-09-08T11:48:01.910936',
        modifiedBy: '00000000-0000-0000-0000-000000000000',
        modifiedOn: '2023-09-08T19:17:31.441362',
        id: '4b704106-7bea-467d-aa73-725196018a75'
    }

export const frodoCharacter: ICharacter = jsonData as unknown as ICharacter;
    