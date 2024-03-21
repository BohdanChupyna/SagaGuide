// @ts-ignore
import {ISkill} from "../redux/features/skill/ISkill";

const jsonData =
    [
        {
            name: 'Alchemy',
            tags: [],
            techLevel: 0,
            difficultyLevel: 'VeryHard',
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
            createdOn: '2023-08-02T08:37:09.047522',
            modifiedBy: '00000000-0000-0000-0000-000000000000',
            modifiedOn: '2023-08-02T08:37:09.047522',
            id: '026c343d-f6ac-45b2-9ac3-a1a216755f17'
        },
        {
            name: 'Guns',
            tags: [
                'Combat',
                'Ranged Combat',
                'Weapon'
            ],
            specialization: 'Pistol',
            difficultyLevel: 'Easy',
            pointsCost: 1,
            defaults: [
                {
                    attributeType: 'Dexterity',
                    modifier: -4
                }
            ],
            attributeType: 'Dexterity',
            bookReferences: [
                {
                    pageNumber: 198,
                    sourceBook: 'BasicSet'
                }
            ],
            createdBy: '00000000-0000-0000-0000-000000000000',
            createdOn: '2023-08-02T08:37:09.047574',
            modifiedBy: '00000000-0000-0000-0000-000000000000',
            modifiedOn: '2023-08-02T08:37:09.047574',
            id: 'dfac08e6-3434-4391-b2e9-8954c32e04af'
        },
    ];

export const skillsTestData = jsonData as unknown as ISkill[];