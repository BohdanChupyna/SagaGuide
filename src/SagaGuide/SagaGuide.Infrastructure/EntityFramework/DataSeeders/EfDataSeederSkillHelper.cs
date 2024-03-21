using SagaGuide.Core.Domain.SkillAggregate;
using SagaGuide.Infrastructure.JsonConverters;

namespace SagaGuide.Infrastructure.EntityFramework.DataSeeders;

public static class EfDataSeederSkillHelper
{
    public static List<Skill> GetSkills()
    {
          const string json = @"[
            {
                ""id"": ""5ac0e358-c9bb-486e-bcaa-b56d2e1afacc"",
                ""type"": ""skill"",
                ""name"": ""Judo"",
                ""reference"": ""B203,MA57"",
                ""notes"": ""Allows parrying two different attacks per turn, one with each hand."",
                ""tags"": [
                ""Combat"",
                ""Melee Combat"",
                ""Weapon""
                  ],
                ""difficulty"": ""dx/h"",
                ""points"": 1,
                ""encumbrance_penalty_multiplier"": 1
            },
            {
                ""id"": ""0f7bf286-2776-4226-b859-b8ecb9431625"",
                ""type"": ""skill"",
                ""name"": ""Karate"",
                ""reference"": ""B203,MA57"",
                ""tags"": [
                ""Combat"",
                ""Melee Combat"",
                ""Weapon""
                  ],
                ""difficulty"": ""dx/h"",
                ""points"": 1,
                ""encumbrance_penalty_multiplier"": 1,
                ""features"": [
                {
                  ""type"": ""weapon_bonus"",
                  ""selection_type"": ""weapons_with_required_skill"",
                  ""name"": {
                      ""compare"": ""is"",
                      ""qualifier"": ""Karate""
                  },
                  ""level"": {
                      ""compare"": ""at_least""
                  },
                  ""amount"": 1,
                  ""per_level"": true
                },
                {
                  ""type"": ""weapon_bonus"",
                  ""selection_type"": ""weapons_with_required_skill"",
                  ""name"": {
                      ""compare"": ""is"",
                      ""qualifier"": ""Karate""
                  },
                  ""level"": {
                      ""compare"": ""at_least"",
                      ""qualifier"": 1
                  },
                  ""amount"": 1,
                  ""per_level"": true
                }
                ]
            },
			{
				""id"": ""435aab38-e1ac-43ac-89bd-c72cd1b2dc1a"",
				""type"": ""skill"",
				""name"": ""Accounting"",
				""reference"": ""B174"",
				""tags"": [
					""Business""
				],
				""difficulty"": ""iq/h"",
				""points"": 1,
				""defaults"": [
					{
						""type"": ""iq"",
						""modifier"": -6
					},
					{
						""type"": ""skill"",
						""name"": ""Finance"",
						""modifier"": -4
					},
					{
						""type"": ""skill"",
						""name"": ""Mathematics"",
						""specialization"": ""Statistics"",
						""modifier"": -5
					},
					{
						""type"": ""skill"",
						""name"": ""Merchant"",
						""modifier"": -5
					}
				]
			},
			{
				""id"": ""ca2a4b51-514e-4095-8fb8-0f5ec0f3e975"",
				""type"": ""skill"",
				""name"": ""Swimming"",
				""reference"": ""B224"",
				""tags"": [
					""Athletic"",
					""Exploration"",
					""Outdoor""
				],
				""difficulty"": ""ht/e"",
				""points"": 1,
				""encumbrance_penalty_multiplier"": 2,
				""defaults"": [
					{
						""type"": ""ht"",
						""modifier"": -4
					}
				]
			},
			{
				""id"": ""d9064ee5-dab6-43aa-b758-840ceba6a892"",
				""type"": ""skill"",
				""name"": ""Acting"",
				""reference"": ""B174"",
				""tags"": [
					""Social"",
					""Spy""
				],
				""difficulty"": ""iq/a"",
				""points"": 1,
				""defaults"": [
					{
						""type"": ""iq"",
						""modifier"": -5
					},
					{
						""type"": ""skill"",
						""name"": ""Performance"",
						""modifier"": -2
					},
					{
						""type"": ""skill"",
						""name"": ""Public Speaking"",
						""modifier"": -5
					}
				]
			},
			{
				""id"": ""3a6a81af-a2e5-414e-92e4-093df1e1b9dd"",
				""type"": ""skill"",
				""name"": ""Administration"",
				""reference"": ""B174"",
				""tags"": [
					""Business"",
					""Social""
				],
				""difficulty"": ""iq/a"",
				""points"": 1,
				""defaults"": [
					{
						""type"": ""skill"",
						""name"": ""Merchant"",
						""modifier"": -3
					},
					{
						""type"": ""iq"",
						""modifier"": -5
					}
				]
			},
			{
				""id"": ""1c88af23-4607-4448-83e3-ef4e7899d8ff"",
				""type"": ""skill"",
				""name"": ""Alchemy"",
				""reference"": ""B174"",
				""tags"": [
					""Magical"",
					""Natural Science"",
					""Occult""
				],
				""tech_level"": """",
				""difficulty"": ""iq/vh"",
				""points"": 1
			},
			{
				""id"": ""9bead93f-7da1-4222-a87e-2bed3832aefe"",
				""type"": ""skill"",
				""name"": ""Animal Handling"",
				""reference"": ""B175"",
				""tags"": [
					""Animal""
				],
				""specialization"": ""@Specialty@"",
				""difficulty"": ""iq/a"",
				""points"": 1,
				""defaults"": [
					{
						""type"": ""iq"",
						""modifier"": -5
					}
				]
			},
			{
				""id"": ""221f5c1e-4801-48b6-9253-bc9214cf3957"",
				""type"": ""skill"",
				""name"": ""Anthropology"",
				""reference"": ""B175"",
				""tags"": [
					""Humanities"",
					""Social Sciences""
				],
				""specialization"": ""@Species@"",
				""difficulty"": ""iq/h"",
				""points"": 1,
				""defaults"": [
					{
						""type"": ""iq"",
						""modifier"": -6
					},
					{
						""type"": ""skill"",
						""name"": ""Paleontology"",
						""specialization"": ""Paleoanthropology"",
						""modifier"": -2
					},
					{
						""type"": ""skill"",
						""name"": ""Sociology"",
						""modifier"": -3
					}
				]
			},
			{
				""id"": ""4533d744-d558-4ac2-974f-f0590388b94b"",
				""type"": ""skill"",
				""name"": ""Aquabatics"",
				""reference"": ""B174"",
				""tags"": [
					""Athletic""
				],
				""difficulty"": ""dx/h"",
				""points"": 1,
				""defaults"": [
					{
						""type"": ""dx"",
						""modifier"": -6
					},
					{
						""type"": ""skill"",
						""name"": ""Acrobatics"",
						""modifier"": -4
					},
					{
						""type"": ""skill"",
						""name"": ""Aerobatics"",
						""modifier"": -4
					}
				]
			},
			{
				""id"": ""b2f7e149-5c11-481e-97c6-c23415ae833f"",
				""type"": ""skill"",
				""name"": ""Archaeology"",
				""reference"": ""B176"",
				""tags"": [
					""Humanities"",
					""Social Sciences""
				],
				""difficulty"": ""iq/h"",
				""points"": 1,
				""defaults"": [
					{
						""type"": ""iq"",
						""modifier"": -6
					}
				]
			},
			{
				""id"": ""0de28e50-c231-4dcc-b86e-dc6d4e35b241"",
				""type"": ""skill"",
				""name"": ""Architecture"",
				""reference"": ""B176"",
				""tags"": [
					""Design"",
					""Invention""
				],
				""tech_level"": """",
				""difficulty"": ""iq/a"",
				""points"": 1,
				""defaults"": [
					{
						""type"": ""iq"",
						""modifier"": -5
					},
					{
						""type"": ""skill"",
						""name"": ""Engineer"",
						""specialization"": ""Civil"",
						""modifier"": -4
					}
				]
			},
			{
				""id"": ""58aed076-efab-49f7-8a20-b3c8ace15724"",
				""type"": ""skill"",
				""name"": ""Area Knowledge"",
				""reference"": ""B176"",
				""notes"": ""General nature of its settlements and towns, political allegiances, leaders, and most citizens of Status 5+"",
				""tags"": [
					""Everyman"",
					""Knowledge""
				],
				""specialization"": ""@Barony, County, Duchy, or Small Nation@"",
				""difficulty"": ""iq/e"",
				""points"": 1,
				""defaults"": [
					{
						""type"": ""skill"",
						""name"": ""Geography"",
						""specialization"": ""@Specialty@"",
						""modifier"": -3
					}
				]
			},
			{
				""id"": ""e2bcb35e-8477-42eb-8091-47925ea8ecd0"",
				""type"": ""skill"",
				""name"": ""Surgery"",
				""reference"": ""B223"",
				""tags"": [
					""Medical""
				],
				""tech_level"": """",
				""difficulty"": ""iq/vh"",
				""points"": 1,
				""defaults"": [
					{
						""type"": ""skill"",
						""name"": ""First Aid"",
						""modifier"": -12
					},
					{
						""type"": ""skill"",
						""name"": ""Physician"",
						""modifier"": -5
					},
					{
						""type"": ""skill"",
						""name"": ""Physiology"",
						""modifier"": -8
					},
					{
						""type"": ""skill"",
						""name"": ""Veterinary"",
						""modifier"": -5
					}
				],
				""prereqs"": {
					""type"": ""prereq_list"",
					""all"": false,
					""prereqs"": [
						{
							""type"": ""skill_prereq"",
							""has"": true,
							""name"": {
								""compare"": ""is"",
								""qualifier"": ""first aid""
							}
						},
						{
							""type"": ""skill_prereq"",
							""has"": true,
							""name"": {
								""compare"": ""is"",
								""qualifier"": ""physician""
							}
						}
					]
				}
			},
			{
				""id"": ""5e1c0a14-8625-40f4-9fd9-23a6e847688c"",
				""type"": ""skill"",
				""name"": ""Thrown Weapon"",
				""reference"": ""B226"",
				""tags"": [
					""Combat"",
					""Ranged Combat"",
					""Weapon""
				],
				""specialization"": ""Axe/Mace"",
				""difficulty"": ""dx/e"",
				""points"": 1,
				""defaults"": [
					{
						""type"": ""dx"",
						""modifier"": -4
					}
				]
			},
			{
				""id"": ""88c17f64-d52b-4db8-ace5-9efe1832b4b5"",
				""type"": ""skill"",
				""name"": ""Axe/Mace"",
				""reference"": ""B208"",
				""tags"": [
					""Combat"",
					""Melee Combat"",
					""Weapon""
				],
				""difficulty"": ""dx/a"",
				""points"": 1,
				""defaults"": [
					{
						""type"": ""dx"",
						""modifier"": -5
					},
					{
						""type"": ""skill"",
						""name"": ""Two-Handed Axe/Mace"",
						""modifier"": -3
					},
					{
						""type"": ""skill"",
						""name"": ""Flail"",
						""modifier"": -4
					}
				]
			},
			{
				""id"": ""dfac08e6-3434-4391-b2e9-8954c32e04af"",
				""type"": ""skill"",
				""name"": ""Guns"",
				""reference"": ""B198"",
				""tags"": [
					""Combat"",
					""Ranged Combat"",
					""Weapon""
				],
				""specialization"": ""Pistol"",
				""tech_level"": """",
				""difficulty"": ""dx/e"",
				""points"": 1,
				""defaults"": [
					{
						""type"": ""dx"",
						""modifier"": -4
					},
					{
						""type"": ""skill"",
						""name"": ""Guns"",
						""specialization"": ""Grenade Launcher"",
						""modifier"": -4
					},
					{
						""type"": ""skill"",
						""name"": ""Guns"",
						""specialization"": ""Gyroc"",
						""modifier"": -4
					},
					{
						""type"": ""skill"",
						""name"": ""Guns"",
						""specialization"": ""Light Anti-Armor Weapon"",
						""modifier"": -4
					},
					{
						""type"": ""skill"",
						""name"": ""Guns"",
						""specialization"": ""Light Machine Gun"",
						""modifier"": -2
					},
					{
						""type"": ""skill"",
						""name"": ""Guns"",
						""specialization"": ""Musket"",
						""modifier"": -2
					},
					{
						""type"": ""skill"",
						""name"": ""Beam Weapons"",
						""specialization"": ""Pistol"",
						""modifier"": -4
					},
					{
						""type"": ""skill"",
						""name"": ""Guns"",
						""specialization"": ""Rifle"",
						""modifier"": -2
					},
					{
						""type"": ""skill"",
						""name"": ""Guns"",
						""specialization"": ""Shotgun"",
						""modifier"": -2
					},
					{
						""type"": ""skill"",
						""name"": ""Guns"",
						""specialization"": ""Submachine Gun"",
						""modifier"": -2
					}
				]
			},
			{
				""id"": ""f4078925-07f5-439e-8ddf-7e41e717ccbf"",
				""type"": ""skill"",
				""name"": ""Guns"",
				""reference"": ""B198"",
				""tags"": [
					""Combat"",
					""Ranged Combat"",
					""Weapon""
				],
				""specialization"": ""Rifle"",
				""tech_level"": """",
				""difficulty"": ""dx/e"",
				""points"": 1,
				""defaults"": [
					{
						""type"": ""dx"",
						""modifier"": -4
					},
					{
						""type"": ""skill"",
						""name"": ""Guns"",
						""specialization"": ""Grenade Launcher"",
						""modifier"": -4
					},
					{
						""type"": ""skill"",
						""name"": ""Guns"",
						""specialization"": ""Gyroc"",
						""modifier"": -4
					},
					{
						""type"": ""skill"",
						""name"": ""Guns"",
						""specialization"": ""Light Anti-Armor Weapon"",
						""modifier"": -4
					},
					{
						""type"": ""skill"",
						""name"": ""Guns"",
						""specialization"": ""Light Machine Gun"",
						""modifier"": -2
					},
					{
						""type"": ""skill"",
						""name"": ""Guns"",
						""specialization"": ""Musket"",
						""modifier"": -2
					},
					{
						""type"": ""skill"",
						""name"": ""Guns"",
						""specialization"": ""Pistol"",
						""modifier"": -2
					},
					{
						""type"": ""skill"",
						""name"": ""Beam Weapons"",
						""specialization"": ""Rifle"",
						""modifier"": -4
					},
					{
						""type"": ""skill"",
						""name"": ""Guns"",
						""specialization"": ""Shotgun"",
						""modifier"": -2
					},
					{
						""type"": ""skill"",
						""name"": ""Guns"",
						""specialization"": ""Submachine Gun"",
						""modifier"": -2
					}
				]
			},
			{
				""id"": ""4ae61866-c3b3-4c48-bb72-513962af9fbe"",
				""type"": ""skill"",
				""name"": ""Armoury"",
				""reference"": ""B178"",
				""tags"": [
					""Maintenance"",
					""Military"",
					""Repair""
				],
				""specialization"": ""Battlesuits"",
				""tech_level"": """",
				""difficulty"": ""iq/a"",
				""points"": 1,
				""defaults"": [
					{
						""type"": ""iq"",
						""modifier"": -5
					},
					{
						""type"": ""skill"",
						""name"": ""Engineer"",
						""specialization"": ""Battlesuits"",
						""modifier"": -4
					}
				]
			},
			{
				""id"": ""72e8feab-a380-4af8-ae30-e3ae4325c494"",
				""type"": ""skill"",
				""name"": ""Artillery"",
				""reference"": ""B178"",
				""tags"": [
					""Combat"",
					""Ranged Combat"",
					""Weapon""
				],
				""specialization"": ""Beams"",
				""tech_level"": """",
				""difficulty"": ""iq/a"",
				""points"": 1,
				""defaults"": [
					{
						""type"": ""iq"",
						""modifier"": -5
					}
				]
			},
			{
				""id"": ""9ebc02db-5d3a-4235-842e-7b5b97b96c3b"",
				""type"": ""skill"",
				""name"": ""Artist"",
				""reference"": ""B179"",
				""tags"": [
					""Arts"",
					""Entertainment""
				],
				""specialization"": ""Body Art"",
				""difficulty"": ""iq/h"",
				""points"": 1,
				""defaults"": [
					{
						""type"": ""iq"",
						""modifier"": -6
					},
					{
						""type"": ""skill"",
						""name"": ""Artist"",
						""specialization"": ""Calligraphy"",
						""modifier"": -4
					},
					{
						""type"": ""skill"",
						""name"": ""Artist"",
						""specialization"": ""Drawing"",
						""modifier"": -4
					},
					{
						""type"": ""skill"",
						""name"": ""Artist"",
						""specialization"": ""Illumination"",
						""modifier"": -4
					},
					{
						""type"": ""skill"",
						""name"": ""Artist"",
						""specialization"": ""Painting"",
						""modifier"": -4
					},
					{
						""type"": ""skill"",
						""name"": ""Artist"",
						""modifier"": -6
					}
				]
			}
        ]";
        
        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        return JsonConverterWrapper.Deserialize<List<Skill>>(json, jsonSettings)!;
    }
    
}