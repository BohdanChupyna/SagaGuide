using SagaGuide.Core.Domain.EquipmentAggregate;
using SagaGuide.Infrastructure.JsonConverters;

namespace SagaGuide.Infrastructure.EntityFramework.DataSeeders;

public class EfDataSeederEquipmentHelper
{
      public static List<Equipment> GetAllEquipment()
    {
        var equipment = new List<Equipment>();
        equipment.AddRange(GetBasicEquipment());
        return equipment;
    }

    private static List<Equipment> GetBasicEquipment()
    {
        const string json = @"[{
            ""id"": ""f1ab1686-327b-42b5-9393-8b94d33cdc63"",
            ""type"": ""equipment"",
            ""description"": ""Scale Leggings"",
            ""reference"": ""B283"",
            ""tech_level"": ""2"",
            ""legality_class"": ""3"",
            ""tags"": [
              ""Limb Armor""
            ],
            ""quantity"": 1,
            ""value"": 250,
            ""weight"": ""21 lb"",
            ""features"": [
              {
                ""type"": ""dr_bonus"",
                ""location"": ""leg"",
                ""amount"": 4
              }
            ],
          },
          {
            ""id"": ""937a2a7a-76b4-4937-8949-7e1248afe3fe"",
            ""type"": ""equipment"",
            ""description"": ""Scale Armor"",
            ""reference"": ""B283"",
            ""tech_level"": ""2"",
            ""tags"": [
              ""Body Armor""
            ],
            ""quantity"": 1,
            ""value"": 420,
            ""weight"": ""35 lb"",
            ""features"": [
              {
                ""type"": ""dr_bonus"",
                ""location"": ""torso"",
                ""amount"": 4
              },
              {
                ""type"": ""dr_bonus"",
                ""location"": ""vitals"",
                ""amount"": 4
              },
              {
                ""type"": ""dr_bonus"",
                ""location"": ""groin"",
                ""amount"": 4
              }
            ],
          },
          {
            ""id"": ""37f5447c-8566-4a59-8b84-5272704fe90b"",
            ""type"": ""equipment"",
            ""description"": ""SAW, 5.56mm"",
            ""reference"": ""B281"",
            ""tech_level"": ""7"",
            ""legality_class"": ""1"",
            ""tags"": [
              ""Missile Weapon""
            ],
            ""quantity"": 1,
            ""value"": 4800,
            ""weight"": ""24 lb"",
            ""weapons"": [
              {
                ""id"": ""5d3068e8-1985-4621-9ae2-c23d4d182da4"",
                ""type"": ""ranged_weapon"",
                ""damage"": {
                  ""type"": ""pi"",
                  ""base"": ""5d+1""
                },
                ""strength"": ""12B†"",
                ""accuracy"": ""5"",
                ""range"": ""800/3500"",
                ""rate_of_fire"": ""12!"",
                ""shots"": ""200(5)"",
                ""bulk"": ""-6"",
                ""recoil"": ""2"",
                ""defaults"": [
                  {
                    ""type"": ""dx"",
                    ""modifier"": -4
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Guns"",
                    ""specialization"": ""Pistol"",
                    ""modifier"": -2
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
                    ""specialization"": ""Light Machine Gun""
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Guns"",
                    ""specialization"": ""Submachine Gun"",
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
                    ""name"": ""Gun!""
                  }
                ],
              }
            ],
          },
          {
            ""id"": ""244ed05d-f5c4-452f-aded-88f092e6fa09"",
            ""type"": ""equipment"",
            ""description"": ""Throwing Axe"",
            ""reference"": ""B271"",
            ""tech_level"": ""0"",
            ""tags"": [
              ""Melee Weapon"",
              ""Missile Weapon""
            ],
            ""quantity"": 1,
            ""value"": 60,
            ""weight"": ""4 lb"",
            ""weapons"": [
              {
                ""id"": ""8eaf7588-b9f4-4641-a762-9012cf3a039b"",
                ""type"": ""melee_weapon"",
                ""damage"": {
                  ""type"": ""cut"",
                  ""st"": ""sw"",
                  ""base"": ""2""
                },
                ""strength"": ""11"",
                ""usage"": ""Swung"",
                ""reach"": ""1"",
                ""parry"": ""0U"",
                ""block"": ""No"",
                ""defaults"": [
                  {
                    ""type"": ""dx"",
                    ""modifier"": -5
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Axe/Mace""
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Flail"",
                    ""modifier"": -4
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Two-Handed Axe/Mace"",
                    ""modifier"": -3
                  }
                ],
              },
              {
                ""id"": ""5d2a970a-01a3-4ef9-9090-3e81835b4e95"",
                ""type"": ""ranged_weapon"",
                ""damage"": {
                  ""type"": ""cut"",
                  ""st"": ""sw"",
                  ""base"": ""2""
                },
                ""strength"": ""11"",
                ""usage"": ""Thrown"",
                ""accuracy"": ""2"",
                ""range"": ""x1/x1.5"",
                ""rate_of_fire"": ""1"",
                ""shots"": ""T(1)"",
                ""bulk"": ""-3"",
                ""defaults"": [
                  {
                    ""type"": ""dx"",
                    ""modifier"": -4
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Thrown Weapon"",
                    ""specialization"": ""Axe/Mace""
                  }
                ],
              }
            ],
          },
          {
            ""id"": ""f4a38ef1-ef41-4a9e-bd30-54c7324fbbd9"",
            ""type"": ""equipment"",
            ""description"": ""Thrusting Bastard Sword"",
            ""reference"": ""B271"",
            ""tech_level"": ""3"",
            ""tags"": [
              ""Melee Weapon""
            ],
            ""quantity"": 1,
            ""value"": 750,
            ""weight"": ""5 lb"",
            ""weapons"": [
              {
                ""id"": ""e996d369-f7c5-44d8-8ac3-27464dbf8502"",
                ""type"": ""melee_weapon"",
                ""damage"": {
                  ""type"": ""cut"",
                  ""st"": ""sw"",
                  ""base"": ""1""
                },
                ""strength"": ""11"",
                ""usage"": ""Swung"",
                ""usage_notes"": ""1-handed"",
                ""reach"": ""1,2"",
                ""parry"": ""0U"",
                ""block"": ""No"",
                ""defaults"": [
                  {
                    ""type"": ""dx"",
                    ""modifier"": -5
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Broadsword""
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Force Sword"",
                    ""modifier"": -4
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Rapier"",
                    ""modifier"": -4
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Saber"",
                    ""modifier"": -4
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Shortsword"",
                    ""modifier"": -2
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Two-Handed Sword""
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Sword!""
                  }
                ],
              },
              {
                ""id"": ""f04d5b62-d226-4f09-8957-4cb473672ffa"",
                ""type"": ""melee_weapon"",
                ""damage"": {
                  ""type"": ""imp"",
                  ""st"": ""thr"",
                  ""base"": ""2""
                },
                ""strength"": ""11"",
                ""usage"": ""Thrust"",
                ""usage_notes"": ""1-handed"",
                ""reach"": ""2"",
                ""parry"": ""0U"",
                ""block"": ""No"",
                ""defaults"": [
                  {
                    ""type"": ""dx"",
                    ""modifier"": -5
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Broadsword""
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Force Sword"",
                    ""modifier"": -4
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Rapier"",
                    ""modifier"": -4
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Saber"",
                    ""modifier"": -4
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Shortsword"",
                    ""modifier"": -2
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Two-Handed Sword""
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Sword!""
                  }
                ],
              },
              {
                ""id"": ""bc2ec4e2-1643-46df-9d14-2a0e1704f386"",
                ""type"": ""melee_weapon"",
                ""damage"": {
                  ""type"": ""cut"",
                  ""st"": ""sw"",
                  ""base"": ""2""
                },
                ""strength"": ""10†"",
                ""usage"": ""Swung"",
                ""usage_notes"": ""2-handed"",
                ""reach"": ""1,2"",
                ""parry"": ""0"",
                ""block"": ""No"",
                ""defaults"": [
                  {
                    ""type"": ""dx"",
                    ""modifier"": -5
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Two-Handed Sword""
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Broadsword"",
                    ""modifier"": -4
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Force Sword"",
                    ""modifier"": -4
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Sword!""
                  }
                ],
              },
              {
                ""id"": ""a7b7ba35-ab9b-4d6d-be17-bdb82be674da"",
                ""type"": ""melee_weapon"",
                ""damage"": {
                  ""type"": ""imp"",
                  ""st"": ""thr"",
                  ""base"": ""3""
                },
                ""strength"": ""10†"",
                ""usage"": ""Thrust"",
                ""usage_notes"": ""2-handed"",
                ""reach"": ""2"",
                ""parry"": ""0"",
                ""block"": ""No"",
                ""defaults"": [
                  {
                    ""type"": ""dx"",
                    ""modifier"": -5
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Two-Handed Sword""
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Broadsword"",
                    ""modifier"": -4
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Force Sword"",
                    ""modifier"": -4
                  },
                  {
                    ""type"": ""skill"",
                    ""name"": ""Sword!""
                  }
                ],
              }
            ],
          },
          {
            ""id"": ""8ef3ab82-c779-45a3-b207-64e54c301479"",
            ""type"": ""equipment"",
            ""description"": ""Group Basics"",
            ""reference"": ""B288"",
            ""notes"": ""Basic equipment for Cooking and Survival skill for a group. Cook pot, rope, hatchet etc., for 3-8 campers."",
            ""tech_level"": ""0"",
            ""tags"": [
              ""Camping and Survival Gear""
            ],
            ""quantity"": 1,
            ""value"": 50,
            ""weight"": ""20 lb"",
          },
		      {
			      ""id"": ""e3c1f380-1380-4d3f-adc2-fce98369059e"",
              ""type"": ""equipment"",
              ""description"": ""Small Buckler"",
              ""reference"": ""B287"",
              ""tech_level"": ""1"",
              ""tags"": [
              ""Shield""
                ],
              ""quantity"": 1,
              ""value"": 40,
              ""weight"": ""8 lb"",
              ""weapons"": [
                {
                  ""id"": ""3e7b8a4b-5bea-4fa0-890e-f21ba658351b"",
                  ""type"": ""melee_weapon"",
                  ""damage"": {
                    ""type"": ""cr"",
                    ""st"": ""thr""
                  },
                  ""strength"": ""0"",
                  ""reach"": ""1"",
                  ""parry"": ""No"",
                  ""block"": ""+0"",
                  ""defaults"": [
                    {
                      ""type"": ""dx"",
                      ""modifier"": -4
                    },
                    {
                      ""type"": ""skill"",
                      ""name"": ""Shield"",
                      ""specialization"": ""Shield"",
                      ""modifier"": -2
                    },
                    {
                      ""type"": ""skill"",
                      ""name"": ""Shield"",
                      ""specialization"": ""Force Shield"",
                      ""modifier"": -2
                    },
                    {
                      ""type"": ""skill"",
                      ""name"": ""Shield"",
                      ""specialization"": ""Buckler""
                    }
                ],
              }
              ],
              ""features"": [
              {
                ""type"": ""attribute_bonus"",
                ""attribute"": ""dodge"",
                ""amount"": 1
              },
              {
                ""type"": ""attribute_bonus"",
                ""attribute"": ""parry"",
                ""amount"": 1
              },
              {
                ""type"": ""attribute_bonus"",
                ""attribute"": ""block"",
                ""amount"": 1
              }
              ],
          },
        ]";
        
        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        return JsonConverterWrapper.Deserialize<List<Equipment>>(json, jsonSettings)!;
    }
}