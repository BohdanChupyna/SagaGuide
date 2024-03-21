// @ts-ignore
import {ITechnique} from "../redux/features/technique/ITechnique";

const jsonData =
    [
        {
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
            createdOn: '2023-08-03T09:27:44.695734',
            modifiedBy: '00000000-0000-0000-0000-000000000000',
            modifiedOn: '2023-08-03T09:27:44.695735',
            id: '3faabd25-9946-4c09-859c-56ceb9c3c84b'
        },
        {
            name: 'Off-Hand Weapon Training',
            tags: [
                'Combat',
                'Ranged Combat',
                'Technique',
                'Weapon'
            ],
            bookReferences: [
                {
                    pageNumber: 232,
                    sourceBook: 'BasicSet'
                }
            ],
            difficultyLevel: 'Hard',
            pointsCost: 2,
            'default': {
                name: 'Guns',
                specialization: 'Pistol',
                modifier: -4
            },
            techniqueLimitModifier: 0,
            createdBy: '00000000-0000-0000-0000-000000000000',
            createdOn: '2023-08-03T09:27:44.695942',
            modifiedBy: '00000000-0000-0000-0000-000000000000',
            modifiedOn: '2023-08-03T09:27:44.695942',
            id: 'b76c876b-8874-42c4-bccd-c378d2fdc0e8'
        },
        {
            name: 'Retain Weapon',
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
                    pageNumber: 78,
                    sourceBook: 'MartialArts'
                }
            ],
            difficultyLevel: 'Hard',
            pointsCost: 2,
            'default': {
                name: '@Melee Weapon Skill@',
                modifier: 0
            },
            techniqueLimitModifier: 5,
            createdBy: '00000000-0000-0000-0000-000000000000',
            createdOn: '2023-08-03T09:27:44.69592',
            modifiedBy: '00000000-0000-0000-0000-000000000000',
            modifiedOn: '2023-08-03T09:27:44.695921',
            id: '8105c2fa-ce51-41e7-8be7-ff194166a226'
        },
        {
            name: 'Retain Weapon (@Ranged Weapon@)',
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
                    pageNumber: 78,
                    sourceBook: 'MartialArts'
                }
            ],
            difficultyLevel: 'Hard',
            pointsCost: 2,
            'default': {
                attributeType: 'Dexterity',
                modifier: 0
            },
            techniqueLimitModifier: 5,
            createdBy: '00000000-0000-0000-0000-000000000000',
            createdOn: '2023-08-03T09:27:44.695939',
            modifiedBy: '00000000-0000-0000-0000-000000000000',
            modifiedOn: '2023-08-03T09:27:44.69594',
            id: '9eb76547-3426-46e2-94a9-2744ddd870a0'
        }
    ];

export const techniquesTestData = jsonData as unknown as ITechnique[];