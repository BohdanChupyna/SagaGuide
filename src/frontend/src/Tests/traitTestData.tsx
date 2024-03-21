// @ts-ignore
import ITrait from "../domain/interfaces/trait/ITrait";

const jsonData =
    [
        {
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
            createdOn: '2023-08-23T08:41:47.927915',
            modifiedBy: '00000000-0000-0000-0000-000000000000',
            modifiedOn: '2023-08-23T08:41:47.927918',
            id: '003f1809-fb3f-48c6-b962-2c8177457f20'
        },
        {
            name: 'Absent-Mindedness',
            localNotes: 'Once adrift in your own thoughts, you must roll against Perception-5 in order to notice any event short of personal physical injury',
            tags: [
                'Disadvantage',
                'Mental'
            ],
            bookReferences: [
                {
                    pageNumber: 122,
                    sourceBook: 'BasicSet'
                }
            ],
            pointsCostPerLevel: 0,
            basePointsCost: -15,
            canLevel: false,
            roundCostDown: false,
            features: [
                {
                    $type: 'GeneratedType_1, Mapster.Dynamic',
                    featureType: 'ConditionalModifierBonus'
                }
            ],
            modifiers: [],
            modifierGroups: [],
            selfControlRoll: 0,
            selfControlRollAdjustment: 'None',
            createdBy: '00000000-0000-0000-0000-000000000000',
            createdOn: '2023-08-23T08:41:47.94477',
            modifiedBy: '00000000-0000-0000-0000-000000000000',
            modifiedOn: '2023-08-23T08:41:47.944771',
            id: '5f10acf3-8205-4194-80ad-f76b63ef867d'
        },
        {
            name: 'Ally (@Who@)',
            tags: [
                'Advantage',
                'Social'
            ],
            bookReferences: [
                {
                    pageNumber: 36,
                    sourceBook: 'BasicSet'
                },
                {
                    pageNumber: 41,
                    sourceBook: 'Powers'
                }
            ],
            pointsCostPerLevel: 0,
            basePointsCost: 0,
            canLevel: false,
            roundCostDown: false,
            features: [],
            modifiers: [
                {
                    id: '689f0318-5bb4-45b8-bc81-74c14097fe68',
                    name: 'Favor',
                    tags: [],
                    bookReferences: [
                        {
                            pageNumber: 55,
                            sourceBook: 'BasicSet'
                        }
                    ],
                    features: [],
                    pointsCost: 0.2,
                    costType: 'Multiplier',
                    costAffectType: 'Total',
                    canLevel: false
                },
                {
                    id: '1423dd75-af66-4dfb-bf98-39ebd775beb3',
                    name: 'Minion',
                    tags: [],
                    bookReferences: [
                        {
                            pageNumber: 38,
                            sourceBook: 'BasicSet'
                        }
                    ],
                    features: [],
                    pointsCost: 50,
                    costType: 'Points',
                    costAffectType: 'Total',
                    canLevel: false
                },
                {
                    id: '6dc2adbd-08c7-4ef0-8614-22ec36071dbb',
                    name: 'Minion',
                    localNotes: 'IQ 0 or Slave Mentality',
                    tags: [],
                    bookReferences: [
                        {
                            pageNumber: 38,
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
                    id: 'd8886838-7444-495b-aa11-02ac3ae23dd2',
                    name: 'Special Abilities',
                    localNotes: '@Abilities@',
                    tags: [],
                    bookReferences: [
                        {
                            pageNumber: 38,
                            sourceBook: 'BasicSet'
                        }
                    ],
                    features: [],
                    pointsCost: 50,
                    costType: 'Points',
                    costAffectType: 'Total',
                    canLevel: false
                },
                {
                    id: '2777d1e3-2b29-4c22-b543-d2553828e7e9',
                    name: 'Unwilling',
                    tags: [],
                    bookReferences: [
                        {
                            pageNumber: 38,
                            sourceBook: 'BasicSet'
                        }
                    ],
                    features: [],
                    pointsCost: -50,
                    costType: 'Points',
                    costAffectType: 'Total',
                    canLevel: false
                }
            ],
            modifierGroups: [
                {
                    modifiers: [
                        {
                            id: '90ea2d51-49e6-484c-9cb3-461df7b7bd54',
                            name: 'Appears quite rarely (6-)',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 36,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 0.5,
                            costType: 'Multiplier',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: 'be41be9a-9e87-47cf-bff3-755e3635a670',
                            name: 'Appears fairly often (9-)',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 36,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 1,
                            costType: 'Multiplier',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: '558c0415-bdd8-4a47-bf06-09fd5e1d7e80',
                            name: 'Appears quite often (12-)',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 36,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 2,
                            costType: 'Multiplier',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: '1f0b6315-dff4-473e-bdef-19346d578523',
                            name: 'Appears almost all the time (15-)',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 36,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 3,
                            costType: 'Multiplier',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: 'd2c39c4d-c98d-4266-8b8c-dcd6383e3a0a',
                            name: 'Appears constantly',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 36,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 4,
                            costType: 'Multiplier',
                            costAffectType: 'Total',
                            canLevel: false
                        }
                    ],
                    id: 'a3aae46a-4033-47e9-8212-84f46bf2a404',
                    name: 'Frequency of Appearance',
                    bookReferences: [
                        {
                            pageNumber: 37,
                            sourceBook: 'BasicSet'
                        }
                    ]
                },
                {
                    modifiers: [
                        {
                            id: '9b920170-2506-4d12-b871-be1100043704',
                            name: 'Group of 6-10',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 37,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 6,
                            costType: 'Multiplier',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: 'd0e44bc7-5889-46ec-879f-3d9ba9a4e54f',
                            name: 'Group of 11-20',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 37,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 8,
                            costType: 'Multiplier',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: '8fc8e24d-4b27-4e45-9b3a-5a6f64ada309',
                            name: 'Group of 21-50',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 37,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 10,
                            costType: 'Multiplier',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: '1dedde55-e356-448c-961a-53dc73e8e58b',
                            name: 'Group of 51-100',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 37,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 12,
                            costType: 'Multiplier',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: 'a6fa2681-e601-4915-9e43-e357c1119e73',
                            name: 'Group of 101-1000',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 37,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 18,
                            costType: 'Multiplier',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: 'd176a29c-87f6-41b9-8d4e-10beb54d1822',
                            name: 'Group of 1001-10000',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 37,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 24,
                            costType: 'Multiplier',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: '8584249f-64fa-4939-87eb-63382071fbf3',
                            name: 'Group of 10001-100000',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 37,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 30,
                            costType: 'Multiplier',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: '7429f8b9-70f3-4fe7-8c48-a9d1b9c06e0a',
                            name: 'Group of 100001-1000000',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 37,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 36,
                            costType: 'Multiplier',
                            costAffectType: 'Total',
                            canLevel: false
                        }
                    ],
                    id: 'aa16b468-bd6e-42aa-b471-8656922c6757',
                    name: 'Ally Group',
                    bookReferences: [
                        {
                            pageNumber: 37,
                            sourceBook: 'BasicSet'
                        }
                    ]
                },
                {
                    modifiers: [
                        {
                            id: 'c1eb750d-bdd1-4483-a293-892526735fad',
                            name: '25% of your starting points',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 37,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 1,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: '0b93dc24-8f30-4c58-8595-a5f2bd05173d',
                            name: '50% of your starting points',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 37,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 2,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: '469b4a79-c6cf-4f87-be4d-c39428a08d9b',
                            name: '75% of your starting points',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 37,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 3,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: '1c5fa2a8-39df-4651-a06b-21635ead97ca',
                            name: '100% of your starting points',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 37,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 5,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: 'b9452cfa-cda2-4ce0-8531-7a6b9c33ddb9',
                            name: '150% of your starting points',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 37,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: 10,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        }
                    ],
                    id: 'af970aba-632e-444c-abbe-ea978ae30f1f',
                    name: 'Point Total',
                    bookReferences: [
                        {
                            pageNumber: 37,
                            sourceBook: 'BasicSet'
                        }
                    ]
                },
                {
                    modifiers: [
                        {
                            id: 'd00c3a09-82c4-4569-81df-955e8e62ef34',
                            name: 'Summonable',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 38,
                                    sourceBook: 'BasicSet'
                                },
                                {
                                    pageNumber: 41,
                                    sourceBook: 'Powers'
                                }
                            ],
                            features: [],
                            pointsCost: 100,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: '564697bc-4d59-487f-be52-9cd695d74145',
                            name: 'Conjured',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 4,
                                    magazineNumber: 9,
                                    sourceBook: 'DungeonFantasy'
                                },
                                {
                                    pageNumber: 41,
                                    sourceBook: 'Powers'
                                }
                            ],
                            features: [],
                            pointsCost: 100,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: '2b7c2195-de87-4b42-b5b1-7adc5c64dc24',
                            name: 'Harder Summoning 1',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 4,
                                    magazineNumber: 9,
                                    sourceBook: 'DungeonFantasy'
                                }
                            ],
                            features: [],
                            pointsCost: -5,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: '356fa9ea-c5e9-40c5-b3f2-a7f7ab5d3f3a',
                            name: 'Harder Summoning 2',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 4,
                                    magazineNumber: 9,
                                    sourceBook: 'DungeonFantasy'
                                }
                            ],
                            features: [],
                            pointsCost: -10,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: 'c405a9b9-2959-4866-8974-43ef5373bbe9',
                            name: 'Harder Summoning 3',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 4,
                                    magazineNumber: 9,
                                    sourceBook: 'DungeonFantasy'
                                }
                            ],
                            features: [],
                            pointsCost: -20,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        }
                    ],
                    id: 'c432c70e-76f8-4215-9b0e-1e1297817f73',
                    name: 'Summonable',
                    bookReferences: []
                },
                {
                    modifiers: [
                        {
                            id: 'f3001c31-1cbb-4f95-af86-c92a7755a044',
                            name: 'Sympathy',
                            localNotes: 'Death of one party kills the other',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 38,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: -50,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: '30df2a61-903a-4f3f-b7ce-6319dcc61e6a',
                            name: 'Sympathy',
                            localNotes: 'Death of one party kills the other and wounds affect ally but not you',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 38,
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
                            id: 'd3be4428-f357-41d5-8bce-142240934518',
                            name: 'Sympathy',
                            localNotes: 'Death of one party reduces the other to 0 HP',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 38,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: -25,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        },
                        {
                            id: '036a0c85-d7af-4de9-af22-eea6cec5ed47',
                            name: 'Sympathy',
                            localNotes: 'Death of one party reduces the other to 0 HP and wounds affect ally but not you',
                            tags: [],
                            bookReferences: [
                                {
                                    pageNumber: 38,
                                    sourceBook: 'BasicSet'
                                }
                            ],
                            features: [],
                            pointsCost: -5,
                            costType: 'Points',
                            costAffectType: 'Total',
                            canLevel: false
                        }
                    ],
                    id: 'dd121846-2ba4-46bb-83fc-d881218cffca',
                    name: 'Sympathy',
                    bookReferences: [
                        {
                            pageNumber: 38,
                            sourceBook: 'BasicSet'
                        }
                    ]
                }
            ],
            selfControlRoll: 0,
            selfControlRollAdjustment: 'None',
            createdBy: '00000000-0000-0000-0000-000000000000',
            createdOn: '2023-08-23T08:41:47.950088',
            modifiedBy: '00000000-0000-0000-0000-000000000000',
            modifiedOn: '2023-08-23T08:41:47.950089',
            id: 'b5be5c9e-af78-40a3-8f94-c061622c27d7'
        }
    ];

export const traitsTestData = jsonData as unknown as ITrait[];