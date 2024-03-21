// @ts-ignore
import {IEquipment} from "../domain/interfaces/equipment/IEquipment";

const jsonData =
    [
        {
            "name": "Group Basics",
            "bookReferences": [
                {
                    "pageNumber": 288,
                    "sourceBook": "BasicSet"
                }
            ],
            "notes": "Basic equipment for Cooking and Survival skill for a group. Cook pot, rope, hatchet etc., for 3-8 campers.",
            "techLevel": "0",
            "cost": 50.0,
            "weight": "20 lb",
            "tags": [
                "Camping and Survival Gear"
            ],
            "features": [],
            "attacks": [],
            "modifiers": [],
            "ignoreWeightForSkills": false,
            "createdBy": "00000000-0000-0000-0000-000000000000",
            "createdOn": "2023-10-25T08:45:34.238597",
            "modifiedBy": "00000000-0000-0000-0000-000000000000",
            "modifiedOn": "2023-10-25T08:45:34.238597",
            "id": "8ef3ab82-c779-45a3-b207-64e54c301479"
        },
        {
            "name": "SAW, 5.56mm",
            "bookReferences": [
                {
                    "pageNumber": 281,
                    "sourceBook": "BasicSet"
                }
            ],
            "techLevel": "7",
            "legalityClass": "1",
            "cost": 4800.0,
            "weight": "24 lb",
            "tags": [
                "Missile Weapon"
            ],
            "features": [],
            "attacks": [
                {
                    "$type": "SagaGuide.Core.Domain.EquipmentAggregate.RangedAttack, SagaGuide.Core",
                    "accuracy": "5",
                    "range": "800/3500",
                    "rateOfFire": "12!",
                    "shots": "200(5)",
                    "bulk": "-6",
                    "recoil": "2",
                    "id": "5d3068e8-1985-4621-9ae2-c23d4d182da4",
                    "damage": {
                        "damageType": "pi",
                        "baseDamage": "5d+1"
                    },
                    "minimumStrength": "12B†",
                    "defaults": [
                        {
                            "attributeType": "Dexterity",
                            "modifier": -4
                        },
                        {
                            "name": "Guns",
                            "specialization": "Pistol",
                            "modifier": -2
                        },
                        {
                            "name": "Guns",
                            "specialization": "Rifle",
                            "modifier": -2
                        },
                        {
                            "name": "Guns",
                            "specialization": "Shotgun",
                            "modifier": -2
                        },
                        {
                            "name": "Guns",
                            "specialization": "Light Machine Gun",
                            "modifier": 0
                        },
                        {
                            "name": "Guns",
                            "specialization": "Submachine Gun",
                            "modifier": -2
                        },
                        {
                            "name": "Guns",
                            "specialization": "Musket",
                            "modifier": -2
                        },
                        {
                            "name": "Guns",
                            "specialization": "Grenade Launcher",
                            "modifier": -4
                        },
                        {
                            "name": "Guns",
                            "specialization": "Gyroc",
                            "modifier": -4
                        },
                        {
                            "name": "Guns",
                            "specialization": "Light Anti-Armor Weapon",
                            "modifier": -4
                        },
                        {
                            "name": "Gun!",
                            "modifier": 0
                        }
                    ]
                }
            ],
            "modifiers": [],
            "ignoreWeightForSkills": false,
            "createdBy": "00000000-0000-0000-0000-000000000000",
            "createdOn": "2023-10-25T08:45:34.23361",
            "modifiedBy": "00000000-0000-0000-0000-000000000000",
            "modifiedOn": "2023-10-25T08:45:34.23361",
            "id": "37f5447c-8566-4a59-8b84-5272704fe90b"
        },
        {
            "name": "Scale Armor",
            "bookReferences": [
                {
                    "pageNumber": 283,
                    "sourceBook": "BasicSet"
                }
            ],
            "techLevel": "2",
            "cost": 420.0,
            "weight": "35 lb",
            "tags": [
                "Body Armor"
            ],
            "features": [
                {
                    "$type": "SagaGuide.Core.Domain.Features.DamageReductionBonusFeature, SagaGuide.Core",
                    "featureType": "DamageReductionBonus",
                    "location": "torso",
                    "amount": 4,
                    "isScalingWithLevel": false
                },
                {
                    "$type": "SagaGuide.Core.Domain.Features.DamageReductionBonusFeature, SagaGuide.Core",
                    "featureType": "DamageReductionBonus",
                    "location": "vitals",
                    "amount": 4,
                    "isScalingWithLevel": false
                },
                {
                    "$type": "SagaGuide.Core.Domain.Features.DamageReductionBonusFeature, SagaGuide.Core",
                    "featureType": "DamageReductionBonus",
                    "location": "groin",
                    "amount": 4,
                    "isScalingWithLevel": false
                }
            ],
            "attacks": [],
            "modifiers": [],
            "ignoreWeightForSkills": false,
            "createdBy": "00000000-0000-0000-0000-000000000000",
            "createdOn": "2023-10-25T08:45:34.233441",
            "modifiedBy": "00000000-0000-0000-0000-000000000000",
            "modifiedOn": "2023-10-25T08:45:34.233441",
            "id": "937a2a7a-76b4-4937-8949-7e1248afe3fe"
        },
        {
            "name": "Scale Leggings",
            "bookReferences": [
                {
                    "pageNumber": 283,
                    "sourceBook": "BasicSet"
                }
            ],
            "techLevel": "2",
            "legalityClass": "3",
            "cost": 250.0,
            "weight": "21 lb",
            "tags": [
                "Limb Armor"
            ],
            "features": [
                {
                    "$type": "SagaGuide.Core.Domain.Features.DamageReductionBonusFeature, SagaGuide.Core",
                    "featureType": "DamageReductionBonus",
                    "location": "leg",
                    "amount": 4,
                    "isScalingWithLevel": false
                }
            ],
            "attacks": [],
            "modifiers": [],
            "ignoreWeightForSkills": false,
            "createdBy": "00000000-0000-0000-0000-000000000000",
            "createdOn": "2023-10-25T08:45:34.232028",
            "modifiedBy": "00000000-0000-0000-0000-000000000000",
            "modifiedOn": "2023-10-25T08:45:34.232029",
            "id": "f1ab1686-327b-42b5-9393-8b94d33cdc63"
        },
        {
            "name": "Small Buckler",
            "bookReferences": [
                {
                    "pageNumber": 287,
                    "sourceBook": "BasicSet"
                }
            ],
            "techLevel": "1",
            "cost": 40.0,
            "weight": "8 lb",
            "tags": [
                "Shield"
            ],
            "features": [
                {
                    "$type": "SagaGuide.Core.Domain.Features.AttributeBonusFeature, SagaGuide.Core",
                    "featureType": "AttributeBonus",
                    "attributeType": "Dodge",
                    "bonusLimitation": "None",
                    "amount": 1,
                    "isScalingWithLevel": false
                },
                {
                    "$type": "SagaGuide.Core.Domain.Features.AttributeBonusFeature, SagaGuide.Core",
                    "featureType": "AttributeBonus",
                    "attributeType": "Parry",
                    "bonusLimitation": "None",
                    "amount": 1,
                    "isScalingWithLevel": false
                },
                {
                    "$type": "SagaGuide.Core.Domain.Features.AttributeBonusFeature, SagaGuide.Core",
                    "featureType": "AttributeBonus",
                    "attributeType": "Block",
                    "bonusLimitation": "None",
                    "amount": 1,
                    "isScalingWithLevel": false
                }
            ],
            "attacks": [
                {
                    "$type": "SagaGuide.Core.Domain.EquipmentAggregate.MeleeAttack, SagaGuide.Core",
                    "reach": "1",
                    "parry": "No",
                    "block": "+0",
                    "id": "3e7b8a4b-5bea-4fa0-890e-f21ba658351b",
                    "damage": {
                        "damageType": "cr",
                        "attackType": "thr"
                    },
                    "minimumStrength": "0",
                    "defaults": [
                        {
                            "attributeType": "Dexterity",
                            "modifier": -4
                        },
                        {
                            "name": "Shield",
                            "specialization": "Shield",
                            "modifier": -2
                        },
                        {
                            "name": "Shield",
                            "specialization": "Force Shield",
                            "modifier": -2
                        },
                        {
                            "name": "Shield",
                            "specialization": "Buckler",
                            "modifier": 0
                        }
                    ]
                }
            ],
            "modifiers": [],
            "ignoreWeightForSkills": false,
            "createdBy": "00000000-0000-0000-0000-000000000000",
            "createdOn": "2023-10-25T08:45:34.238928",
            "modifiedBy": "00000000-0000-0000-0000-000000000000",
            "modifiedOn": "2023-10-25T08:45:34.238928",
            "id": "e3c1f380-1380-4d3f-adc2-fce98369059e"
        },
        {
            "name": "Throwing Axe",
            "bookReferences": [
                {
                    "pageNumber": 271,
                    "sourceBook": "BasicSet"
                }
            ],
            "techLevel": "0",
            "cost": 60.0,
            "weight": "4 lb",
            "tags": [
                "Melee Weapon",
                "Missile Weapon"
            ],
            "features": [],
            "attacks": [
                {
                    "$type": "SagaGuide.Core.Domain.EquipmentAggregate.MeleeAttack, SagaGuide.Core",
                    "reach": "1",
                    "parry": "0U",
                    "block": "No",
                    "id": "8eaf7588-b9f4-4641-a762-9012cf3a039b",
                    "damage": {
                        "damageType": "cut",
                        "attackType": "sw",
                        "baseDamage": "2"
                    },
                    "minimumStrength": "11",
                    "usage": "Swung",
                    "defaults": [
                        {
                            "attributeType": "Dexterity",
                            "modifier": -5
                        },
                        {
                            "name": "Axe/Mace",
                            "modifier": 0
                        },
                        {
                            "name": "Flail",
                            "modifier": -4
                        },
                        {
                            "name": "Two-Handed Axe/Mace",
                            "modifier": -3
                        }
                    ]
                },
                {
                    "$type": "SagaGuide.Core.Domain.EquipmentAggregate.RangedAttack, SagaGuide.Core",
                    "accuracy": "2",
                    "range": "x1/x1.5",
                    "rateOfFire": "1",
                    "shots": "T(1)",
                    "bulk": "-3",
                    "id": "5d2a970a-01a3-4ef9-9090-3e81835b4e95",
                    "damage": {
                        "damageType": "cut",
                        "attackType": "sw",
                        "baseDamage": "2"
                    },
                    "minimumStrength": "11",
                    "usage": "Thrown",
                    "defaults": [
                        {
                            "attributeType": "Dexterity",
                            "modifier": -4
                        },
                        {
                            "name": "Thrown Weapon",
                            "specialization": "Axe/Mace",
                            "modifier": 0
                        }
                    ]
                }
            ],
            "modifiers": [],
            "ignoreWeightForSkills": false,
            "createdBy": "00000000-0000-0000-0000-000000000000",
            "createdOn": "2023-10-25T08:45:34.237802",
            "modifiedBy": "00000000-0000-0000-0000-000000000000",
            "modifiedOn": "2023-10-25T08:45:34.237802",
            "id": "244ed05d-f5c4-452f-aded-88f092e6fa09"
        },
        {
            "name": "Thrusting Bastard Sword",
            "bookReferences": [
                {
                    "pageNumber": 271,
                    "sourceBook": "BasicSet"
                }
            ],
            "techLevel": "3",
            "cost": 750.0,
            "weight": "5 lb",
            "tags": [
                "Melee Weapon"
            ],
            "features": [],
            "attacks": [
                {
                    "$type": "SagaGuide.Core.Domain.EquipmentAggregate.MeleeAttack, SagaGuide.Core",
                    "reach": "1,2",
                    "parry": "0U",
                    "block": "No",
                    "id": "e996d369-f7c5-44d8-8ac3-27464dbf8502",
                    "damage": {
                        "damageType": "cut",
                        "attackType": "sw",
                        "baseDamage": "1"
                    },
                    "minimumStrength": "11",
                    "usage": "Swung",
                    "usageNotes": "1-handed",
                    "defaults": [
                        {
                            "attributeType": "Dexterity",
                            "modifier": -5
                        },
                        {
                            "name": "Broadsword",
                            "modifier": 0
                        },
                        {
                            "name": "Force Sword",
                            "modifier": -4
                        },
                        {
                            "name": "Rapier",
                            "modifier": -4
                        },
                        {
                            "name": "Saber",
                            "modifier": -4
                        },
                        {
                            "name": "Shortsword",
                            "modifier": -2
                        },
                        {
                            "name": "Two-Handed Sword",
                            "modifier": 0
                        },
                        {
                            "name": "Sword!",
                            "modifier": 0
                        }
                    ]
                },
                {
                    "$type": "SagaGuide.Core.Domain.EquipmentAggregate.MeleeAttack, SagaGuide.Core",
                    "reach": "2",
                    "parry": "0U",
                    "block": "No",
                    "id": "f04d5b62-d226-4f09-8957-4cb473672ffa",
                    "damage": {
                        "damageType": "imp",
                        "attackType": "thr",
                        "baseDamage": "2"
                    },
                    "minimumStrength": "11",
                    "usage": "Thrust",
                    "usageNotes": "1-handed",
                    "defaults": [
                        {
                            "attributeType": "Dexterity",
                            "modifier": -5
                        },
                        {
                            "name": "Broadsword",
                            "modifier": 0
                        },
                        {
                            "name": "Force Sword",
                            "modifier": -4
                        },
                        {
                            "name": "Rapier",
                            "modifier": -4
                        },
                        {
                            "name": "Saber",
                            "modifier": -4
                        },
                        {
                            "name": "Shortsword",
                            "modifier": -2
                        },
                        {
                            "name": "Two-Handed Sword",
                            "modifier": 0
                        },
                        {
                            "name": "Sword!",
                            "modifier": 0
                        }
                    ]
                },
                {
                    "$type": "SagaGuide.Core.Domain.EquipmentAggregate.MeleeAttack, SagaGuide.Core",
                    "reach": "1,2",
                    "parry": "0",
                    "block": "No",
                    "id": "bc2ec4e2-1643-46df-9d14-2a0e1704f386",
                    "damage": {
                        "damageType": "cut",
                        "attackType": "sw",
                        "baseDamage": "2"
                    },
                    "minimumStrength": "10†",
                    "usage": "Swung",
                    "usageNotes": "2-handed",
                    "defaults": [
                        {
                            "attributeType": "Dexterity",
                            "modifier": -5
                        },
                        {
                            "name": "Two-Handed Sword",
                            "modifier": 0
                        },
                        {
                            "name": "Broadsword",
                            "modifier": -4
                        },
                        {
                            "name": "Force Sword",
                            "modifier": -4
                        },
                        {
                            "name": "Sword!",
                            "modifier": 0
                        }
                    ]
                },
                {
                    "$type": "SagaGuide.Core.Domain.EquipmentAggregate.MeleeAttack, SagaGuide.Core",
                    "reach": "2",
                    "parry": "0",
                    "block": "No",
                    "id": "a7b7ba35-ab9b-4d6d-be17-bdb82be674da",
                    "damage": {
                        "damageType": "imp",
                        "attackType": "thr",
                        "baseDamage": "3"
                    },
                    "minimumStrength": "10†",
                    "usage": "Thrust",
                    "usageNotes": "2-handed",
                    "defaults": [
                        {
                            "attributeType": "Dexterity",
                            "modifier": -5
                        },
                        {
                            "name": "Two-Handed Sword",
                            "modifier": 0
                        },
                        {
                            "name": "Broadsword",
                            "modifier": -4
                        },
                        {
                            "name": "Force Sword",
                            "modifier": -4
                        },
                        {
                            "name": "Sword!",
                            "modifier": 0
                        }
                    ]
                }
            ],
            "modifiers": [],
            "ignoreWeightForSkills": false,
            "createdBy": "00000000-0000-0000-0000-000000000000",
            "createdOn": "2023-10-25T08:45:34.238237",
            "modifiedBy": "00000000-0000-0000-0000-000000000000",
            "modifiedOn": "2023-10-25T08:45:34.238237",
            "id": "f4a38ef1-ef41-4a9e-bd30-54c7324fbbd9"
        }
    ];

export const equipmentsTestData = jsonData as unknown as IEquipment[];